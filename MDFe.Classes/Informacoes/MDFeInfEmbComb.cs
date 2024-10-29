using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfEmbComb
    {
        /// <summary>
        /// 2 - Código da embarcação do comboio 
        /// </summary>
        [XmlElement(ElementName = "cEmbComb")]
        public string CEmbComb { get; set; }

        public string xBalsa { get; set; }
    }
}