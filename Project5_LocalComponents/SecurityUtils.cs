using System;
using System.Security.Cryptography;
using System.Text;

namespace Project5_LocalComponents
{
    public class SecurityUtils
    {
        public static string ComputeSha256(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}