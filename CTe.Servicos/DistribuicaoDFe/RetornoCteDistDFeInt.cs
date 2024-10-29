using CTe.Classes.Servicos.DistribuicaoDFe;

namespace CTe.Servicos.DistribuicaoDFe
{
    public class RetornoCteDistDFeInt
    {
        public RetornoCteDistDFeInt(string envioStr, string retornoStr, string retornoCompletaStr, retDistDFeInt retorno)
        {
            EnvioStr = envioStr;
            RetornoStr = retornoStr;
            RetornoCompletoStr = retornoCompletaStr;
            Retorno = retorno;
        }


        public string EnvioStr { get; protected set; }
        public string RetornoStr { get; protected set; }
        public string RetornoCompletoStr { get; protected set; }

        public new retDistDFeInt Retorno { get; set; }
    }
}