using System;

namespace Mundane
{
	/// <summary>The exception thrown when the requested route parameter is not one of the route parameters configured for the route.</summary>
	public sealed class RouteParameterNotFound : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="RouteParameterNotFound"/> class.</summary>
		/// <param name="parameterName">The name of the route parameter.</param>
		public RouteParameterNotFound(string parameterName)
			: base(RouteParameterNotFound.CreateMessage(parameterName))
		{
		}

		private static string CreateMessage(string parameterName)
		{
			if (parameterName is null)
			{
				throw new ArgumentNullException(nameof(parameterName));
			}

			return $"The route configuration does not contain a parameter called \"{parameterName}\".";
		}
	}
}
