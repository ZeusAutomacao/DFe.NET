using System;
using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Retorno.ConsultaNaoEncerrados
{
    [Serializable]
    public class infMDFe
    {
        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }
        [XmlElement(ElementName = "nProt")]
        public string NProt { get; set; }
    }
}