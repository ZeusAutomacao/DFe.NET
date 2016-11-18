using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    public class MDFeEvCancMDFe : MDFeEventoContainer
    {
        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; } = "Cancelamento";

        [XmlElement(ElementName = "NPort")]
        public string nPort { get; set; }

        [XmlElement(ElementName = "xJust")]
        public string XJust { get; set; }
    }
}