using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gIBSMun
    {
        private decimal _pIbsMun;
        private decimal _vIbsMun;

        [XmlElement(Order = 1)]
        public decimal pIBSMun
        {
            get => _pIbsMun.Arredondar(4);
            set => _pIbsMun = value.Arredondar(4);
        }

        [XmlElement(Order = 2)]
        public gDif gDif { get; set; }

        [XmlElement(Order = 3)]
        public gDevTrib gDevTrib { get; set; }

        [XmlElement(Order = 4)]
        public gRed gRed { get; set; }

        [XmlElement(Order = 5)]
        public decimal vIBSMun
        {
            get => _vIbsMun.Arredondar(2);
            set => _vIbsMun = value.Arredondar(2);
        }
    }
}
