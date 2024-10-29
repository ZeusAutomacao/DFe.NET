using NFe.Classes.Servicos.Inutilizacao;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeInutilizacao : RetornoBasico
    {
        public RetornoNfeInutilizacao(string envioStr, string retornoStr, string retornoCompletaStr, retInutNFe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retInutNFe Retorno { get; set; }
    }
}