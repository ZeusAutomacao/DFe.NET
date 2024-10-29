using NFe.Classes.Servicos.Download;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeDownload : RetornoBasico
    {
        public RetornoNfeDownload(string envioStr, string retornoStr, string retornoCompletaStr, retDownloadNFe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retDownloadNFe Retorno { get; set; }
    }
}