using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gCBS
    {
        private decimal _pGBS;
        private decimal _vGBS;

        [XmlElement(Order = 1)]
        public decimal pCBS
        {
            get => _pGBS.Arredondar(4);
            set => _pGBS = value.Arredondar(4);
        }

        [XmlElement(Order = 2)]
        public gDif gDif { get; set; }

        [XmlElement(Order = 3)]
        public gDevTrib gDevTrib { get; set; }

        [XmlElement(Order = 4)]
        public gRed gRed { get; set; }

        [XmlElement(Order = 5)]
        public decimal vCBS
        {
            get => _vGBS.Arredondar(2);
            set => _vGBS = value.Arredondar(2);
        }
    }
}
