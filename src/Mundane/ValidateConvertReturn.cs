namespace Mundane;

/// <summary>Converts and returns the value.</summary>
/// <param name="value">The value to convert.</param>
/// <typeparam name="T">The type to which the value is converted.</typeparam>
public delegate T ValidateConvertReturn<out T>(string value);
