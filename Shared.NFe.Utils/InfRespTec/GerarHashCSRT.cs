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

            var hash = Hash(csrtChave, encoding);

            var hashCSRT = Convert.ToBase64String(encoding.GetBytes(hash));

            return hashCSRT;
        }

        private static string Hash(string input, Encoding encoding)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(encoding.GetBytes(input));

                var sb = new StringBuilder(hash.Length * 2);
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString(0, 20);
            }
        }
    }
}
