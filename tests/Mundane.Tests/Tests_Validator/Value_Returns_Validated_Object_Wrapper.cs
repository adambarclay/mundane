using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using Xunit;

namespace Mundane.Tests.Tests_Validator
{
	[ExcludeFromCodeCoverage]
	public static class Value_Returns_Validated_Object_Wrapper
	{
		[Fact]
		public static void Equivalent_To_The_Fallback_Value_When_The_Convert_Out_Delegate_Fails()
		{
			var fallback = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var errorMessage = Guid.NewGuid().ToString();

			(var invalid, var validatedValue) = Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString(), int.TryParse, fallback, errorMessage));

			Assert.True(invalid);
			Assert.NotNull(validatedValue);
			Assert.IsAssignableFrom<Validated<int>>(validatedValue);
			Assert.Equal(fallback, validatedValue);
			Assert.Single(validatedValue.ErrorMessages);
			Assert.Equal(errorMessage, validatedValue.ErrorMessages[0]);
		}

		[Fact]
		public static void Equivalent_To_The_Fallback_Value_When_The_Convert_Return_Delegate_Fails()
		{
			var fallback = RandomNumberGenerator.GetInt32(0, int.MaxValue);
			var errorMessage = Guid.NewGuid().ToString();

			(var invalid, var validatedValue) = Validator.Validate(
				validator => validator.Value(Guid.NewGuid().ToString(), int.Parse, fallback, errorMessage));

			Assert.True(invalid);
			Assert.NotNull(validatedValue);
			Assert.IsAssignableFrom<Validated<int>>(validatedValue);
			Assert.Equal(fallback, validatedValue);
			Assert.Single(validatedValue.ErrorMessages);
			Assert.Equal(errorMessage, validatedValue.ErrorMessages[0]);
		}

		[Fact]
		public static void Equivalent_To_The_Original_Value()
		{
			var value = Guid.NewGuid().ToString();

			(var invalid, var validatedValue) = Validator.Validate(validator => validator.Value(value));

			Assert.False(invalid);
			Assert.NotNull(validatedValue);
			Assert.IsAssignableFrom<Validated<string>>(validatedValue);
			Assert.Equal(value, validatedValue);
			Assert.Empty(validatedValue.ErrorMessages);
		}

		[Fact]
		public static void Equivalent_To_The_Original_Value_When_The_Convert_Out_Delegate_Succeeds()
		{
			var value = RandomNumberGenerator.GetInt32(0, int.MaxValue);

			(var invalid, var validatedValue) = Validator.Validate(
				validator => validator.Value(value.ToString(CultureInfo.InvariantCulture), int.TryParse, 0, "Error"));

			Assert.False(invalid);
			Assert.NotNull(validatedValue);
			Assert.IsAssignableFrom<Validated<int>>(validatedValue);
			Assert.Equal(value, validatedValue);
			Assert.Empty(validatedValue.ErrorMessages);
		}

		[Fact]
		public static void Equivalent_To_The_Original_Value_When_The_Convert_Return_Delegate_Succeeds()
		{
			var value = RandomNumberGenerator.GetInt32(0, int.MaxValue);

			(var invalid, var validatedValue) = Validator.Validate(
				validator => validator.Value(value.ToString(CultureInfo.InvariantCulture), int.Parse, 0, "Error"));

			Assert.False(invalid);
			Assert.NotNull(validatedValue);
			Assert.IsAssignableFrom<Validated<int>>(validatedValue);
			Assert.Equal(value, validatedValue);
			Assert.Empty(validatedValue.ErrorMessages);
		}
	}
}
