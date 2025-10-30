using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class IBSCBS
    {
        // UB13
        [XmlElement(Order = 1)]
        public CSTIBSCBS CST { get; set; }

        // UB14
        [XmlElement(Order = 2)]
        public cClassTrib cClassTrib { get; set; }

        // UB15
        [XmlElement(Order = 3)]
        public gIBSCBS gIBSCBS { get; set; }

        // UB84
        [XmlElement(Order = 4)]
        public gIBSCBSMono gIBSCBSMono { get; set; }

        // UB106
        [XmlElement(Order = 5)]
        public gTransfCred gTransfCred { get; set; }

        // UB109
        [XmlElement(Order = 6)]
        public gCredPresIBSZFM gCredPresIBSZFM { get; set; }
    }
}