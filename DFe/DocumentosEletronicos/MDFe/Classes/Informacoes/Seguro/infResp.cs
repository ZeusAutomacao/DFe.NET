using System;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Seguro
{
    [Serializable]
    public class infResp
    {
        [XmlElement(ElementName = "respSeg")]
        public respSeg respSeg { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
    }
}