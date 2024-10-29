using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeLacUnidCarga
    {
        /// <summary>
        /// 7 - Número do lacre 
        /// </summary>
        [XmlElement(ElementName = "nLacre")]
        public string NLacre { get; set; }
    }
}