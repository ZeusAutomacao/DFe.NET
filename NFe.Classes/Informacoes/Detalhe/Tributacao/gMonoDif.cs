using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gMonoDif
    {
        private decimal _pDifIbs;
        private decimal _vIbsMonoDif;
        private decimal _pDifCbs;
        private decimal _vCbsMonoDif;

        // UB100
        [XmlElement(Order = 1)]
        public decimal pDifIBS
        {
            get => _pDifIbs.Arredondar(4);
            set => _pDifIbs = value.Arredondar(4);
        }

        // UB101
        [XmlElement(Order = 2)]
        public decimal vIBSMonoDif
        {
            get => _vIbsMonoDif.Arredondar(2);
            set => _vIbsMonoDif = value.Arredondar(2);
        }

        // UB102
        [XmlElement(Order = 3)]
        public decimal pDifCBS
        {
            get => _pDifCbs.Arredondar(4);
            set => _pDifCbs = value.Arredondar(4);
        }

        // UB103
        [XmlElement(Order = 4)]
        public decimal vCBSMonoDif
        {
            get => _vCbsMonoDif.Arredondar(2);
            set => _vCbsMonoDif = value.Arredondar(2);
        }
    }
}