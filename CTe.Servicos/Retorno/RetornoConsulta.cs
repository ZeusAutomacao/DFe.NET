using CTeDLL.Classes.Servicos.Consulta;

namespace CTeDLL.Servicos.Retorno
{
    public class RetornoCTeConsultaProtocolo : RetornoBasico
    {
        public RetornoCTeConsultaProtocolo(string envioStr, string retornoStr, string retornoCompletaStr, retConsSitCTe retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsSitCTe Retorno { get; set; }
    }
}