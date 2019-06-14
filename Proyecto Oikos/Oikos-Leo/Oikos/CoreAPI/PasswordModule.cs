using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;


namespace CoreAPI {
    public class PasswordModule {
        private static readonly Random Random = new Random();

        public string EncryptPassword(string pPassword) {

            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(pPassword));
                return Convert.ToBase64String(data);
            }
        }

        public string PasswordGenerator(){
            int passwordLength = 8;
            int seed = Random.Next(1, int.MaxValue);
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            const string specialCharacters = @"!#$%&'()*+,-./:;<=>?@[\]_";

            var chars = new char[passwordLength];
            var rd = new Random(seed);

            for (var i = 0; i < passwordLength; i++) {
                // If we are to use special characters
                if (i % Random.Next(3, passwordLength) == 0) {
                    chars[i] = specialCharacters[rd.Next(0, specialCharacters.Length)];
                }
                else {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
            }

            return new string(chars);
        }

        
    }
}
