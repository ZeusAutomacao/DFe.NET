using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.InfCTeNormal.Aereo;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class aereo
    {
        public string nMinu { get; set; }

        public string nOCA { get; set; }

        [XmlIgnore]
        public DateTime dPrevAereo { get; set; }

        public string ProxydPrevAereo
        {
            get { return dPrevAereo.ParaDataString(); }
            set { dPrevAereo = Convert.ToDateTime(dPrevAereo); }
        }

        public string xLAgEmi { get; set; }

        public string IdT { get; set; }

        public natCarga natCarga { get; set; }

        public tarifa tarifa { get; set; }

        [XmlElement(ElementName = "peri")]
        public List<Aereo.peri> peri { get; set; }
    }
}