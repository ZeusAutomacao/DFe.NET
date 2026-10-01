using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gIBSUF
    {
        private decimal _pIbsUf;
        private decimal _vIbsUf;

        [XmlElement(Order = 1)]
        public decimal pIBSUF
        {
            get => _pIbsUf.Arredondar(4);
            set => _pIbsUf = value.Arredondar(4);
        }

        [XmlElement(Order = 2)]
        public gDif gDif { get; set; }

        [XmlElement(Order = 3)]
        public gDevTrib gDevTrib { get; set; }

        [XmlElement(Order = 4)]
        public gRed gRed { get; set; }

        [XmlElement(Order = 5)]
        public decimal vIBSUF
        {
            get => _vIbsUf.Arredondar(2);
            set => _vIbsUf = value.Arredondar(2);
        }
    }
     
}
