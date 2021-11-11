using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Mundane;

/// <summary>Represents a validated value.</summary>
public abstract class Validated
{
	private readonly List<string> mutableErrorMessages;

	internal Validated()
	{
		this.mutableErrorMessages = new List<string>();
		this.ErrorMessages = new ReadOnlyCollection<string>(this.mutableErrorMessages);
	}

	/// <summary>Gets the collection of error messages.</summary>
	public ReadOnlyCollection<string> ErrorMessages { get; }

	/// <summary>Gets a value indicating whether the validation failed.</summary>
	public bool Failed
	{
		get
		{
			return this.ErrorMessages.Count > 0;
		}
	}

	/// <summary>Adds an error message.</summary>
	/// <param name="errorMessage">The error message to add.</param>
	/// <exception cref="ArgumentNullException"><paramref name="errorMessage"/> is <see langword="null"/>.</exception>
	/// <exception cref="ArgumentException"><paramref name="errorMessage"/> does not have a value.</exception>
	public void AddErrorMessage(string errorMessage)
	{
		if (errorMessage is null)
		{
			throw new ArgumentNullException(nameof(errorMessage));
		}

		if (errorMessage.AsSpan().Trim().IsEmpty)
		{
			throw new ArgumentException("Error message must not be empty.", nameof(errorMessage));
		}

		this.mutableErrorMessages.Add(errorMessage);
	}
}
