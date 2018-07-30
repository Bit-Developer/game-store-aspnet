using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GameStore.WebUI.Helper
{
    public class CreditAuthorizationClient
    {
        /*
         *pSecretValue - the secret key issued when you registered at the Credit Gateway
         *pAppId - the appid issued to you when you registered at the credit gateway
         *pTransId - the transaction id your system issues to identify the purchase
         *pTransAmount - the value you are charging for this transaction
         */
        public static String GenerateClientRequestHash(String pSecretValue, String pAppId, String pTransId, String pTransAmount)
        {
            try
            {
                String secretPartA = pSecretValue.Substring(0, 5);
                String secretPartB = pSecretValue.Substring(5, 5);
                String val = secretPartA + "-" + pAppId + "-" + pTransId + "-" + pTransAmount + "-" + secretPartB;
                var pwdBytes = Encoding.UTF8.GetBytes(val);

                SHA256 hashAlg = new SHA256Managed();
                hashAlg.Initialize();
                var hashedBytes = hashAlg.ComputeHash(pwdBytes);
                var hash = Convert.ToBase64String(hashedBytes);
                return hash;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /*
         *pHash - is the Hash Returned from the Credit Service.  You need to URLDecode the value before passing it in.
         *pSecretValue - the secret key issued when you registered at the Credit Gateway
         *pAppId - the appid issued to you when you registered at the credit gateway
         *pTransId - the transaction id your system issues to identify the purchase
         *pTransAmount - the value you are charging for this transaction
         *pAppStatus - The status of the credit transaction. Values : A = Accepted, D = Denied
         */
        public static bool VerifyServerResponseHash(String pHash, String pSecretValue, String pAppId, String pTransId, String pTransAmount, String pAppStatus)
        {
            String secretPartA = pSecretValue.Substring(0, 5);
            String secretPartB = pSecretValue.Substring(5, 5);
            String val = secretPartA + "-" + pAppId + "-" + pTransId + "-" + pTransAmount + "-" + pAppStatus + "-" + secretPartB;
            var pwdBytes = Encoding.UTF8.GetBytes(val);

            SHA256 hashAlg = new SHA256Managed();
            hashAlg.Initialize();
            var hashedBytes = hashAlg.ComputeHash(pwdBytes);
            var hash = Convert.ToBase64String(hashedBytes);

            if (hash == pHash)
                return true;
            else
                return false;
        }
    }
}