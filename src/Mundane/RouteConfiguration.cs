using System;
using System.Collections.Generic;
using Mundane.RoutingImplementation.Build;
using Mundane.RoutingImplementation.Lookup;

namespace Mundane
{
	/// <summary>Builds routes for <see cref="Routing"/>.</summary>
	public sealed class RouteConfiguration
	{
		private readonly Dictionary<string, RouteNodeTreeBuilder> lookupBuilder;

		internal RouteConfiguration()
		{
			this.lookupBuilder = new Dictionary<string, RouteNodeTreeBuilder>();
		}

		private delegate MundaneEndpoint CreateEndpoint<in T>(T endpoint)
			where T : Delegate;

		/// <summary>Adds an endpoint for the "DELETE" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Delete(string route, MundaneEndpointNoParametersSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Delete, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "DELETE" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Delete(string route, MundaneEndpointSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Delete, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "DELETE" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Delete(string route, MundaneEndpointNoParameters endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Delete, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "DELETE" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Delete(string route, MundaneEndpoint endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Delete, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the specified HTTP method.</summary>
		/// <param name="method">The HTTP method which will trigger this endpoint.</param>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="method"/>, <paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="method"/> is empty or <paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Endpoint(string method, string route, MundaneEndpointNoParametersSync endpoint)
		{
			try
			{
				return this.AddEndpointWithValidatedMethod(method, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the specified HTTP method.</summary>
		/// <param name="method">The HTTP method which will trigger this endpoint.</param>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="method"/>, <paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="method"/> is empty or <paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Endpoint(string method, string route, MundaneEndpointSync endpoint)
		{
			try
			{
				return this.AddEndpointWithValidatedMethod(method, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the specified HTTP method.</summary>
		/// <param name="method">The HTTP method which will trigger this endpoint.</param>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="method"/>, <paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="method"/> is empty or <paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Endpoint(string method, string route, MundaneEndpointNoParameters endpoint)
		{
			try
			{
				return this.AddEndpointWithValidatedMethod(method, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the specified HTTP method.</summary>
		/// <param name="method">The HTTP method which will trigger this endpoint.</param>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="method"/>, <paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="method"/> is empty or <paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Endpoint(string method, string route, MundaneEndpoint endpoint)
		{
			try
			{
				return this.AddEndpointWithValidatedMethod(method, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "GET" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Get(string route, MundaneEndpointNoParametersSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Get, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "GET" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Get(string route, MundaneEndpointSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Get, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "GET" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Get(string route, MundaneEndpointNoParameters endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Get, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "GET" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Get(string route, MundaneEndpoint endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Get, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "POST" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Post(string route, MundaneEndpointNoParametersSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Post, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "POST" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Post(string route, MundaneEndpointSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Post, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "POST" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Post(string route, MundaneEndpointNoParameters endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Post, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "POST" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Post(string route, MundaneEndpoint endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Post, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "PUT" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Put(string route, MundaneEndpointNoParametersSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Put, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "PUT" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Put(string route, MundaneEndpointSync endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Put, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "PUT" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Put(string route, MundaneEndpointNoParameters endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Put, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		/// <summary>Adds an endpoint for the "PUT" HTTP method.</summary>
		/// <param name="route">The route URL which will trigger this endpoint.</param>
		/// <param name="endpoint">The endpoint to execute.</param>
		/// <returns>The Mundane engine routing configuration builder.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="route"/> or <paramref name="endpoint"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="route"/> is invalid.</exception>
		public RouteConfiguration Put(string route, MundaneEndpoint endpoint)
		{
			try
			{
				return this.AddEndpoint(HttpMethod.Put, route, endpoint, MundaneEndpointFactory.Create);
			}
			catch (ArgumentException exception)
			{
				throw exception;
			}
		}

		internal (Dictionary<string, RouteNode[]> Lookup, EndpointData[] Endpoints) Build(
			MundaneEndpoint notFoundHandler)
		{
			var lookup = new Dictionary<string, RouteNode[]>(this.lookupBuilder.Count);

			var numberOfEndpoints = 0;

			foreach (var treeBuilder in this.lookupBuilder.Values)
			{
				numberOfEndpoints += treeBuilder.NumberOfEndpoints;
			}

			var endpoints = new EndpointData[numberOfEndpoints + 1];
			var count = 0;

			endpoints[count++] = new EndpointData(
				new Score(0, 0, 0, ushort.MaxValue),
				notFoundHandler,
				0,
				true,
				Array.Empty<RouteSegment>());

			foreach ((var method, var treeBuilder) in this.lookupBuilder)
			{
				lookup.Add(method, treeBuilder.Build(endpoints, ref count));
			}

			return (lookup, endpoints);
		}

		private RouteConfiguration AddEndpoint<T>(
			string method,
			string route,
			T endpoint,
			CreateEndpoint<T> createEndpoint)
			where T : Delegate
		{
			if (route == null)
			{
				throw new ArgumentNullException(nameof(route));
			}

			if (endpoint == null)
			{
				throw new ArgumentNullException(nameof(endpoint));
			}

			if (route.AsSpan().Trim().IsEmpty || route[0] != '/')
			{
				throw new ArgumentException("Route must start with a forward slash \"/\".", nameof(route));
			}

			if (!this.lookupBuilder.TryGetValue(method, out var routeNodeTreeBuilder))
			{
				this.lookupBuilder.Add(method, routeNodeTreeBuilder = new RouteNodeTreeBuilder());
			}

			routeNodeTreeBuilder.AddRoute(route, createEndpoint(endpoint));

			return this;
		}

		private RouteConfiguration AddEndpointWithValidatedMethod<T>(
			string method,
			string route,
			T endpoint,
			CreateEndpoint<T> createEndpoint)
			where T : Delegate
		{
			if (method == null)
			{
				throw new ArgumentNullException(nameof(method));
			}

			if (method.AsSpan().Trim().IsEmpty)
			{
				throw new ArgumentException("Method must have a value.", nameof(method));
			}

			return this.AddEndpoint(method, route, endpoint, createEndpoint);
		}
	}
}
