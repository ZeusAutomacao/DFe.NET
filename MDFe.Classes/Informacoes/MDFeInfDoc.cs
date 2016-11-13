using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfDoc
    {
        [XmlElement(ElementName = "infMunDescarga")]
        public List<MDFeInfMunDescarga> InfMunDescarga { get; set; }

        [XmlElement(ElementName = "infCTe")]
        public List<MDFeInfCTe> InfCTe { get; set; }


    }
}