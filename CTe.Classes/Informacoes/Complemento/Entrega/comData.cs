using System;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Complemento.Tipos;
using CTe.Classes.Informacoes.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.Complemento
{
    public class comData : comDataBase
    {
        public tpPer tpPer { get; set; }

        [XmlIgnore]
        public DateTime dProg { get; set; }

        [XmlElement(ElementName = "dProg")]
        public string ProxydProg
        {
            get
            {
                return dProg.ParaDataString();
             
            }
            set { dProg = DateTime.Parse(value); }
        }
    }
}