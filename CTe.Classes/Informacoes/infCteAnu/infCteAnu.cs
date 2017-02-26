using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeAnulacao
{
    public class infCteAnu
    {
        public string chCte { get; set; }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxydEmi { get { return dEmi.ParaDataString(); }
            set { dEmi = Convert.ToDateTime(value); } }
    }
}
