using NFe.Classes.Servicos.Recepcao.Retorno;

namespace NFe.Servicos.Retorno
{
    public class RetornoNFeRetAutorizacao : RetornoBasico
    {
        public RetornoNFeRetAutorizacao(string envioStr, string retornoStr, string retornoCompletaStr, retConsReciNFe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsReciNFe Retorno { get; set; }
    }
}