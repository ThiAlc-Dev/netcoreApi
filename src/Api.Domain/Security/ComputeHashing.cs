using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Api.Domain.Security
{
    public class ComputeHashing
    {
        public static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        } 

        public static bool VerifyHash(string password, string bdHash)
        {
            var newHash = ComputeSha256Hash(password);
            return bdHash == newHash;
        }
    }
}