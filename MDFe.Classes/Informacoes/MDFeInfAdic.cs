using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfAdic
    {
        [XmlElement(ElementName = "infAdFisco")]
        public string InfAdFisco { get; set; }

        [XmlElement(ElementName = "infCpl")]
        public string InfCpl { get; set; }
    }
}