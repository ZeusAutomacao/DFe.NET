using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    public class MDFeInfUnidCargaVazia
    {
        [XmlElement(ElementName = "idUnidCargaVazia")]
        public string IdUnidCargaVazia { get; set; }

        [XmlElement(ElementName = "tpUnidCargaVazia")]
        public MDFeTpUnidCargaVazia TpUnidCargaVazia { get; set; }
    }
}