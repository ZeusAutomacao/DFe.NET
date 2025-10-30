using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    public class gCBSTotal
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vCBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;

        // W53
        [XmlElement(Order = 1)]
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }

        // W54
        [XmlElement(Order = 2)]
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }

        // W56
        [XmlElement(Order = 3)]
        public decimal vCBS
        {
            get => _vCBS;
            set => _vCBS = value.Arredondar(2);
        }

        // W56a
        [XmlElement(Order = 4)]
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        // W56b
        [XmlElement(Order = 5)]
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}