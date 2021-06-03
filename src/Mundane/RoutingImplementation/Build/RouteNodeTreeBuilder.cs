using System;
using System.Collections.Generic;
using Mundane.RoutingImplementation.Lookup;

namespace Mundane.RoutingImplementation.Build
{
	internal sealed class RouteNodeTreeBuilder
	{
		private readonly RouteNodeBuilder root;
		private int numberOfNodes;

		internal RouteNodeTreeBuilder()
		{
			this.root = new RouteNodeBuilder(new RouteSegment(NodeType.Literal, string.Empty));
			this.numberOfNodes = 1;
		}

		internal int NumberOfEndpoints { get; private set; }

		internal void AddRoute(string route, MundaneEndpoint endpoint)
		{
			++this.NumberOfEndpoints;

			var node = this.root;

			var captureParameters = 0;

			var capture = (ushort)0;
			var greedy = (ushort)0;
			var literal = (ushort)0;

			var routeSegments = RouteParser.ParseRoute(route);

			if (route.Length > 1)
			{
				foreach (var routeSegment in routeSegments)
				{
					if (routeSegment.Type == NodeType.Capture)
					{
						++captureParameters;
						++capture;
					}
					else if (routeSegment.Type == NodeType.Greedy)
					{
						++captureParameters;
						++greedy;
					}
					else
					{
						++literal;
					}

					var childNode = node.FindChildNode(in routeSegment);

					if (childNode is null)
					{
						childNode = node.AddChild(in routeSegment);

						++this.numberOfNodes;
					}

					node = childNode;
				}
			}

			if (node.HasEndpoint)
			{
				throw new ArgumentException(
					$"The endpoint for route \"{route}\" will never be reached because a similar route \"{node.OriginalRoute}\" was already defined.",
					nameof(route));
			}

			node.SetEndpoint(
				route,
				new Score(capture, greedy, literal, (ushort)this.NumberOfEndpoints),
				endpoint,
				captureParameters,
				greedy == 0 || route[^1] != '/',
				routeSegments);
		}

		internal RouteNode[] Build(EndpointData[] endpoints, ref int endpointCount)
		{
			var nodeQueue = new Queue<RouteNodeBuilder>(this.numberOfNodes);
			var nodeList = new List<RouteNodeBuilder>(this.numberOfNodes);

			nodeQueue.Enqueue(this.root);

			while (nodeQueue.Count > 0)
			{
				var node = nodeQueue.Dequeue();

				foreach (var child in node.Children)
				{
					nodeQueue.Enqueue(child);
				}

				node.Index = nodeList.Count;

				nodeList.Add(node);

				if (node.HasEndpoint)
				{
					node.EndpointIndex = endpointCount;

					endpoints[endpointCount++] = node.EndpointData;
				}
			}

			var nodes = new RouteNode[this.numberOfNodes];
			var nodeCount = 0;

			foreach (var node in nodeList)
			{
				var children = new int[node.Children.Count];
				var childrenCount = 0;

				foreach (var child in node.Children)
				{
					children[childrenCount++] = child.Index;
				}

				nodes[nodeCount++] = new RouteNode(node.RouteSegment, children, node.HasEndpoint, node.EndpointIndex);
			}

			return nodes;
		}
	}
}
