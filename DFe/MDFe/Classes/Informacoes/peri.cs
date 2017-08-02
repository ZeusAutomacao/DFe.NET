using System;
using System.Xml.Serialization;

namespace DFe.MDFe.Classes.Informacoes
{
    [Serializable]
    public class peri
    {
        [XmlElement(ElementName = "nONU")]
        public string NONU { get; set; }

        [XmlElement(ElementName = "xNomeAE")]
        public string XNomeAE { get; set; }

        [XmlElement(ElementName = "xClaRisco")]
        public string XClaRisco { get; set; }

        [XmlElement(ElementName = "grEmb")]
        public string GrEmb { get; set; }

        [XmlElement(ElementName = "qTotProd")]
        public string QTotProd { get; set; }

        [XmlElement(ElementName = "qVolTipo")]
        public string QVolTipo { get; set; }
    }
}