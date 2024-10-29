using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals.aereos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class aereo : ContainerModal
    {
        public string nMinu { get; set; }

        public string nOCA { get; set; }

        [XmlIgnore]
        public DateTime dPrevAereo { get; set; }

        [XmlElement(ElementName = "dPrevAereo")]
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
        public List<aereoPeri> peri { get; set; }
    }
}