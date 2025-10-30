using NFe.Classes.Servicos.Recepcao;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeRecepcao : RetornoBasico
    {
        public RetornoNfeRecepcao(string envioStr, string retornoStr, string retornoCompletaStr, retEnviNFe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retEnviNFe Retorno { get; set; }
    }
}