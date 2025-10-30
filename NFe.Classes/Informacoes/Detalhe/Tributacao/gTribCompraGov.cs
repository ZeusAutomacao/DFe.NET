using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gTribCompraGov
    {
        private decimal _pAliqIbsUf;
        private decimal _vTribIbsUf;
        private decimal _pAliqIbsMun;
        private decimal _vTribIbsMun;
        private decimal _pAliqCbs;
        private decimal _vTribCbs;

        // UB82b
        [XmlElement(Order = 1)]
        public decimal pAliqIBSUF
        {
            get => _pAliqIbsUf.Arredondar(4);
            set => _pAliqIbsUf = value.Arredondar(4);
        }

        // UB82c
        [XmlElement(Order = 2)]
        public decimal vTribIBSUF
        {
            get => _vTribIbsUf.Arredondar(2);
            set => _vTribIbsUf = value.Arredondar(2);
        }

        // UB82d
        [XmlElement(Order = 3)]
        public decimal pAliqIBSMun
        {
            get => _pAliqIbsMun.Arredondar(4);
            set => _pAliqIbsMun = value.Arredondar(4);
        }

        // UB82e
        [XmlElement(Order = 4)]
        public decimal vTribIBSMun
        {
            get => _vTribIbsMun.Arredondar(2);
            set => _vTribIbsMun = value.Arredondar(2);
        }

        // UB82f
        [XmlElement(Order = 5)]
        public decimal pAliqCBS
        {
            get => _pAliqCbs.Arredondar(4);
            set => _pAliqCbs = value.Arredondar(4);
        }

        // UB82g
        [XmlElement(Order = 6)]
        public decimal vTribCBS
        {
            get => _vTribCbs.Arredondar(2);
            set => _vTribCbs = value.Arredondar(2);
        }
    }
}