using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    [XmlRoot(ElementName = "evIncDFeMDFe")]
    public class MDFeEvIncDFeMDFe : MDFeEventoContainer
    {
        public MDFeEvIncDFeMDFe()
        {
            DescEvento = "Inclusao DF-e";
        }

        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; }

        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }

        [XmlElement(ElementName = "cMunCarrega")]
        public string CMunCarrega { get; set; }

        [XmlElement(ElementName = "xMunCarrega")]
        public string XMunCarrega { get; set; }

        [XmlElement(ElementName = "infDoc")]
        public List<MDFeInfDocInc> InfDoc { get; set; }
    }

    public class MDFeInfDocInc
    {
        [XmlElement(ElementName = "cMunDescarga")]
        public string CMunDescarga { get; set; }

        [XmlElement(ElementName = "xMunDescarga")]
        public string XMunDescarga { get; set; }

        [XmlElement(ElementName = "chNFe")]
        public string ChNFe { get; set; }
    }
}