using CTeDLL.Classes.Servicos.Recepcao.Retorno;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoCteRetRecepcao : RetornoBasico
    {
        public RetornoCteRetRecepcao(string envioStr, string retornoStr, string retornoCompletaStr, retConsReciCTe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsReciCTe Retorno { get; set; }
    }
}