using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Mundane.Tests.Tests_Routing;

[ExcludeFromCodeCoverage]
public static class FindEndpoint_Captures_The_Correct_Route_Parameters
{
	[Fact]
	public static async Task When_The_Route_Has_A_Greedy_Capture()
	{
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{Greedy*}",
				request =>
				{
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one/two/three";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one/two/three", greedy);
	}

	[Fact]
	public static async Task
		When_The_Route_Has_A_Greedy_Capture_With_A_Trailing_Slash_And_The_Path_Has_A_Trailing_Slash()
	{
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{Greedy*}/",
				request =>
				{
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one/two/three/";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one/two/three", greedy);
	}

	[Fact]
	public static async Task
		When_The_Route_Has_A_Greedy_Capture_Without_A_Trailing_Slash_And_The_Path_Has_A_Trailing_Slash()
	{
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{Greedy*}",
				request =>
				{
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one/two/three/";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one/two/three/", greedy);
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Capture()
	{
		var capture = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{Capture}",
				request =>
				{
					capture = request.Route("Capture");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one", capture);
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Capture_And_A_Greedy_Capture()
	{
		var capture = string.Empty;
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{Capture}/{Greedy*}",
				request =>
				{
					capture = request.Route("Capture");
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one/two/three/four";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one", capture);
		Assert.Equal("two/three/four", greedy);
	}

	[Fact]
	public static async Task When_The_Route_Has_A_Single_Literal_And_A_Greedy_Capture()
	{
		var capture = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/literal/{Capture}",
				request =>
				{
					capture = request.Route("Capture");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/literal/one";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one", capture);
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Captures()
	{
		var captureOne = string.Empty;
		var captureTwo = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{CaptureOne}/{CaptureTwo}",
				request =>
				{
					captureOne = request.Route("CaptureOne");
					captureTwo = request.Route("CaptureTwo");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one/two";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one", captureOne);
		Assert.Equal("two", captureTwo);
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Captures_And_A_Greedy_Capture()
	{
		var captureOne = string.Empty;
		var captureTwo = string.Empty;
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/{CaptureOne}/{CaptureTwo}/{Greedy*}",
				request =>
				{
					captureOne = request.Route("CaptureOne");
					captureTwo = request.Route("CaptureTwo");
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/one/two/three/four";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one", captureOne);
		Assert.Equal("two", captureTwo);
		Assert.Equal("three/four", greedy);
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Literals_And_A_Greedy_Capture()
	{
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/multiple/literals/{Greedy*}",
				request =>
				{
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/multiple/literals/one/two";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one/two", greedy);
	}

	[Fact]
	public static async Task When_The_Route_Has_Multiple_Literals_Multiple_Captures_And_A_Greedy_Capture()
	{
		var captureOne = string.Empty;
		var captureTwo = string.Empty;
		var greedy = string.Empty;

		var routing = new Routing(
			o => o.Get(
				"/multiple/{CaptureOne}/literals/{CaptureTwo}/{Greedy*}",
				request =>
				{
					captureOne = request.Route("CaptureOne");
					captureTwo = request.Route("CaptureTwo");
					greedy = request.Route("Greedy");

					return Response.Ok();
				}));

		const string method = HttpMethod.Get;
		const string path = "/multiple/one/literals/two/three/four";

		(var endpoint, var routeParameters) = routing.FindEndpoint(method, path);

		await MundaneEngine.ExecuteRequest(endpoint, RequestHelper.Request(method, path, routeParameters));

		Assert.Equal("one", captureOne);
		Assert.Equal("two", captureTwo);
		Assert.Equal("three/four", greedy);
	}
}
