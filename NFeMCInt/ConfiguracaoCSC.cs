using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeMCInt
{
    public class ConfiguracaoCsc
    {
        public ConfiguracaoCsc(string cIdToken, string csc)
        {
            this.CIdToken = cIdToken;
            Csc = csc;
        }

        /// <summary>
        /// Construtor sem parâmetros para serialização
        /// </summary>
        private ConfiguracaoCsc()
        {
        }

        /// <summary>
        /// Identificador do CSC – Código de Segurança do Contribuinte no Banco de Dados da SEFAZ
        /// </summary>
        public string CIdToken { get; set; }

        /// <summary>
        /// Código de Segurança do Contribuinte(antigo Token)
        /// </summary>
        public string Csc { get; set; }
    }
}
