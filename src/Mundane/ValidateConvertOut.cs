namespace Mundane
{
	/// <summary>Converts and returns the value as an out parameter.</summary>
	/// <param name="value">The value to convert.</param>
	/// <param name="convertedValue">The converted value.</param>
	/// <typeparam name="T">The type to which the value is converted.</typeparam>
	public delegate bool ValidateConvertOut<T>(string value, out T convertedValue);
}
