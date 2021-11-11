namespace Mundane.RoutingImplementation.Lookup;

internal readonly struct RouteNode
{
	internal readonly int[] Children;
	internal readonly int Endpoint;
	internal readonly bool HasEndpoint;
	internal readonly NodeType Type;
	internal readonly string Value;

	internal RouteNode(in RouteSegment routeSegment, int[] children, bool hasEndpoint, int endpoint)
	{
		this.Type = routeSegment.Type;
		this.Value = routeSegment.Value;
		this.Children = children;
		this.HasEndpoint = hasEndpoint;
		this.Endpoint = endpoint;
	}
}
