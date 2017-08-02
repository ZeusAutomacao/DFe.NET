using System;
using System.Xml.Serialization;

namespace DFe.MDFe.Classes.Informacoes.DocumentosFiscais
{
    [Serializable]
    public class peri
    {
        [XmlElement(ElementName = "nONU")]
        public string nONU { get; set; }

        [XmlElement(ElementName = "xNomeAE")]
        public string xNomeAE { get; set; }

        [XmlElement(ElementName = "xClaRisco")]
        public string xClaRisco { get; set; }

        [XmlElement(ElementName = "grEmb")]
        public string grEmb { get; set; }

        [XmlElement(ElementName = "qTotProd")]
        public string qTotProd { get; set; }

        [XmlElement(ElementName = "qVolTipo")]
        public string qVolTipo { get; set; }
    }
}