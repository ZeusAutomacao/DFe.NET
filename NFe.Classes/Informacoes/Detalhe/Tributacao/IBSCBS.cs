using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class IBSCBS
    {
        // UB13
        public CSTIBSCBS CST { get; set; }

        // UB14
        public cClassTrib cClassTrib { get; set; }

        // UB15
        public gIBSCBS gIBSCBS { get; set; }
    }
}