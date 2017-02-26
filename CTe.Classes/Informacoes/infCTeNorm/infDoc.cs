using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infDoc
    {
        [XmlElement(ElementName = "infNF")]
        public List<infNF> infNF { get; set; }
        public List<infNFe> infNFe { get; set; }
        public List<infOutros> infOutros { get; set; }

        public string nCont { get; set; }

        public string dPrev { get; set; }
    }
}
