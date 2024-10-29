using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfAdic
    {
        /// <summary>
        /// 2 - Informações adicionais de interesse do Fisco
        /// </summary>
        [XmlElement(ElementName = "infAdFisco")]
        public string InfAdFisco { get; set; }

        /// <summary>
        /// 2 - Informações complementares de interesse do Contribuinte
        /// </summary>
        [XmlElement(ElementName = "infCpl")]
        public string InfCpl { get; set; }
    }
}