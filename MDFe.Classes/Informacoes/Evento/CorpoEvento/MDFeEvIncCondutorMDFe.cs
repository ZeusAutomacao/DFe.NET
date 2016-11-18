using System;
using System.Xml.Serialization;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento
{
    [Serializable]
    public class MDFeEvIncCondutorMDFe : MDFeEventoContainer
    {
        [XmlElement(ElementName = "descEvento")]
        public string DescEvento { get; set; } = "Inclusao Condutor";

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