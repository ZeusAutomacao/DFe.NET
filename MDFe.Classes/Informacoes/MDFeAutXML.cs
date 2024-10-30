using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeAutXML
    {
        /// <summary>
        /// 2 - CNPJ do autorizado 
        /// </summary>
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// 2 - CPF do autorizado
        /// </summary>
        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }
    }
}