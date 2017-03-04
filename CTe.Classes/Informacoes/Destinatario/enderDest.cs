using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.Destinatario
{
    public class enderDest
    {
        public string xLgr { get; set; }

        public string nro { get; set; }

        public string xCpl { get; set; }

        public string xBairro { get; set; }

        public long cMun { get; set; }

        public string xMun { get; set; }

        /// <summary>
        /// 3 - CEP
        /// </summary>
        [XmlIgnore]
        public long CEP { get; set; }

        /// <summary>
        /// Proxy para colocar zeros a esquerda no CEP 
        /// </summary>
        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get
            {
                return CEP.ToString("D8");
            }
            set { CEP = long.Parse(value); }
        }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        public int? cPais { get; set; }

        public bool cPaisSpecified { get { return cPais.HasValue; } }

        public string xPais { get; set; }
    }
}
