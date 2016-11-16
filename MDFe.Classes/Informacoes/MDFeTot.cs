using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    [Serializable]
    public class MDFeTot
    {
        [XmlElement(ElementName = "qCTe")]
        public int? QCTe { get; set; }

        [XmlElement(ElementName = "qNFe")]
        public int? QNFe { get; set; }

        [XmlElement(ElementName = "qMDFe")]
        public int? QMDFe { get; set; }

        [XmlElement(ElementName = "vCarga")]
        public decimal vCarga { get; set; }

        [XmlElement(ElementName = "cUnid")]
        public MDFeCUnid CUnid { get; set; }

        [XmlElement(ElementName = "qCarga")]
        public decimal QCarga { get; set; }

        public bool QCTeSpecified => QCTe.HasValue;
        public bool QNFeSpecified => QNFe.HasValue;
        public bool QMDFeSpecified => QMDFe.HasValue;
    }
}