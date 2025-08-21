using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    public class gIBS
    {
        private decimal _vIBS;
        private decimal _vCredPres;
        private decimal _vCredPresCondSus;

        // W37
        [XmlElement(Order = 1)]
        public gIBSUFTotal gIBSUF { get; set; }

        // W42
        [XmlElement(Order = 2)]
        public gIBSMunTotal gIBSMun { get; set; }

        // W47
        [XmlElement(Order = 3)]
        public decimal vIBS
        {
            get => _vIBS;
            set => _vIBS = value.Arredondar(2);
        }

        // W48
        [XmlElement(Order = 4)]
        public decimal vCredPres
        {
            get => _vCredPres;
            set => _vCredPres = value.Arredondar(2);
        }

        // W49
        [XmlElement(Order = 5)]
        public decimal vCredPresCondSus
        {
            get => _vCredPresCondSus;
            set => _vCredPresCondSus = value.Arredondar(2);
        }
    }
}