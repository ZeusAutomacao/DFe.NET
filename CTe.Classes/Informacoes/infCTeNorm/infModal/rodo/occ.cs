using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class occ
    {
        public short? serie { get; set; }
        public bool serieSpecified => serie.HasValue;

        public int nOcc { get; set; }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxydEmi
        {
            get
            {
                return dEmi.ParaDataString();
            }
            set
            {
                dEmi = Convert.ToDateTime(value);
            }
        }

        public emiOcc emiOcc { get; set; }
    }
}
