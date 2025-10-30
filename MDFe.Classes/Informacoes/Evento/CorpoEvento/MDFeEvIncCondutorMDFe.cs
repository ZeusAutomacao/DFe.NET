using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    [XmlRoot(ElementName = "evIncCondutorMDFe")]
    public class MDFeEvIncCondutorMDFe : MDFeEventoContainer
    {
        public MDFeEvIncCondutorMDFe()
        {
            DescEvento = "Inclusao Condutor";
        }

        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; }

        [XmlElement(ElementName = "condutor")]
        public MDFeCondutorIncluir Condutor { get; set; }
    }

    public class MDFeCondutorIncluir
    {
        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
    }
}