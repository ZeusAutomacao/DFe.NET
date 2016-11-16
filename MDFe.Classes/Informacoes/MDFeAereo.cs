using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Contratos;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeAereo : MDFeModalContainer
    {
        [XmlElement(ElementName = "nac")]
        public string Nac { get; set; }

        [XmlElement(ElementName = "matr")]
        public string Matr { get; set; }

        [XmlElement(ElementName = "nVoo")]
        public string NVoo { get; set; }

        [XmlElement(ElementName = "cAerEmb")]
        public string CAerEmb { get; set; }

        [XmlElement(ElementName = "cAerDes")]
        public string CAerDes { get; set; }

        [XmlIgnore]
        public DateTime DVoo { get; set; }

        [XmlElement(ElementName = "dVoo")]
        public string ProxyDVoo
        {
            get
            {
                return DVoo.ToString("yyyy-MM-dd");
            }
            set { DVoo = DateTime.Parse(value); }
        }
    }
}