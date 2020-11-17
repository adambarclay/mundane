namespace Mundane.RoutingImplementation.Lookup
{
	internal readonly struct Score
	{
		internal readonly ushort Capture;
		internal readonly ushort Greedy;
		internal readonly ushort Literal;
		internal readonly ushort Order;

		internal Score(ushort capture, ushort greedy, ushort literal, ushort order)
		{
			this.Literal = literal;
			this.Capture = capture;
			this.Greedy = greedy;
			this.Order = order;
		}
	}
}
