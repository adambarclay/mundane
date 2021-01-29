using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Mundane.RoutingImplementation.Lookup;

namespace Mundane
{
	/// <summary>The route configuration delegate.</summary>
	/// <param name="routeConfiguration">Configures routes for <see cref="Routing"/>.</param>
	public delegate void RouteConfigurationBuilder([DisallowNull] RouteConfiguration routeConfiguration);

	/// <summary>The Mundane engine routing configuration.</summary>
	public sealed class Routing
	{
		private static readonly Dictionary<string, string> EmptyDictionary = new Dictionary<string, string>(0);

		private readonly EndpointData[] endpoints;
		private readonly Dictionary<string, RouteNode[]> lookup;

		/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
		/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
		/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> is <see langword="null"/>.</exception>
		public Routing([DisallowNull] RouteConfigurationBuilder routeConfigurationBuilder)
		{
			if (routeConfigurationBuilder == null)
			{
				throw new ArgumentNullException(nameof(routeConfigurationBuilder));
			}

			var routeConfiguration = new RouteConfiguration();

			routeConfigurationBuilder.Invoke(routeConfiguration);

			(this.lookup, this.endpoints) =
				routeConfiguration.Build(MundaneEndpointFactory.Create(this.DefaultNotFoundResponse));
		}

		/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
		/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
		/// <param name="notFoundEndpoint">The endpoint to execute when the request path is not matched to any route.</param>
		/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> or <paramref name="notFoundEndpoint"/> is <see langword="null"/>.</exception>
		public Routing(
			[DisallowNull] RouteConfigurationBuilder routeConfigurationBuilder,
			[DisallowNull] MundaneEndpoint notFoundEndpoint)
		{
			if (routeConfigurationBuilder == null)
			{
				throw new ArgumentNullException(nameof(routeConfigurationBuilder));
			}

			if (notFoundEndpoint == null)
			{
				throw new ArgumentNullException(nameof(notFoundEndpoint));
			}

			var routeConfiguration = new RouteConfiguration();

			routeConfigurationBuilder.Invoke(routeConfiguration);

			(this.lookup, this.endpoints) = routeConfiguration.Build(notFoundEndpoint);
		}

		/// <summary>Gets the HTTP methods by which a path can be accessed.</summary>
		/// <param name="path">The request path.</param>
		/// <returns>A collection of HTTP methods.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
		[return: NotNull]
		public string[] AllowedMethodsForPath([DisallowNull] string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			var foundGet = false;
			var foundHead = false;
			var length = 0;

			foreach ((var method, var tree) in this.lookup)
			{
				if (Routing.Find(this.endpoints, tree, path) > 0)
				{
					if (method == HttpMethod.Get)
					{
						foundGet = true;
					}
					else if (method == HttpMethod.Head)
					{
						foundHead = true;
					}

					++length;
				}
			}

			var addHead = foundGet && !foundHead;

			if (addHead)
			{
				++length;
			}

			var allowedMethods = new string[length];
			var count = 0;

			foreach ((var method, var tree) in this.lookup)
			{
				if (Routing.Find(this.endpoints, tree, path) > 0)
				{
					allowedMethods[count++] = method;
				}
			}

			if (addHead)
			{
				allowedMethods[count] = HttpMethod.Head;
			}

			Array.Sort(allowedMethods);

			return allowedMethods;
		}

		/// <summary>The default response when no match is found for a route. This can be overriden with NotFound methods of <see cref="RouteConfiguration"/>.</summary>
		/// <param name="request">The HTTP request.</param>
		/// <returns>405 Method Not Allowed if the route is registered to a different method, otherwise 404 Not Found.</returns>
		[return: NotNull]
		public Response DefaultNotFoundResponse([DisallowNull] Request request)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			var allowedMethods = this.AllowedMethodsForPath(request.Path);

			return allowedMethods.Length == 0 ? Response.NotFound() : Response.MethodNotAllowed(allowedMethods);
		}

