using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfEmbComb
    {
        [XmlElement(ElementName = "cEmbComb")]
        public string CEmbComb { get; set; }
    }
}