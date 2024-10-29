using NFe.Classes.Servicos.Status;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeStatusServico : RetornoBasico
    {
        public RetornoNfeStatusServico(string envioStr, string retornoStr, string retornoCompletaStr, retConsStatServ retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsStatServ Retorno { get; set; }
    }
}