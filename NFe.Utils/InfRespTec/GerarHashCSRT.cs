using System;
using System.Text;
using DFe.Utils.Assinatura;

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

            var sha1HashBytes = AssinaturaDigital.ObterHashSha1Bytes(data);
            var chaveBase64 = Convert.ToBase64String(sha1HashBytes);

            return chaveBase64;
        }
    }
}
