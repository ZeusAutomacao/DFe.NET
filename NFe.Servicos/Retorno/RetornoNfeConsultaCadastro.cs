using NFe.Classes.Servicos.ConsultaCadastro;

namespace NFe.Servicos.Retorno
{
    public class RetornoNfeConsultaCadastro : RetornoBasico
    {
        public RetornoNfeConsultaCadastro(string envioStr, string retornoStr, string retornoCompletaStr, retConsCad retorno)
            : base(envioStr, retornoStr, retornoCompletaStr, retorno)
        {
            Retorno = retorno;
        }

        public new retConsCad Retorno { get; set; }
    }
}