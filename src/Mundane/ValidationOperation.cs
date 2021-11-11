namespace Mundane;

/// <summary>The validation to perform, typically constructing a validated object.</summary>
/// <param name="validator">Manages the validation of input values.</param>
/// <typeparam name="T">The type of object being validated.</typeparam>
public delegate T ValidationOperation<out T>(Validator validator);
