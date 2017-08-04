using System;
using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Autorizacao
{
    [Serializable]
    public class infRec
    {
        [XmlElement(ElementName = "nRec")]
        public string nRec { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public DateTime dhRecbto { get; set; }

        [XmlElement(ElementName = "tMed")]
        public int tMed { get; set; }
    }
}