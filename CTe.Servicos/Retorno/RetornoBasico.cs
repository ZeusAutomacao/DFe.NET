using CTeDLL.Classes.Servicos;

namespace CTeDLL.Servicos.Retorno
{
    public abstract class RetornoBasico
    {
        protected RetornoBasico(string envioStr, string retornoStr, string retornoCompletaStr, RetornoBase retornoBase)
        {
            EnvioStr = envioStr;
            RetornoStr = retornoStr;
            RetornoCompletoStr = retornoCompletaStr;
            RetornoBase = retornoBase;
        }

        public RetornoBase RetornoBase { get; protected set; }
        public string EnvioStr { get; protected set; }
        public string RetornoStr { get; protected set; }
        public string RetornoCompletoStr { get; protected set; }
    }
}