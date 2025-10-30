using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfTermCarreg
    {
        /// <summary>
        /// 2 - Código do Terminal de Carregamento 
        /// </summary>
        [XmlElement(ElementName = "cTermCarreg")]
        public string CTermCarreg { get; set; }

        /// <summary>
        /// 2 - Nome do Terminal de Carregamento 
        /// </summary>
        [XmlElement(ElementName = "xTermCarreg")]
        public string XTermCarreg { get; set; }
    }
}