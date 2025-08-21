using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class IS
    {
        private decimal _vBcIs;
        private decimal _pIs;
        private decimal? _pIsEspec;
        private decimal _qTrib;
        private decimal _vIs;

        // UB02
        [XmlElement(Order = 1)]
        public CSTIS CSTIS { get; set; }

        // UB03
        [XmlElement(Order = 2)]
        public cClassTribIS cClassTribIS { get; set; }

        // UB05
        [XmlElement(Order = 3)]
        public decimal vBCIS
        {
            get => _vBcIs.Arredondar(2);
            set => _vBcIs = value.Arredondar(2);
        }

        // UB06
        [XmlElement(Order = 4)]
        public decimal pIS
        {
            get => _pIs.Arredondar(4);
            set => _pIs = value.Arredondar(4);
        }

        // UB07
        [XmlElement(Order = 5)]
        public decimal? pISEspec
        {
            get => _pIsEspec.Arredondar(4);
            set => _pIsEspec = value.Arredondar(4);
        }
        public bool ShouldSerializepISEspec()
        {
            return pISEspec.HasValue;
        }

        // UB09
        [XmlElement(Order = 6)]
        public string uTrib { get; set; }

        // UB10
        [XmlElement(Order = 7)]
        public decimal qTrib
        {
            get => _qTrib.Arredondar(4);
            set => _qTrib = value.Arredondar(4);
        }

        // UB11
        [XmlElement(Order = 8)]
        public decimal vIS
        {
            get => _vIs.Arredondar(2);
            set => _vIs = value.Arredondar(2);
        }
    }
}