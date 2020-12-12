using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Mundane.Tests.Tests_Routing
{
	[ExcludeFromCodeCoverage]
	public static class Constructor_Throws_ArgumentException
	{
		[Fact]
		public static void When_A_Duplicate_Route_Is_Added()
		{
			const string routeOne = "/{captureOne}/literal/{greedy*}";
			const string routeTwo = "/{captureTwo}/literal/{greedy*}";

			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(
					o =>
					{
						o.Get(routeOne, Response.Ok);
						o.Get(routeTwo, Response.Ok);
					}));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				$"The endpoint for route \"{routeTwo}\" will never be reached because a similar route \"{routeOne}\" was already defined.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_A_Capture_Segment_With_No_Name()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get("/{}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{}\" is not valid. Capture segments require a name.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_A_Capture_Segment_With_The_Same_Name_As_A_Greedy_Segment()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/{capture}/{capture*}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{capture*}\" is not valid. Capture and greedy segments must have unique names, and \"capture\" has already been used.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_A_Close_Brace_That_Is_Not_The_Last_Character()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/inva}lid", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"inva}lid\" is not valid. The \"}\" character can only appear at the end of a capture segment e.g. {capture} or {greedy*}.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_A_Greedy_Capture_Segment_With_No_Name()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get("/{*}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{*}\" is not valid. Capture segments require a name.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_A_Greedy_Segment_Which_Is_Not_The_Last_Part_Of_The_Route()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/{greedy*}/{capture}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{capture}\" is not valid. A greedy segment has previously been used. Greedy segments may only appear once, at the end of the route.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_A_Literal_Segment_With_No_Name()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(() => new Routing(o => o.Get("//", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"\" is not valid. Capture segments require a name.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_An_Asterisk_That_Is_Not_Second_Last_Character()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/inva*lid", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"inva*lid\" is not valid. The \"*\" character can only appear at the end of a greedy capture segment e.g. {greedy*}.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_An_Open_Brace_That_Is_Not_The_First_Character()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/inva{lid", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"inva{lid\" is not valid. The \"{\" character can only appear at the start of a capture segment e.g. {capture} or {greedy*}.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_An_Unclosed_Capture_Segment()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/{captureOne", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{captureOne\" is not valid. A capture segment has been opened (with \"{\") but not closed (with \"}\").",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_An_Unclosed_Greedy_Segment()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/{captureOne*", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{captureOne*\" is not valid. A capture segment has been opened (with \"{\") but not closed (with \"}\").",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_An_Unopened_Capture_Segment()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/captureOne}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"captureOne}\" is not valid. The \"}\" character can only appear at the end of a capture segment e.g. {capture} or {greedy*}.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_An_Unopened_Greedy_Segment()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/captureOne*}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"captureOne*}\" is not valid. The \"}\" character can only appear at the end of a capture segment e.g. {capture} or {greedy*}.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_More_Than_One_Greedy_Segment()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/{greedyOne*}/{greedyTwo*}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{greedyTwo*}\" is not valid. A greedy segment has previously been used. Greedy segments may only appear once, at the end of the route.",
				exception.Message,
				StringComparison.Ordinal);
		}

		[Fact]
		public static void When_A_Route_Contains_Multiple_Capture_Segments_With_The_Same_Name()
		{
			var exception = Assert.ThrowsAny<ArgumentException>(
				() => new Routing(o => o.Get("/{blah}/{blah}", Response.Ok)));

			Assert.Equal("route", exception.ParamName!);

			Assert.StartsWith(
				"The route segment \"{blah}\" is not valid. Capture and greedy segments must have unique names, and \"blah\" has already been used.",
				exception.Message,
				StringComparison.Ordinal);
		}
	}
}
