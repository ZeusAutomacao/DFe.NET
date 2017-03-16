using CTeDLL.Classes.Servicos.Recepcao;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoCteRecepcao : RetornoBasico
    {
        public RetornoCteRecepcao(string envioStr, string retornoStr, string retornoCompletaStr, retEnviCte retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retEnviCte Retorno { get; set; }
    }
}