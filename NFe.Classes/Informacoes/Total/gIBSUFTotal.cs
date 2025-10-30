using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    public class gIBSUFTotal
    {
        private decimal _vDif;
        private decimal _vDevTrib;
        private decimal _vIBSUF;

        // W38
        [XmlElement(Order = 1)]
        public decimal vDif
        {
            get => _vDif;
            set => _vDif = value.Arredondar(2);
        }

        // W39
        [XmlElement(Order = 2)]
        public decimal vDevTrib
        {
            get => _vDevTrib;
            set => _vDevTrib = value.Arredondar(2);
        }

        // W41
        [XmlElement(Order = 3)]
        public decimal vIBSUF
        {
            get => _vIBSUF;
            set => _vIBSUF = value.Arredondar(2);
        }
    }
}