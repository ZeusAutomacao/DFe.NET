using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gTribRegular
    {
        private string _cClassTribReg;
        private decimal _pAliqEfetRegIbsUf;
        private decimal _vTribRegIbsUf;
        private decimal _pAliqEfetRegIbsMun;
        private decimal _vTribRegIbsMun;
        private decimal _pAliqEfetRegCbs;
        private decimal _vTribRegCbs;

        [XmlElement(Order = 1)]
        public CSTIBSCBS CSTReg { get; set; }

        [XmlElement(Order = 2)]
        public string cClassTribReg
        {
            get => _cClassTribReg;
            set => _cClassTribReg = value;
        }

        [XmlElement(Order = 3)]
        public decimal pAliqEfetRegIBSUF
        {
            get => _pAliqEfetRegIbsUf.Arredondar(4);
            set => _pAliqEfetRegIbsUf = value.Arredondar(4);
        }

        [XmlElement(Order = 4)]
        public decimal vTribRegIBSUF
        {
            get => _vTribRegIbsUf.Arredondar(2);
            set => _vTribRegIbsUf = value.Arredondar(2);
        }

        [XmlElement(Order = 5)]
        public decimal pAliqEfetRegIBSMun
        {
            get => _pAliqEfetRegIbsMun.Arredondar(4);
            set => _pAliqEfetRegIbsMun = value.Arredondar(4);
        }

        [XmlElement(Order = 6)]
        public decimal vTribRegIBSMun
        {
            get => _vTribRegIbsMun.Arredondar(2);
            set => _vTribRegIbsMun = value.Arredondar(2);
        }

        [XmlElement(Order = 7)]
        public decimal pAliqEfetRegCBS
        {
            get => _pAliqEfetRegCbs.Arredondar(4);
            set => _pAliqEfetRegCbs = value.Arredondar(4);
        }

        [XmlElement(Order = 8)]
        public decimal vTribRegCBS
        {
            get => _vTribRegCbs.Arredondar(2);
            set => _vTribRegCbs = value.Arredondar(2);
        }

        /// <summary>
        /// Define o valor de cClassTrib a partir de um inteiro
        /// </summary>
        public void SetcClassTrib(int intValue)
        {
            _cClassTribReg = intValue.ToString("D6");
        }
    }
}
