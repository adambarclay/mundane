using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;

namespace Mundane.Tests
{
	[ExcludeFromCodeCoverage]
	internal static class RequestHelper
	{
		internal static Request Request()
		{
			return new Request(
				HttpMethod.Get,
				"/",
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new MemoryStream(Array.Empty<byte>(), false),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, FileUpload>(0),
				new Dependencies(),
				new RequestHost(string.Empty, string.Empty, string.Empty),
				CancellationToken.None);
		}

		internal static Request Request(string method, string path)
		{
			return new Request(
				method,
				path,
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new MemoryStream(Array.Empty<byte>(), false),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, FileUpload>(0),
				new Dependencies(),
				new RequestHost(string.Empty, string.Empty, string.Empty),
				CancellationToken.None);
		}

		internal static Request Request(string method, string path, EnumerableDictionary<string, string> route)
		{
			return new Request(
				method,
				path,
				route,
				new Dictionary<string, string>(0),
				new MemoryStream(Array.Empty<byte>(), false),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, FileUpload>(0),
				new Dependencies(),
				new RequestHost(string.Empty, string.Empty, string.Empty),
				CancellationToken.None);
		}

		internal static Request Request(
			string method,
			string path,
			EnumerableDictionary<string, string> route,
			EnumerableDictionary<string, string> headers)
		{
			return new Request(
				method,
				path,
				route,
				headers,
				new MemoryStream(Array.Empty<byte>(), false),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, FileUpload>(0),
				new Dependencies(),
				new RequestHost(string.Empty, string.Empty, string.Empty),
				CancellationToken.None);
		}

		internal static Request Request(
			string method,
			string path,
			EnumerableDictionary<string, string> route,
			EnumerableDictionary<string, string> headers,
			Stream body)
		{
			return new Request(
				method,
				path,
				route,
				headers,
				body,
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, FileUpload>(0),
				new Dependencies(),
				new RequestHost(string.Empty, string.Empty, string.Empty),
				CancellationToken.None);
		}

		internal static Request Request(DependencyFinder dependencyFinder)
		{
			return new Request(
				HttpMethod.Get,
				"/",
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new MemoryStream(Array.Empty<byte>(), false),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, string>(0),
				new Dictionary<string, FileUpload>(0),
				dependencyFinder,
				new RequestHost(string.Empty, string.Empty, string.Empty),
				CancellationToken.None);
		}
	}
}
