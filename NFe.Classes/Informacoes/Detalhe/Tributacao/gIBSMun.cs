using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSMun
    {
        private decimal _pIbsMun;
        private decimal _vIbsMun;

        // UB37
        [XmlElement(Order = 1)]
        public decimal pIBSMun
        {
            get => _pIbsMun.Arredondar(4);
            set => _pIbsMun = value.Arredondar(4);
        }

        // UB40
        [XmlElement(Order = 2)]
        public gDif gDif { get; set; }

        // UB43
        [XmlElement(Order = 3)]
        public gDevTrib gDevTrib { get; set; }

        // UB45
        [XmlElement(Order = 4)]
        public gRed gRed { get; set; }

        // UB54
        [XmlElement(Order = 5)]
        public decimal vIBSMun
        {
            get => _vIbsMun.Arredondar(2);
            set => _vIbsMun = value.Arredondar(2);
        }
    }
}