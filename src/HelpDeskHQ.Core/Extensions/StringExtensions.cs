using System.Security.Cryptography;

namespace HelpDeskHQ.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ComputeMD5(this string str)
        {
            var md5 = MD5.Create();
            var bytes = System.Text.Encoding.ASCII.GetBytes(str);
            var hash = md5.ComputeHash(bytes);
            var base64 = Convert.ToBase64String(hash);
            return base64;
        }

        public static string ComputeHash(this string str)
        {
            return str.ComputeMD5();
        }
    }
}
