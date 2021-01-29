using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteConfiguration
{
	[ExcludeFromCodeCoverage]
	public static class Returned_Value_Is_RouteBuilder_Identical_To_This
	{
		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpoint()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", _ => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpointNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", () => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpointNoParametersSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", Response.Ok);
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpointSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", _ => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpoint()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", _ => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpointNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", () => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpointNoParametersSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", Response.Ok);
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpointSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", _ => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpoint()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", _ => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpointNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", () => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpointNoParametersSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", Response.Ok);
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpointSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", _ => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpoint()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", _ => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpointNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", () => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpointNoParametersSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", Response.Ok);
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpointSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", _ => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpoint()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", _ => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpointNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", () => ValueTask.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpointNoParametersSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", Response.Ok);
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpointSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", _ => Response.Ok());
				});

			Assert.Same(expected, actual);
		}
	}
}
