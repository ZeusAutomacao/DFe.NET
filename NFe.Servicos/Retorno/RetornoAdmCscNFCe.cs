using NFe.Classes.Servicos.AdmCsc;

namespace NFe.Servicos.Retorno
{
    public class RetornoAdmCscNFCe : RetornoBasico
    {
        public RetornoAdmCscNFCe(string envioStr, string retornoStr, string retornoCompletaStr, retAdmCscNFCe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retAdmCscNFCe Retorno { get; set; }
    }
}