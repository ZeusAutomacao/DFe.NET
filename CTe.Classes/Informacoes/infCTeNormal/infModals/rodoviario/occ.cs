using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviario
{
    public class occ
    {
        public string serie { get; set; }
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