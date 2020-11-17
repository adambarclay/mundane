using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Mundane
{
	/// <summary>Represents an uploaded file.</summary>
	public abstract class FileUpload
	{
		/// <summary>Gets the value returned when no file upload can be found.</summary>
		[NotNull]
		public static FileUpload Unknown { get; } = new UnknownFileUpload();

		/// <summary>Gets the file name of the uploaded file.</summary>
		[NotNull]
		public abstract string FileName { get; }

		/// <summary>Gets the length of the uploaded file in bytes.</summary>
		public abstract long Length { get; }

		/// <summary>Gets the media type of the uploaded file.</summary>
		[NotNull]
		public abstract string MediaType { get; }

		/// <summary>Opens the uploaded file for reading.</summary>
		/// <returns>A read-only stream.</returns>
		[return: NotNull]
		public abstract Stream Open();

		private sealed class UnknownFileUpload : FileUpload
		{
			public override string FileName
			{
				get
				{
					return string.Empty;
				}
			}

			public override long Length
			{
				get
				{
					return 0;
				}
			}

			public override string MediaType
			{
				get
				{
					return string.Empty;
				}
			}

			public override Stream Open()
			{
				return new MemoryStream(Array.Empty<byte>(), false);
			}
		}
	}
}