		/// <summary>Finds an endpoint matching the HTTP method and request path.</summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="path">The request path.</param>
		/// <returns>The matching endpoint and any parameters extracted from the route.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="method"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		public RequestEndpoint FindEndpoint([DisallowNull] string method, [DisallowNull] string path)
		{
			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			if (path.Length == 0 || path[0] != '/')
			{
				throw new ArgumentException("The path must start with a forward slash \"/\".", nameof(path));
			}

			var endpointIndex = this.lookup.TryGetValue(method, out var tree)
				? Routing.Find(this.endpoints, tree, path)
				: 0;

			ref readonly var endpointData = ref this.endpoints[endpointIndex];

			var routeParameters = endpointData.NumberOfCaptureParameters > 0
				? Routing.CaptureRouteParameters(in endpointData, path)
				: Routing.EmptyDictionary;

			return new RequestEndpoint(
				endpointData.Endpoint,
				new EnumerableDictionary<string, string>(routeParameters));
		}

		private static Dictionary<string, string> CaptureRouteParameters(in EndpointData endpointData, string path)
		{
			var routeParameters = new Dictionary<string, string>(endpointData.NumberOfCaptureParameters);

			var pathSegments = new PathSegments(path);

			foreach (ref readonly var node in new ReadOnlySpan<RouteSegment>(endpointData.NodesInRoute))
			{
				Debug.Assert(pathSegments.MoreSegments);

				if (node.Type == NodeType.Capture)
				{
					routeParameters.Add(node.Value, WebUtility.UrlDecode(new string(pathSegments.Next())));
				}
				else if (node.Type == NodeType.Greedy)
				{
					routeParameters.Add(
						node.Value,
						WebUtility.UrlDecode(new string(pathSegments.AllRemaining(endpointData.CaptureTrailingSlash))));
				}
				else
				{
					pathSegments.Next();
				}
			}

			return routeParameters;
		}

		private static int Find(EndpointData[] endpoints, RouteNode[] routeNodes, string path)
		{
			if (path.Length == 1)
			{
				return routeNodes[0].Endpoint;
			}

			var nodes = (Span<int>)stackalloc int[routeNodes.Length];

			var nodesCount = 0;

			foreach (var childNodeIndex in routeNodes[0].Children)
			{
				nodes[nodesCount++] = childNodeIndex;
			}

			var bestEndpoint = 0;

			var startIndex = 0;

			var pathSegments = new PathSegments(path);

			while (pathSegments.MoreSegments)
			{
				var pathSegment = pathSegments.Next();

				var endIndex = nodesCount;

				for (var i = startIndex; i < endIndex; ++i)
				{
					var currentNodeIndex = nodes[i];

					ref readonly var node = ref routeNodes[currentNodeIndex];

					if ((node.Type == NodeType.Literal && pathSegment.Equals(node.Value, StringComparison.Ordinal)) ||
						node.Type == NodeType.Capture)
					{
						if (pathSegments.MoreSegments)
						{
							foreach (var childNodeIndex in node.Children)
							{
								nodes[nodesCount++] = childNodeIndex;
							}
						}
						else if (node.HasEndpoint &&
							Routing.NodeBeatsBest(in endpoints[node.Endpoint].Score, in endpoints[bestEndpoint].Score))
						{
							bestEndpoint = node.Endpoint;
						}
					}
					else if (node.Type == NodeType.Greedy)
					{
						var nodeEndpoint = node.Children.Length == 0 || path[^1] != '/'
							? node.Endpoint
							: routeNodes[node.Children[0]].Endpoint;

						if (Routing.NodeBeatsBest(in endpoints[nodeEndpoint].Score, in endpoints[bestEndpoint].Score))
						{
							bestEndpoint = nodeEndpoint;
						}
					}
				}

				startIndex = endIndex;
			}

			return bestEndpoint;
		}

		private static bool NodeBeatsBest(in Score node, in Score best)
		{
			bool result;

			if (node.Literal != best.Literal)
			{
				result = node.Literal > best.Literal;
			}
			else if (node.Capture != best.Capture)
			{
				result = node.Capture > best.Capture;
			}
			else if (node.Greedy != best.Greedy)
			{
				result = node.Greedy > best.Greedy;
			}
			else
			{
				result = node.Order < best.Order;
			}

			return result;
		}
	}
}
