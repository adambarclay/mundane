namespace Mundane.RoutingImplementation.Lookup
{
	internal readonly struct RouteSegment
	{
		internal readonly NodeType Type;
		internal readonly string Value;

		internal RouteSegment(NodeType type, string value)
		{
			this.Type = type;
			this.Value = value;
		}
	}
}
