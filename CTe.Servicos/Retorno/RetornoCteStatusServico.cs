using CTeDLL.Classes.Servicos.Status;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoCteStatusServico : RetornoBasico
    {
        public RetornoCteStatusServico(string envioStr, string retornoStr, string retornoCompletaStr, retConsStatServCte retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsStatServCte Retorno { get; set; }
    }
}