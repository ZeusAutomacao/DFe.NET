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
        public CSTIS CSTIS { get; set; }

        // UB03
        public cClassTribIS cClassTribIS { get; set; }

        // UB05
        public decimal vBCIS
        {
            get => _vBcIs.Arredondar(2);
            set => _vBcIs = value.Arredondar(2);
        }

        // UB06
        public decimal pIS
        {
            get => _pIs.Arredondar(4);
            set => _pIs = value.Arredondar(4);
        }

        // UB07
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
        public string uTrib { get; set; }

        // UB10
        public decimal qTrib
        {
            get => _qTrib.Arredondar(4);
            set => _qTrib = value.Arredondar(4);
        }

        // UB11
        public decimal vIS
        {
            get => _vIs.Arredondar(2);
            set => _vIs = value.Arredondar(2);
        }
    }
}