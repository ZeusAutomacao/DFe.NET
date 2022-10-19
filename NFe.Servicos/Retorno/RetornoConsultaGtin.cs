using NFe.Classes.Servicos.ConsultaGtin;

namespace NFe.Servicos.Retorno
{
    public class RetornoConsultaGtin : RetornoBasico
    {
        public RetornoConsultaGtin(string envioStr, string retornoStr, string retornoCompletaStr, retConsGTIN retorno) 
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            retConsGtin = retorno;
        }

        public retConsGTIN retConsGtin { get; set; }
    }
}