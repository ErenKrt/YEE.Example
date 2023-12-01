using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Models;

namespace YEE.Identity.Application.Helpers
{
    public interface IHasher
    {
        string Hash(string password);
    }

    public class Hasher : IHasher
    {
        private readonly AppSettings AppSettings;

        public Hasher(IOptions<AppSettings> appSettings)
        {
            AppSettings = appSettings.Value;
        }

        public string Hash(string password)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(AppSettings.Crypto.HashKey)))
            {
                byte[] hashedBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
