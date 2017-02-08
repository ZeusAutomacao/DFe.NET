using CTeDLL.Classes.Servicos.Recepcao.Retorno;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoCteRetRecepcao : RetornoBasico
    {
        public RetornoCteRetRecepcao(string envioStr, string retornoStr, string retornoCompletaStr, retconsReciCTe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retconsReciCTe Retorno { get; set; }
    }
}