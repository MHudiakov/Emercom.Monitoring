using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Ble.Common
{
    public static class CryptographyHelper
    {
        private static string _salt = "fby9u2Ch7CGd1FVFjHA395y46UJiBjoap3I9m8";
        
        public static string GetSha256Hash(string value)
        {
            using (var sha256 = SHA256.Create())
            {
                return string.Concat(sha256
                    .ComputeHash(Encoding.UTF8.GetBytes(string.Concat(value, _salt)))
                    .Select(item => item.ToString("x2")));
            }
        }
    }
}