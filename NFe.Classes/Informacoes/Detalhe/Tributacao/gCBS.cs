using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gCBS
    {
        private decimal _pGBS;
        private decimal _vGBS;

        // UB37
        [XmlElement(Order = 1)]
        public decimal pCBS
        {
            get => _pGBS.Arredondar(4);
            set => _pGBS = value.Arredondar(4);
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

        // UB67
        [XmlElement(Order = 5)]
        public decimal vCBS
        {
            get => _vGBS.Arredondar(2);
            set => _vGBS = value.Arredondar(2);
        }
    }
}