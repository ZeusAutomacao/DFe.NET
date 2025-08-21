using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gTribRegular
    {
        private decimal _pAliqEfetRegIbsUf;
        private decimal _vTribRegIbsUf;
        private decimal _pAliqEfetRegIbsMun;
        private decimal _vTribRegIbsMun;
        private decimal _pAliqEfetRegCbs;
        private decimal _vTribRegCbs;

        // UB69
        [XmlElement(Order = 1)]
        public CSTIBSCBS CSTReg { get; set; }

        // UB70
        [XmlElement(Order = 2)]
        public cClassTrib cClassTribReg { get; set; }

        // UB71
        [XmlElement(Order = 3)]
        public decimal pAliqEfetRegIBSUF
        {
            get => _pAliqEfetRegIbsUf.Arredondar(4);
            set => _pAliqEfetRegIbsUf = value.Arredondar(4);
        }

        // UB72
        [XmlElement(Order = 4)]
        public decimal vTribRegIBSUF
        {
            get => _vTribRegIbsUf.Arredondar(2);
            set => _vTribRegIbsUf = value.Arredondar(2);
        }

        // UB72a
        [XmlElement(Order = 5)]
        public decimal pAliqEfetRegIBSMun
        {
            get => _pAliqEfetRegIbsMun.Arredondar(4);
            set => _pAliqEfetRegIbsMun = value.Arredondar(4);
        }

        // UB72b
        [XmlElement(Order = 6)]
        public decimal vTribRegIBSMun
        {
            get => _vTribRegIbsMun.Arredondar(2);
            set => _vTribRegIbsMun = value.Arredondar(2);
        }

        // UB72c
        [XmlElement(Order = 7)]
        public decimal pAliqEfetRegCBS
        {
            get => _pAliqEfetRegCbs.Arredondar(4);
            set => _pAliqEfetRegCbs = value.Arredondar(4);
        }

        // UB72d
        [XmlElement(Order = 8)]
        public decimal vTribRegCBS
        {
            get => _vTribRegCbs.Arredondar(2);
            set => _vTribRegCbs = value.Arredondar(2);
        }
    }
}