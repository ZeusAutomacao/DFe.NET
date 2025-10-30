using System;
using NFe.Utils.NFe;

namespace NFe.Utils.Excecoes
{
    /// <summary>
    /// Utilize essa classe para determinar se houve erros de validação de schema XSD
    /// Na biblioteca, são realizadas validações de schema XSD
    /// <para>1 - No consumo de qualquer serviço, o pacote a ser enviado para a SEFAZ é validado, para garantir que está de acordo com a estrutura esperada</para>
    /// <para>2 - No método de extensão <see cref="ExtNFe.Valida"/>, responsável por validar, contra o schema, um objeto NFe</para>    
    /// </summary>
    public class ValidacaoSchemaException : Exception
    {
        /// <summary>
        /// Houve erros de validação de schema XSD
        /// </summary>
        /// <param name="message"></param>
        public ValidacaoSchemaException(string message, string xmlString) : base(
            $"Erros na validação:\n {message}")
        {
            XmlString = xmlString;
        }

        public string XmlString { get; private set; }
    }
}
