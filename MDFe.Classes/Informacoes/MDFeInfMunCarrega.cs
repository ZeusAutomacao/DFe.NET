using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfMunCarrega
    {
        /// <summary>
        /// 3 - Código do Município de Carregamento 
        /// </summary>
        [XmlElement(ElementName = "cMunCarrega")]
        public string CMunCarrega { get; set; }

        /// <summary>
        /// 3 - Nome do Município de Carregamento 
        /// </summary>
        [XmlElement(ElementName = "xMunCarrega")]
        public string XMunCarrega { get; set; }
    }
}