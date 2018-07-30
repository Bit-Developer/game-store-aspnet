using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Helper
{
    public class GameStorePasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {

            //WARNING: This code does not reflect best practice.  It is a simplistic implementation designed to introduce the concept of hashing.
            password = "$$$$$" + password + "$#!%^";
            var pwdBytes = Encoding.UTF8.GetBytes(password);

            SHA256 hashAlg = new SHA256Managed();
            hashAlg.Initialize();
            var hashedBytes = hashAlg.ComputeHash(pwdBytes);
            var hash = Convert.ToBase64String(hashedBytes);
            return hash;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            String hash = this.HashPassword(providedPassword);
            if (hash == hashedPassword)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}
