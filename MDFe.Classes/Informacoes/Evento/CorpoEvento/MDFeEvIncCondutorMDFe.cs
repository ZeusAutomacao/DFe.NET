using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    public class MDFeEvIncCondutorMDFe : MDFeEventoContainer
    {
        [XmlElement(ElementName = "DescEvento")]
        public string DescEvento { get; set; }

        [XmlElement(ElementName = "Condutor")]
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