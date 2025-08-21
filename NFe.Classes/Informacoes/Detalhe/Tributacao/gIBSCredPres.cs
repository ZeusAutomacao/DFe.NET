using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSCredPres
    {
        private decimal _pCredPres;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;

        // UB74
        [XmlElement(Order = 1)]
        public TipocCredPres cCredPres { get; set; }

        // UB75
        [XmlElement(Order = 2)]
        public decimal pCredPres
        {
            get => _pCredPres.Arredondar(4);
            set => _pCredPres = value.Arredondar(4);
        }

        // UB76
        [XmlElement(Order = 3)]
        public decimal vCredPres
        {
            get => _vCredPres.Arredondar(2);
            set => _vCredPres = value.Arredondar(2);
        }

        // UB77
        [XmlElement(Order = 4)]
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus.Arredondar(2);
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}