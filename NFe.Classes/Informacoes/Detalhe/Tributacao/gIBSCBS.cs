using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class gIBSCBS
    {
        private decimal _vBc;

        // UB16
        [XmlElement(Order = 1)]
        public decimal vBC
        {
            get => _vBc.Arredondar(2);
            set => _vBc = value.Arredondar(2);
        }

        // UB17
        [XmlElement(Order = 2)]
        public gIBSUF gIBSUF { get; set; }

        // UB36
        [XmlElement(Order = 3)]
        public gIBSMun gIBSMun { get; set; }

        // UB55
        [XmlElement(Order = 4)]
        public gCBS gCBS { get; set; }

        // UB68
        [XmlElement(Order = 5)]
        public gTribRegular gTribRegular { get; set; }

        // UB73
        [XmlElement(Order = 6)]
        public gIBSCredPres gIBSCredPres { get; set; }

        // UB78
        [XmlElement(Order = 7)]
        public gIBSCredPres gCBSCredPres { get; set; }

        // UB82a
        [XmlElement(Order = 8)]
        public gTribCompraGov gTribCompraGov { get; set; }
    }
}