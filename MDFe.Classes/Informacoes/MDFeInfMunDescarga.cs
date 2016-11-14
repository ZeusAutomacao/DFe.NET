using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfMunDescarga
    {
        [XmlElement(ElementName = "cMunDescarga")]
        public string CMunDescarga { get; set; }

        [XmlElement(ElementName = "xMunDescarga")]
        public string XMunDescarga { get; set; }

        [XmlElement(ElementName = "infCTe")]
        public List<MDFeInfCTe> InfCTe { get; set; }
    }
}