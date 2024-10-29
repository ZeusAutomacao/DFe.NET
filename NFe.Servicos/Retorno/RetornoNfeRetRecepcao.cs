using NFe.Classes.Servicos.Recepcao.Retorno;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeRetRecepcao : RetornoBasico
    {
        public RetornoNfeRetRecepcao(string envioStr, string retornoStr, string retornoCompletaStr, retConsReciNFe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsReciNFe Retorno { get; set; }
    }
}