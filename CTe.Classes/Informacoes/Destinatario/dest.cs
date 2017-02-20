using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Destinatario
{
    public class dest
    {
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string IE { get; set; }

        public string xNome { get; set; }

        public string fone { get; set; }

        public string ISUF { get; set; }

        public enderDest enderDest { get; set; }

        public string email { get; set; }

        /// <summary>
        /// Versão 2.00
        /// </summary>
        public locEnt locEnt { get; set; }
    }
}
