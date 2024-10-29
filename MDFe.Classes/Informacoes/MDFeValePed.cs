using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeValePed
    {
        /// <summary>
        /// 2 - Informações dos dispositivos do Vale Pedágio
        /// </summary>
        [XmlElement(ElementName = "disp")]
        public List<MDFeDisp> Disp { get; set; }

        public categCombVeic? categCombVeic { get; set; }

        public bool categCombVeicSpecified { get { return categCombVeic.HasValue; } }
    }
}