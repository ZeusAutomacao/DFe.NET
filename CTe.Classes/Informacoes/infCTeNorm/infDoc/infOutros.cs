using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infOutros
    {
        public int tpDoc { get; set; }
        public string descOutros { get; set; }
        public string nDoc { get; set; }
        public DateTime dEmi { get; set; }
        public double vDocFisc { get; set; }
        public DateTime? dPrev { get; set; }

        [XmlElement("infUnidTransp")]
        public List<infUnidTransp> infUnidTransp;

        [XmlElement("infUnidCarga")]
        public List<infUnidCarga> infUnidCarga;
    }
}
