using System;
using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Seguro
{
    [Serializable]
    public class infSeg
    {
        [XmlElement(ElementName = "xSeg")]
        public string xSeg { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }
    }
}