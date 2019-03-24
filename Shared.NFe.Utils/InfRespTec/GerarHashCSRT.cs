using System;
using System.Security.Cryptography;
using System.Text;

namespace Shared.NFe.Utils.InfRespTec
{
    public static class GerarHashCSRT
    {
        public static string HashCSRT(string csrt, global::NFe.Classes.NFe nfe, Encoding encoding = null)
        {
            return HashCSRT(csrt, nfe.infNFe.Id.Substring(0, 3), encoding);
        }

        public static string HashCSRT(string csrt, string chave, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            var csrtChave = new StringBuilder(csrt).Append(chave).ToString();

            var data = encoding.GetBytes(csrtChave);

            string chaveBase64;

            using (SHA1CryptoServiceProvider cryptoTransformSha1 = new SHA1CryptoServiceProvider())
            {
                var hash = cryptoTransformSha1.ComputeHash(data);
                chaveBase64 = Convert.ToBase64String(hash);
            }

            return chaveBase64;
        }
    }
}
