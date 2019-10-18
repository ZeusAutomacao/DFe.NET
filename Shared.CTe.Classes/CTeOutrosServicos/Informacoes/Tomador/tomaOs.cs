using System.Xml.Serialization;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.Tomador
{
    public class tomaOs
    {
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        public string IE { get; set; }

        public string xNome { get; set; }

        public string xFant { get; set; }

        public string fone { get; set; }

        [XmlElement(ElementName = "enderToma")]
        public enderTomaOs enderToma { get; set; }

        public string email { get; set; }
    }
}