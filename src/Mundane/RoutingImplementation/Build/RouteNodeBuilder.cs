using System.Collections.Generic;
using Mundane.RoutingImplementation.Lookup;

namespace Mundane.RoutingImplementation.Build;

internal sealed class RouteNodeBuilder
{
	internal RouteNodeBuilder(in RouteSegment routeSegment)
	{
		this.RouteSegment = routeSegment;
		this.Children = new List<RouteNodeBuilder>(16);
		this.OriginalRoute = string.Empty;
	}

	internal List<RouteNodeBuilder> Children { get; }

	internal EndpointData EndpointData { get; private set; }

	internal int EndpointIndex { get; set; }

	internal bool HasEndpoint { get; private set; }

	internal int Index { get; set; }

	internal string OriginalRoute { get; private set; }

	internal RouteSegment RouteSegment { get; }

	internal RouteNodeBuilder AddChild(in RouteSegment routeSegment)
	{
		var node = new RouteNodeBuilder(routeSegment);

		this.Children.Add(node);

		return node;
	}

	internal RouteNodeBuilder? FindChildNode(in RouteSegment routeSegment)
	{
		foreach (var childNode in this.Children)
		{
			if (childNode.RouteSegment.Type == routeSegment.Type &&
				(routeSegment.Type != NodeType.Literal || childNode.RouteSegment.Value == routeSegment.Value))
			{
				return childNode;
			}
		}

		return null;
	}

	internal void SetEndpoint(
		string originalRoute,
		in Score score,
		MundaneEndpoint endpoint,
		int numberOfCaptureParameters,
		bool captureTrailingSlash,
		RouteSegment[] nodesInRoute)
	{
		this.OriginalRoute = originalRoute;

		this.EndpointData = new EndpointData(
			in score,
			endpoint,
			numberOfCaptureParameters,
			captureTrailingSlash,
			nodesInRoute);

		this.HasEndpoint = true;
	}
}
