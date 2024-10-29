using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    [XmlRoot(ElementName = "evCancMDFe")]
    public class MDFeEvCancMDFe : MDFeEventoContainer
    {
        public MDFeEvCancMDFe()
        {
            DescEvento = "Cancelamento";
        }

        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }

        [XmlElement(ElementName = "xJust")]
        public string XJust { get; set; }
    }
}