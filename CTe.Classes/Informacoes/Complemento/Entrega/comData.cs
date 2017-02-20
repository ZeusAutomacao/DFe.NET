using System;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.Complemento
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
