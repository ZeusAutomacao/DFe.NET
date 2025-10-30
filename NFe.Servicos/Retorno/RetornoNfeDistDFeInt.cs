using NFe.Classes.Servicos.DistribuicaoDFe;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeDistDFeInt : RetornoBasico
    {
        public RetornoNfeDistDFeInt(string envioStr, string retornoStr, string retornoCompletaStr, retDistDFeInt retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retDistDFeInt Retorno { get; set; }
    }
}