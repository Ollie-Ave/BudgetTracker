namespace BudgetTracker.Authentication.Extensions
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

    /// <summary>A string extensions.</summary>
	public static class StringExtensions
	{
        /// <summary>A string extension method that converts a value to a sha 256.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="value">The value to act on.</param>
        /// <returns>Value as a string.</returns>
		public static string ToSha256(this string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException(nameof(value));
			}

			byte[] encodedValue = Encoding.UTF8.GetBytes(value);
			byte[] hash;

			using (SHA256 hashAlgorithm = SHA256.Create())
			{
				hash = hashAlgorithm.ComputeHash(encodedValue);
			}

			return Convert.ToBase64String(hash);
		}
	}
}