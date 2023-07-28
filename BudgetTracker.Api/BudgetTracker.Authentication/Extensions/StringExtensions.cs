namespace BudgetTracker.Authentication.Extensions
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	public static class StringExtensions
	{
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