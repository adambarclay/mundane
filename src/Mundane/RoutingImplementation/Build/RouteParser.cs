using System;
using System.Collections.Generic;
using System.Diagnostics;
using Mundane.RoutingImplementation.Lookup;

namespace Mundane.RoutingImplementation.Build;

internal static class RouteParser
{
	internal static RouteSegment[] ParseRoute(string route)
	{
		Debug.Assert(route[0] == '/');

		if (route.Length == 1)
		{
			return Array.Empty<RouteSegment>();
		}

		var captureSegmentNames = new HashSet<string>();
		var pathSegments = new PathSegments(route);

		var routeSegments = new RouteSegment[pathSegments.NumberOfSegments];
		var routeSegmentCount = 0;

		var usedGreedy = false;

		while (pathSegments.MoreSegments)
		{
			var pathSegment = pathSegments.Next();

			try
			{
				if (!pathSegment.IsEmpty || pathSegments.MoreSegments)
				{
					if (usedGreedy)
					{
						throw new ArgumentException(
							"A greedy segment has previously been used. Greedy segments may only appear once, at the end of the route.");
					}

					var segment = RouteParser.ParseRouteSegment(pathSegment, captureSegmentNames);

					if (segment.Type == NodeType.Greedy)
					{
						usedGreedy = true;
					}

					routeSegments[routeSegmentCount++] = segment;
				}
				else
				{
					routeSegments[routeSegmentCount++] = new RouteSegment(NodeType.Literal, string.Empty);
				}
			}
			catch (ArgumentException exception)
			{
				throw new ArgumentException(
					$"The route segment \"{new string(pathSegment)}\" is not valid. {exception.Message}",
					nameof(route));
			}
		}

		return routeSegments;
	}

	private static RouteSegment ParseRouteSegment(
		ReadOnlySpan<char> currentRouteSegment,
		HashSet<string> captureSegmentNames)
	{
		NodeType nodeType;
		ReadOnlySpan<char> routeToken;

		if (!currentRouteSegment.IsEmpty && currentRouteSegment[0] == '{')
		{
			if (currentRouteSegment[^1] == '}')
			{
				if (currentRouteSegment.Length > 2 && currentRouteSegment[^2] == '*')
				{
					nodeType = NodeType.Greedy;
					routeToken = currentRouteSegment[1..^2];
				}
				else
				{
					nodeType = NodeType.Capture;
					routeToken = currentRouteSegment[1..^1];
				}
			}
			else
			{
				throw new ArgumentException(
					"A capture segment has been opened (with \"{\") but not closed (with \"}\").");
			}
		}
		else
		{
			nodeType = NodeType.Literal;
			routeToken = currentRouteSegment;
		}

		if (routeToken.Trim().IsEmpty)
		{
			throw new ArgumentException("Capture segments require a name.");
		}

		if (routeToken.IndexOf('{') >= 0)
		{
			throw new ArgumentException(
				"The \"{\" character can only appear at the start of a capture segment e.g. {capture} or {greedy*}.");
		}

		if (routeToken.IndexOf('}') >= 0)
		{
			throw new ArgumentException(
				"The \"}\" character can only appear at the end of a capture segment e.g. {capture} or {greedy*}.");
		}

		if (routeToken.IndexOf('*') >= 0)
		{
			throw new ArgumentException(
				"The \"*\" character can only appear at the end of a greedy capture segment e.g. {greedy*}.");
		}

		var routeTokenString = new string(routeToken);

		if (nodeType is NodeType.Capture or NodeType.Greedy)
		{
			if (captureSegmentNames.Contains(routeTokenString))
			{
				throw new ArgumentException(
					$"Capture and greedy segments must have unique names, and \"{routeTokenString}\" has already been used.");
			}

			captureSegmentNames.Add(routeTokenString);
		}

		return new RouteSegment(nodeType, routeTokenString);
	}
}
