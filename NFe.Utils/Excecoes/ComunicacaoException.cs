using System;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Excecoes
{
    /// <summary>
    /// Utilize essa classe para determinar se houve problemas com a internet, durante o envio dos dados para um webservice da NFe
    /// </summary>
    public class ComunicacaoException : Exception
    {
        /// <summary>
        /// Houve problemas com a internet, durante o envio dos dados para um webservice da NFe
        /// </summary>
        /// <param name="servico">Serviço que gerou o erro</param>
        /// <param name="message"></param>
        public ComunicacaoException(ServicoNFe servico, string message) : base(string.Format("Sem comunicação com o serviço {0}:\n{1}", servico, message)){}
    }
}