namespace Mundane.RoutingImplementation.Lookup
{
	internal readonly struct EndpointData
	{
		internal readonly bool CaptureTrailingSlash;
		internal readonly MundaneEndpoint Endpoint;
		internal readonly RouteSegment[] NodesInRoute;
		internal readonly int NumberOfCaptureParameters;
		internal readonly Score Score;

		internal EndpointData(
			in Score score,
			MundaneEndpoint endpoint,
			int numberOfCaptureParameters,
			bool captureTrailingSlash,
			RouteSegment[] nodesInRoute)
		{
			this.Score = score;
			this.Endpoint = endpoint;
			this.NumberOfCaptureParameters = numberOfCaptureParameters;
			this.NodesInRoute = nodesInRoute;
			this.CaptureTrailingSlash = captureTrailingSlash;
		}
	}
}
