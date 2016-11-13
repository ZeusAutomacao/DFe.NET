using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Contratos;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes
{
    public class MDFeFerrov : IMDFeModal
    {
        [XmlElement(ElementName = "trem")]
        public MDFeTrem Trem { get; set; }
    }
}