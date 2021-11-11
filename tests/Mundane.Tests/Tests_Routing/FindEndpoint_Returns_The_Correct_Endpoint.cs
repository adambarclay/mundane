using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing;

[ExcludeFromCodeCoverage]
public static class FindEndpoint_Returns_The_Correct_Endpoint
{
	[Fact]
	public static async Task When_The_Path_Has_A_Greedy_Segment_And_A_Trailing_Slash()
	{
		var routing = new Routing(
			o =>
			{
				o.Get("/{greedy*}", () => Response.Ok(x => x.Write("Without")));
				o.Get("/{greedy*}/", () => Response.Ok(x => x.Write("With")));
			});

		var responseWithout = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/a/b/c").Endpoint,
			RequestHelper.Request());

		var responseWith = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/a/b/c/").Endpoint,
			RequestHelper.Request());

		Assert.Equal("Without", await ResponseHelper.Body(responseWithout));
		Assert.Equal("With", await ResponseHelper.Body(responseWith));
	}

	[Fact]
	public static async Task When_The_Path_Has_Trailing_Slash()
	{
		var routing = new Routing(
			o =>
			{
				o.Get("/path", () => Response.Ok(x => x.Write("Without")));
				o.Get("/path/", () => Response.Ok(x => x.Write("With")));
			});

		var responseWithout = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/path").Endpoint,
			RequestHelper.Request());

		var responseWith = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/path/").Endpoint,
			RequestHelper.Request());

		Assert.Equal("Without", await ResponseHelper.Body(responseWithout));
		Assert.Equal("With", await ResponseHelper.Body(responseWith));
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Greedy_Capture()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/one/two/three").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetGreedyCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Capture()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/one").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetSingleCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Capture_And_A_Greedy_Capture()
	{
		var routing = new Routing(
			o =>
			{
				FindEndpoint_Returns_The_Correct_Endpoint.TestRouting(o);

				o.Get("/{Capture}/{Greedy*}", () => Response.Ok(x => x.Write("GetSingleCaptureAndGreedyCapture")));
			});

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/one/two/three/four").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetSingleCaptureAndGreedyCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Literal()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/single-literal").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetSingleLiteral", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Literal_And_A_Greedy_Capture()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/literal/one").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetSingleLiteralAndGreedyCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_Just_The_Root()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetRoot", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Captures()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/one/two").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetMultipleCaptures", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Captures_And_A_Greedy_Capture()
	{
		var routing = new Routing(
			o =>
			{
				FindEndpoint_Returns_The_Correct_Endpoint.TestRouting(o);

				o.Get(
					"/{CaptureOne}/{CaptureTwo}/{Greedy*}",
					() => Response.Ok(x => x.Write("GetMultipleCapturesAndGreedyCapture")));
			});

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/one/two/three/four").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetMultipleCapturesAndGreedyCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Literals()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/multiple/literals").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetMultipleLiterals", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Literals_And_A_Greedy_Capture()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/multiple/literals/one/two").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetMultipleLiteralsAndGreedyCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Literals_Multiple_Captures_And_A_Greedy_Capture()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		var response = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/multiple/one/literals/two/three/four").Endpoint,
			RequestHelper.Request());

		Assert.Equal("GetMultipleLiteralsMultipleCapturesAndGreedyCapture", await ResponseHelper.Body(response));
	}

	[Fact]
	public static async Task When_The_Route_Is_Defined_For_Multiple_Methods()
	{
		var routing = new Routing(FindEndpoint_Returns_The_Correct_Endpoint.TestRouting);

		const string route = "/duplicate";

		var deleteResponse = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Delete, route).Endpoint,
			RequestHelper.Request());

		Assert.Equal("DeleteDuplicate", await ResponseHelper.Body(deleteResponse));

		var postResponse = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Post, route).Endpoint,
			RequestHelper.Request());

		Assert.Equal("PostDuplicate", await ResponseHelper.Body(postResponse));
	}

	[Fact]
	public static async Task When_Two_Similar_Routes_Match_The_Path_The_One_Defined_First_Wins()
	{
		var routing = new Routing(
			o =>
			{
				o.Get("/one/{capture}", () => Response.Ok(x => x.Write("First")));
				o.Get("/{capture}/two", () => Response.Ok(x => x.Write("Second")));
				o.Get("/{capture}/three", () => Response.Ok(x => x.Write("First")));
				o.Get("/four/{capture}", () => Response.Ok(x => x.Write("Second")));
			});

		var firstResponse = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/one/two").Endpoint,
			RequestHelper.Request());

		Assert.Equal("First", await ResponseHelper.Body(firstResponse));

		var secondResponse = await MundaneEngine.ExecuteRequest(
			routing.FindEndpoint(HttpMethod.Get, "/four/three").Endpoint,
			RequestHelper.Request());

		Assert.Equal("First", await ResponseHelper.Body(secondResponse));
	}

	private static void TestRouting(RouteConfiguration routeConfiguration)
	{
		routeConfiguration.Get("/", () => Response.Ok(o => o.Write("GetRoot")));
		routeConfiguration.Get("/{Greedy*}", () => Response.Ok(o => o.Write("GetGreedyCapture")));

		routeConfiguration.Get(
			"/literal/{Greedy*}",
			() => Response.Ok(o => o.Write("GetSingleLiteralAndGreedyCapture")));

		routeConfiguration.Get("/{Capture}", () => Response.Ok(o => o.Write("GetSingleCapture")));
		routeConfiguration.Get("/{Multiple}/{Captures}", () => Response.Ok(o => o.Write("GetMultipleCaptures")));
		routeConfiguration.Get("/single-literal", () => Response.Ok(o => o.Write("GetSingleLiteral")));
		routeConfiguration.Get("/multiple/literals", () => Response.Ok(o => o.Write("GetMultipleLiterals")));

		routeConfiguration.Get(
			"/multiple/literals/{Greedy*}",
			() => Response.Ok(o => o.Write("GetMultipleLiteralsAndGreedyCapture")));

		routeConfiguration.Get(
			"/multiple/{CaptureOne}/literals/{CaptureTwo}/{Greedy*}",
			() => Response.Ok(o => o.Write("GetMultipleLiteralsMultipleCapturesAndGreedyCapture")));

		routeConfiguration.Post("/duplicate", () => Response.Ok(o => o.Write("PostDuplicate")));
		routeConfiguration.Delete("/duplicate", () => Response.Ok(o => o.Write("DeleteDuplicate")));
	}
}
