using System;

namespace Mundane.RoutingImplementation.Lookup
{
	internal ref struct PathSegments
	{
		private readonly ReadOnlySpan<char> route;
		private int startIndex;

		internal PathSegments(ReadOnlySpan<char> route)
		{
			this.route = route;
			this.startIndex = 0;
		}

		internal readonly bool MoreSegments
		{
			get
			{
				return this.startIndex < this.route.Length;
			}
		}

		internal readonly int NumberOfSegments
		{
			get
			{
				var count = 0;

				foreach (var character in this.route)
				{
					if (character == '/')
					{
						++count;
					}
				}

				return count;
			}
		}

		internal readonly ReadOnlySpan<char> AllRemaining(bool captureTrailingSlash)
		{
			return captureTrailingSlash ? this.route[(this.startIndex + 1)..] : this.route[(this.startIndex + 1)..^1];
		}

		internal ReadOnlySpan<char> Next()
		{
			var routeSegment = this.route[(this.startIndex + 1)..];

			var endIndex = routeSegment.IndexOf('/');

			if (endIndex >= 0)
			{
				routeSegment = routeSegment[..endIndex];
			}

			this.startIndex += routeSegment.Length + 1;

			return routeSegment;
		}
	}
}
