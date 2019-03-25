using System;
using System.Security.Cryptography;
using System.Text;
using NFe.Classes.Informacoes.Identificacao;

namespace NFe.Classes.Informacoes.ResponsavelTecnico
{
    public class infRespTec
    {
        /// <summary>
        /// ZD02 - CNPJ da pessoa jurídica responsável pelo sistema utilizado na emissão do documento fiscal eletrônico
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// ZD04 - Nome da pessoa a ser contatada
        /// </summary>
        public string xContato { get; set; }

        /// <summary>
        /// ZD05 - E-mail da pessoa jurídica a ser contatada
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// ZD06 - Telefone da pessoa jurídica/física a ser contatada
        /// </summary>
        public long fone { get; set; }

        /// <summary>
        /// ZD08 - Identificador do CSRT
        /// </summary>
        public int? idCSRT { get; set; }

        /// <summary>
        /// ZD09 - Hash do CSRT
        /// </summary>
        public string hashCSRT { get; set; }

        protected infRespTec()
        {

        }

        public infRespTec(string cnpj, string xContato, string email, long fone, int? idCsrt = null, string csrt = null, string chave = null)
        {
            CNPJ = cnpj;
            this.xContato = xContato;
            this.email = email;
            this.fone = fone;
            idCSRT = idCsrt;

            if (idCsrt.HasValue && !string.IsNullOrWhiteSpace(csrt) && !string.IsNullOrWhiteSpace(chave))
            {
                hashCSRT = GerarHashCsrt(csrt, chave);
            }
        }

        private string GerarHashCsrt(string csrt, string chave)
        {
            var sha1 = SHA1.Create();

            var computedHash = sha1.ComputeHash(Encoding.UTF8.GetBytes($"{csrt}{chave}"));

            var sb = new StringBuilder();

            foreach (var b in computedHash)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(sb.ToString()));
        }

        public bool ShouldSerializeidCSRT()
        {
            return idCSRT.HasValue;
        }
    }
}