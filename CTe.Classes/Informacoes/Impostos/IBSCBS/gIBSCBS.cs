using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gIBSCBS
    {
        private decimal _vBc;
        private decimal? _vIbs;

        [XmlElement(Order = 1)]
        public decimal vBC
        {
            get => _vBc.Arredondar(2);
            set => _vBc = value.Arredondar(2);
        }

        [XmlElement(Order = 2)]
        public gIBSUF gIBSUF { get; set; }

        [XmlElement(Order = 3)]
        public gIBSMun gIBSMun { get; set; }

        [XmlElement(Order = 4)]
        public decimal? vIBS
        {
            get => _vIbs.Arredondar(2);
            set => _vIbs = value.Arredondar(2);
        }

        [XmlElement(Order = 5)]
        public gCBS gCBS { get; set; }

        [XmlElement(Order = 6)]
        public gTribRegular gTribRegular { get; set; }

        [XmlElement(Order = 7)]
        public gTribCompraGov gTribCompraGov { get; set; }

        [XmlElement(Order = 8)]
        public gEstornoCred gEstornoCred { get; set; }

        public bool vIBSSpecified
        {
            get { return vIBS.HasValue; }
        }
    }
}
