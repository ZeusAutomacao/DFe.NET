using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infDocRef
    {
        public string nDoc { get; set; }
        public short serie { get; set; }
        public short? subserie { get; set; }
        public bool subserieSpecified => subserie.HasValue;

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxydEmi { get { return dEmi.ParaDataString(); } set { dEmi = Convert.ToDateTime(value); } }

        public decimal? vDoc { get; set; }
        public bool vDocSpecified => vDoc.HasValue;
    }
}