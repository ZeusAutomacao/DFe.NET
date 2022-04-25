using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.Tomador
{
    public class enderTomaOs
    {
        public string xLgr { get; set; }

        public string nro { get; set; }

        public string xCpl { get; set; }

        public string xBairro { get; set; }

        public long cMun { get; set; }

        public string xMun { get; set; }

        [XmlIgnore]
        public long? CEP { get; set; }

        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get
            {
                return CEP != null ? CEP.Value.ToString("D8") : null;
            }
            set { CEP = long.Parse(value); }
        }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF { get { return UF.GetSiglaUfString(); } set { UF = UF.SiglaParaEstado(value); } }

        public int? cPais { get; set; }

        public bool cPaisSpecified { get { return cPais.HasValue; } }

        public string xPais { get; set; }
    }
}