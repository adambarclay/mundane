using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_RouteBuilder
{
	[ExcludeFromCodeCoverage]
	public static class Returned_Value_Is_RouteBuilder_Identical_To_This
	{
		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpointDelegate()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", r => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpointDelegateNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", () => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Delete_Is_Called_With_MundaneEndpointDelegateNoParametersSync()
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
		public static void When_Delete_Is_Called_With_MundaneEndpointDelegateSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Delete("/", r => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpointDelegate()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", r => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpointDelegateNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", () => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Endpoint_Is_Called_With_MundaneEndpointDelegateNoParametersSync()
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
		public static void When_Endpoint_Is_Called_With_MundaneEndpointDelegateSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Endpoint("method", "/", r => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpointDelegate()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", r => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpointDelegateNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", () => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Get_Is_Called_With_MundaneEndpointDelegateNoParametersSync()
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
		public static void When_Get_Is_Called_With_MundaneEndpointDelegateSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Get("/", r => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_NotFound_Is_Called_With_MundaneEndpointDelegate()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.NotFound(r => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_NotFound_Is_Called_With_MundaneEndpointDelegateNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.NotFound(() => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_NotFound_Is_Called_With_MundaneEndpointDelegateNoParametersSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.NotFound(Response.Ok);
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_NotFound_Is_Called_With_MundaneEndpointDelegateSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.NotFound(r => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpointDelegate()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", r => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpointDelegateNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", () => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Post_Is_Called_With_MundaneEndpointDelegateNoParametersSync()
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
		public static void When_Post_Is_Called_With_MundaneEndpointDelegateSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Post("/", r => Response.Ok());
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpointDelegate()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", r => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpointDelegateNoParameters()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", () => Task.FromResult(Response.Ok()));
				});

			Assert.Same(expected, actual);
		}

		[Fact]
		public static void When_Put_Is_Called_With_MundaneEndpointDelegateNoParametersSync()
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
		public static void When_Put_Is_Called_With_MundaneEndpointDelegateSync()
		{
			RouteConfiguration expected = null!;
			RouteConfiguration actual = null!;

			var unused = new Routing(
				routeConfiguration =>
				{
					expected = routeConfiguration;
					actual = routeConfiguration.Put("/", r => Response.Ok());
				});

			Assert.Same(expected, actual);
		}
	}
}
