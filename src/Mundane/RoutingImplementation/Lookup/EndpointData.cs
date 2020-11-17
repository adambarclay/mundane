namespace Mundane.RoutingImplementation.Lookup
{
	internal readonly struct EndpointData
	{
		internal readonly bool CaptureTrailingSlash;
		internal readonly MundaneEndpointDelegate EndpointDelegate;
		internal readonly RouteSegment[] NodesInRoute;
		internal readonly int NumberOfCaptureParameters;
		internal readonly Score Score;

		internal EndpointData(
			in Score score,
			MundaneEndpointDelegate endpointDelegate,
			int numberOfCaptureParameters,
			bool captureTrailingSlash,
			RouteSegment[] nodesInRoute)
		{
			this.Score = score;
			this.EndpointDelegate = endpointDelegate;
			this.NumberOfCaptureParameters = numberOfCaptureParameters;
			this.NodesInRoute = nodesInRoute;
			this.CaptureTrailingSlash = captureTrailingSlash;
		}
	}
}
