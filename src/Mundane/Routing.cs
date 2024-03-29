using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using Mundane.RoutingImplementation.Lookup;

namespace Mundane;

/// <summary>The Mundane engine routing configuration.</summary>
public sealed class Routing
{
	private static readonly RouteParameters EmptyRouteParameters =
		new RouteParameters(new Dictionary<string, string>(0));

	private readonly EndpointData[] endpoints;
	private readonly Dictionary<string, RouteNode[]> lookup;

	/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
	/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
	/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> is <see langword="null"/>.</exception>
	public Routing(RouteConfigurationBuilder routeConfigurationBuilder)
	{
		ArgumentNullException.ThrowIfNull(routeConfigurationBuilder);

		(this.lookup, this.endpoints) = Routing.Build(
			routeConfigurationBuilder,
			MundaneEndpointFactory.Create(this.DefaultNotFoundResponse));
	}

	/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
	/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
	/// <param name="notFoundEndpoint">The endpoint to execute when the request path is not matched to any route.</param>
	/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> or <paramref name="notFoundEndpoint"/> is <see langword="null"/>.</exception>
	public Routing(RouteConfigurationBuilder routeConfigurationBuilder, MundaneEndpointSync notFoundEndpoint)
	{
		ArgumentNullException.ThrowIfNull(routeConfigurationBuilder);
		ArgumentNullException.ThrowIfNull(notFoundEndpoint);

		(this.lookup, this.endpoints) = Routing.Build(
			routeConfigurationBuilder,
			MundaneEndpointFactory.Create(notFoundEndpoint));
	}

	/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
	/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
	/// <param name="notFoundEndpoint">The endpoint to execute when the request path is not matched to any route.</param>
	/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> or <paramref name="notFoundEndpoint"/> is <see langword="null"/>.</exception>
	public Routing(
		RouteConfigurationBuilder routeConfigurationBuilder,
		MundaneEndpointNoParametersSync notFoundEndpoint)
	{
		ArgumentNullException.ThrowIfNull(routeConfigurationBuilder);
		ArgumentNullException.ThrowIfNull(notFoundEndpoint);

		(this.lookup, this.endpoints) = Routing.Build(
			routeConfigurationBuilder,
			MundaneEndpointFactory.Create(notFoundEndpoint));
	}

	/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
	/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
	/// <param name="notFoundEndpoint">The endpoint to execute when the request path is not matched to any route.</param>
	/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> or <paramref name="notFoundEndpoint"/> is <see langword="null"/>.</exception>
	public Routing(RouteConfigurationBuilder routeConfigurationBuilder, MundaneEndpointNoParameters notFoundEndpoint)
	{
		ArgumentNullException.ThrowIfNull(routeConfigurationBuilder);
		ArgumentNullException.ThrowIfNull(notFoundEndpoint);

		(this.lookup, this.endpoints) = Routing.Build(
			routeConfigurationBuilder,
			MundaneEndpointFactory.Create(notFoundEndpoint));
	}

	/// <summary>Initializes a new instance of the <see cref="Routing"/> class.</summary>
	/// <param name="routeConfigurationBuilder">The route configuration builder.</param>
	/// <param name="notFoundEndpoint">The endpoint to execute when the request path is not matched to any route.</param>
	/// <exception cref="ArgumentNullException"><paramref name="routeConfigurationBuilder"/> or <paramref name="notFoundEndpoint"/> is <see langword="null"/>.</exception>
	public Routing(RouteConfigurationBuilder routeConfigurationBuilder, MundaneEndpoint notFoundEndpoint)
	{
		ArgumentNullException.ThrowIfNull(routeConfigurationBuilder);
		ArgumentNullException.ThrowIfNull(notFoundEndpoint);

		(this.lookup, this.endpoints) = Routing.Build(routeConfigurationBuilder, notFoundEndpoint);
	}

	/// <summary>Gets the HTTP methods by which a path can be accessed.</summary>
	/// <param name="path">The request path.</param>
	/// <returns>A collection of HTTP methods.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
	public string[] AllowedMethodsForPath(string path)
	{
		ArgumentNullException.ThrowIfNull(path);

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
	public Response DefaultNotFoundResponse(Request request)
	{
		ArgumentNullException.ThrowIfNull(request);

		var allowedMethods = this.AllowedMethodsForPath(request.Path);

		return allowedMethods.Length == 0 ? Response.NotFound() : Response.MethodNotAllowed(allowedMethods);
	}

	/// <summary>Finds an endpoint matching the HTTP method and request path.</summary>
	/// <param name="method">The HTTP method.</param>
	/// <param name="path">The request path.</param>
	/// <returns>The matching endpoint and any parameters extracted from the route.</returns>
	/// <exception cref="ArgumentNullException"><paramref name="method"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
	public RequestEndpoint FindEndpoint(string method, string path)
	{
		ArgumentNullException.ThrowIfNull(method);
		ArgumentNullException.ThrowIfNull(path);

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
			: Routing.EmptyRouteParameters;

		return new RequestEndpoint(endpointData.Endpoint, routeParameters);
	}

	private static (Dictionary<string, RouteNode[]> Lookup, EndpointData[] Endpoints) Build(
		RouteConfigurationBuilder routeConfigurationBuilder,
		MundaneEndpoint notFoundEndpoint)
	{
		var routeConfiguration = new RouteConfiguration();

		routeConfigurationBuilder.Invoke(routeConfiguration);

		return routeConfiguration.Build(notFoundEndpoint);
	}

	private static RouteParameters CaptureRouteParameters(in EndpointData endpointData, string path)
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

		return new RouteParameters(routeParameters);
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
