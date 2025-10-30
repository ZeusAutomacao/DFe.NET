using NFe.Classes.Servicos.Consulta;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeConsultaProtocolo : RetornoBasico
    {
        public RetornoNfeConsultaProtocolo(string envioStr, string retornoStr, string retornoCompletaStr, retConsSitNFe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsSitNFe Retorno { get; set; }
    }
}