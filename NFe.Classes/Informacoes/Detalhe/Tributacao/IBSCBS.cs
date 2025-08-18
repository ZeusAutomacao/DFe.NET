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

        // UB84
        public gIBSCBSMono gIBSCBSMono { get; set; }

        // UB106
        public gTransfCred gTransfCred { get; set; }

        // UB109
        public gCredPresIBSZFM gCredPresIBSZFM { get; set; }
    }
}