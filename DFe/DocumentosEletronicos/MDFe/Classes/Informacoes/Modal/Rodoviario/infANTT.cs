using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Modal.Rodoviario
{
    [Serializable]
    public class infANTT
    {
        [XmlElement(ElementName = "RNTRC")]
        public string RNTRC { get; set; }

        [XmlElement(ElementName = "infCIOT")]
        public List<infCIOT> infCIOT { get; set; }

        public valePed valePed { get; set; }

        [XmlElement(ElementName = "infContratante")]
        public List<infContratante> infContratante { get; set; }
    }
}