using DFe.Classes;
using MDFe.Classes.Flags;
using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class Comp
    {
        public MDFeTpComp MdFeTpComp { get; set; }

        [XmlIgnore]
        public decimal vComp { get; set; }

        [XmlElement("vComp")]
        public decimal vCompProxy
        {
            get { return vComp.Arredondar(2); }
            set { vComp = value.Arredondar(2); }
        }

        public string xComp { get; set; }
    }
}