namespace Mundane
{
	internal sealed class ValidatedValue<T> : Validated<T>
		where T : notnull
	{
		internal ValidatedValue(string stringValue, T value)
			: base(stringValue)
		{
			this.Value = value;
		}

		protected override T Value { get; }
	}
}
