using CTeDLL.Classes.Servicos;

namespace CTeDLL.Servicos.Retorno
{
    public abstract class RetornoBasico
    {
        protected RetornoBasico(string envioStr, string retornoStr, string retornoCompletaStr, IRetornoServico retorno)
        {
            EnvioStr = envioStr;
            RetornoStr = retornoStr;
            RetornoCompletoStr = retornoCompletaStr;
            Retorno = retorno;
        }

        public IRetornoServico Retorno { get; protected set; }
        public string EnvioStr { get; protected set; }
        public string RetornoStr { get; protected set; }
        public string RetornoCompletoStr { get; protected set; }
    }
}