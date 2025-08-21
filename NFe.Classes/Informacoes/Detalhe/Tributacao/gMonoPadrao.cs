using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gMonoPadrao
    {
        private decimal _qBcMono;
        private decimal _adRemIbs;
        private decimal _adRemCbs;
        private decimal _vIbsMono;
        private decimal _vCbsMono;

        // UB85
        [XmlElement(Order = 1)]
        public decimal qBCMono
        {
            get => _qBcMono.Arredondar(4);
            set => _qBcMono = value.Arredondar(4);
        }

        // UB86
        [XmlElement(Order = 2)]
        public decimal adRemIBS
        {
            get => _adRemIbs.Arredondar(4);
            set => _adRemIbs = value.Arredondar(4);
        }

        // UB87
        [XmlElement(Order = 3)]
        public decimal adRemCBS
        {
            get => _adRemCbs.Arredondar(4);
            set => _adRemCbs = value.Arredondar(4);
        }

        // UB88
        [XmlElement(Order = 4)]
        public decimal vIBSMono
        {
            get => _vIbsMono.Arredondar(2);
            set => _vIbsMono = value.Arredondar(2);
        }

        // UB89
        [XmlElement(Order = 5)]
        public decimal vCBSMono
        {
            get => _vCbsMono.Arredondar(2);
            set => _vCbsMono = value.Arredondar(2);
        }
    }
}