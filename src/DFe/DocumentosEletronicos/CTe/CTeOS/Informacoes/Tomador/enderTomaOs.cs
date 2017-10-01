using System.Xml.Serialization;
using DFe.DocumentosEletronicos.Entidades;

namespace DFe.DocumentosEletronicos.CTe.CTeOS.Informacoes.Tomador
{
    public class enderTomaOs
    {
        public string xLgr { get; set; }

        public string nro { get; set; }

        public string xCpl { get; set; }

        public string xBairro { get; set; }

        public long cMun { get; set; }

        public string xMun { get; set; }

        public long CEP { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF { get { return UF.GetSiglaUfString(); } set { UF = UF.SiglaParaEstado(value); } }

        public int? cPais { get; set; }

        public bool cPaisSpecified { get { return cPais.HasValue; } }

        public string xPais { get; set; }
    }
}