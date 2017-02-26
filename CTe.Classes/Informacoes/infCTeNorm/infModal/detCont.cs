using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class detCont
    {
        public string nCont { get; set; }

        [XmlElement(ElementName = "lacre")]
        public List<lacre> lacre { get; set; }

        [XmlElement(ElementName = "infDoc")]
        public List<infDocAquav> infDoc { get; set; }

    }

    public class lacre
    {
        public string nLacre { get; set; }
    }

    public class infDocAquav
    {
        [XmlElement(ElementName = "infNF")]
        public List<infNFAquav> infNF { get; set; }

        [XmlElement(ElementName = "infNFe")]
        public List<infNFeAquav> infNFe { get; set; }
    }

    public class infNFAquav
    {
        public short serie { get; set; }
        public string nDoc { get; set; }
        public decimal? unidRat { get; set; }
        public bool unidRatSpecified => unidRat.HasValue;
    }

    public class infNFeAquav
    {
        public string chave { get; set; }
        public decimal? unidRat { get; set; }
        public bool unidRatSpecified => unidRat.HasValue;
    }
}