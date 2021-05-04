using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Moq;

namespace Mundane.Tests
{
	[ExcludeFromCodeCoverage]
	internal static class RequestHelper
	{
		internal static Request Request()
		{
			var mock = new Mock<Request>(MockBehavior.Strict);

			mock.Setup(o => o.Method)!.Returns(HttpMethod.Get);
			mock.Setup(o => o.Path)!.Returns("/");
			mock.Setup(o => o.RequestAborted)!.Returns(CancellationToken.None);
			mock.Setup(o => o.Header(It.IsAny<string>()!))!.Returns(string.Empty);

			return mock.Object!;
		}

		internal static Request Request(string method, string path)
		{
			var mock = new Mock<Request>(MockBehavior.Strict);

			mock.Setup(o => o.Method)!.Returns(method);
			mock.Setup(o => o.Path)!.Returns(path);
			mock.Setup(o => o.RequestAborted)!.Returns(CancellationToken.None);

			return mock.Object!;
		}

		internal static Request Request(string method, string path, RouteParameters routeParameters)
		{
			var mock = new Mock<Request>(MockBehavior.Strict);

			mock.Setup(o => o.Method)!.Returns(method);
			mock.Setup(o => o.Path)!.Returns(path);
			mock.Setup(o => o.RequestAborted)!.Returns(CancellationToken.None);

			mock.Setup(o => o.Route(It.IsAny<string>()!))!.Returns(
				(string parameterName) =>
				{
					routeParameters.TryGetValue(parameterName, out var value);

					return value ?? string.Empty;
				});

			return mock.Object!;
		}

		internal static Request Request(
			string method,
			string path,
			Dictionary<string, string> route,
			Dictionary<string, string> headers)
		{
			var mock = new Mock<Request>(MockBehavior.Strict);

			mock.Setup(o => o.Method)!.Returns(method);
			mock.Setup(o => o.Path)!.Returns(path);
			mock.Setup(o => o.RequestAborted)!.Returns(CancellationToken.None);

			mock.Setup(o => o.Route(It.IsAny<string>()!))!.Returns(
				(string parameterName) =>
				{
					route.TryGetValue(parameterName, out var value);

					return value ?? string.Empty;
				});

			mock.Setup(o => o.Header(It.IsAny<string>()!))!.Returns((string parameterName) => headers[parameterName]);

			return mock.Object!;
		}

		internal static Request Request(
			string method,
			string path,
			Dictionary<string, string> route,
			Dictionary<string, string> headers,
			Stream body)
		{
			var mock = new Mock<Request>(MockBehavior.Strict);

			mock.Setup(o => o.Method)!.Returns(method);
			mock.Setup(o => o.Path)!.Returns(path);
			mock.Setup(o => o.RequestAborted)!.Returns(CancellationToken.None);

			mock.Setup(o => o.Route(It.IsAny<string>()!))!.Returns(
				(string parameterName) =>
				{
					route.TryGetValue(parameterName, out var value);

					return value ?? string.Empty;
				});

			mock.Setup(o => o.Header(It.IsAny<string>()!))!.Returns((string parameterName) => headers[parameterName]);
			mock.Setup(o => o.Body)!.Returns(body);

			return mock.Object!;
		}
	}
}
