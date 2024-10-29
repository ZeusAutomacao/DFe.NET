using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
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
        private decimal? _unidRat;
        public short serie { get; set; }
        public string nDoc { get; set; }

        public decimal? unidRat
        {
            get { return _unidRat.Arredondar(2); }
            set { _unidRat = value.Arredondar(2); }
        }

        public bool unidRatSpecified { get { return unidRat.HasValue; } }
    }

    public class infNFeAquav
    {
        private decimal? _unidRat;
        public string chave { get; set; }

        public decimal? unidRat
        {
            get { return _unidRat.Arredondar(2); }
            set { _unidRat = value.Arredondar(2); }
        }

        public bool unidRatSpecified { get { return unidRat.HasValue; } }
    }
}