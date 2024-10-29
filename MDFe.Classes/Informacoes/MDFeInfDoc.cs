using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfDoc
    {
        /// <summary>
        /// 2 - Informações dos Municípios de descarregamento
        /// </summary>
        [XmlElement(ElementName = "infMunDescarga")]
        public List<MDFeInfMunDescarga> InfMunDescarga { get; set; }

    }
}