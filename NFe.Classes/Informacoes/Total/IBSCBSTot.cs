using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Total
{
    public class IBSCBSTot
    {
        private decimal _vBCIBSCBS;

        // W35
        [XmlElement(Order = 1)]
        public decimal vBCIBSCBS
        {
            get => _vBCIBSCBS;
            set => _vBCIBSCBS = value.Arredondar(2);
        }

        // W36
        [XmlElement(Order = 2)]
        public gIBS gIBS { get; set; }

        // W50
        [XmlElement(Order = 3)]
        public gCBSTotal gCBS { get; set; }

        // W57
        [XmlElement(Order = 4)]
        public gMono gMono { get; set; }
    }
}