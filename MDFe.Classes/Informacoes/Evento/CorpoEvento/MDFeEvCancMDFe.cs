using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    [XmlRoot(ElementName = "evCancMDFe")]
    public class MDFeEvCancMDFe : MDFeEventoContainer
    {
        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; } = "Cancelamento";

        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }

        [XmlElement(ElementName = "xJust")]
        public string XJust { get; set; }
    }
}