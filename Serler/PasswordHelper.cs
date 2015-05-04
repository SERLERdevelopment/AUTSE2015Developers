using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serler.Authentications
{
    public static class PasswordHelper
    {

        /// <summary>
        /// Gets a new password hash string
        /// </summary>
        /// <param name="password"></param>
        /// <returns>128 character hex string containing a salt and hash of the password</returns>
        public static string GetNewPasswordHash(string password)
        {
            var salt = GetRandomSalt();
            var hash = GetHash(salt + password);
            return salt + hash;
        }

        /// <summary>
        /// Validates a password against the expected passwordHash
        /// </summary>
        /// <param name="password">The password to test</param>
        /// <param name="correctPasswordHash">The hash of the correct password</param>
        /// <returns>True if password is the correct password, false otherwise</returns>
        public static bool ValidatePassword(string password, string correctPasswordHash)
        {
            if (correctPasswordHash.Length < 128)
            {
                throw new ArgumentException("correctHash must be 128 hex characters!");
            }

            var salt = correctPasswordHash.Substring(0, 64);
            var validHash = correctPasswordHash.Substring(64, 64);
            var passHash = GetHash(salt + password);

            return string.Compare(validHash, passHash) == 0;
        }

        /// <summary>
        /// Gets a hash of the input string (using SHA256)
        /// </summary>
        /// <param name="toHash"></param>
        /// <returns>64 character hex string (256 bits)</returns>
        private static string GetHash(string input)
        {
            var hasher = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(input);

            return GetHex(hasher.ComputeHash(bytes));
        }

        /// <summary>
        /// Get a new random salt
        /// </summary>
        /// <returns>64 character hex string (256 bits)</returns>
        private static string GetRandomSalt()
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new byte[32]; //256 bits
            random.GetBytes(salt);

            return GetHex(salt);
        }

        /// <summary>
        /// Get a hex string from a byte array
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns>64 character hex string (256 bits)</returns>
        private static string GetHex(byte[] bytes)
        {
            var s = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }
    }
}
