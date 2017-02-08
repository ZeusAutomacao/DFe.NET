using CTeDLL.Classes.Servicos.Inutilizacao;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoCteInutilizacao : RetornoBasico
    {
        public RetornoCteInutilizacao(string envioStr, string retornoStr, string retornoCompletaStr, retInutCTe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retInutCTe Retorno { get; set; }
    }
}