using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfTermDescarreg
    {
        /// <summary>
        /// 2 - Código do Terminal de Descarregamento
        /// </summary>
        [XmlElement(ElementName = "cTermDescarreg")]
        public string CTermDescarreg { get; set; }

        /// <summary>
        /// 2 - Nome do Terminal de Descarregamento
        /// </summary>
        [XmlElement(ElementName = "xTermDescarreg")]
        public string XTermDescarreg { get; set; }
    }
}