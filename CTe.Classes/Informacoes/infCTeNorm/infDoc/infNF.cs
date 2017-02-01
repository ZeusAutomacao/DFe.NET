using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infNF
    {
        public string nRoma { get; set; }
        public string nPed { get; set; }
        public string mod { get; set; }
        public string serie { get; set; }
        public string nDoc { get; set; }
        public DateTime dEmi { get; set; }
        public double vBC { get; set; }
        public double vICMS { get; set; }
        public double vBCST { get; set; }
        public double vST { get; set; }
        public double vProd { get; set; }
        public double vNF { get; set; }
        public int nCFOP { get; set; }
        public decimal nPeso { get; set; }
        public string PIN { get; set; }
        public DateTime? dPrev { get; set; }

        [XmlElement("infUnidTransp")]
        public List<infUnidTransp> infUnidTransp;

        [XmlElement("infUnidCarga")]
        public List<infUnidCarga> infUnidCarga;
    }
}
