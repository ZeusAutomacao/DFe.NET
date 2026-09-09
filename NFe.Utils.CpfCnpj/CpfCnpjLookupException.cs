/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Erro de comunicacao ou de resposta invalida da API de consulta.
    /// </summary>
    public class CpfCnpjLookupException : Exception
    {
        /// <summary>
        ///     Codigo de erro devolvido pela API, quando disponivel.
        /// </summary>
        public int? CodigoErro { get; }

        /// <summary>Cria a excecao com uma mensagem.</summary>
        public CpfCnpjLookupException(string mensagem) : base(mensagem)
        {
        }

        /// <summary>Cria a excecao com uma mensagem e o codigo de erro da API.</summary>
        public CpfCnpjLookupException(string mensagem, int? codigoErro) : base(mensagem)
        {
            CodigoErro = codigoErro;
        }

        /// <summary>Cria a excecao com uma mensagem e a excecao de origem.</summary>
        public CpfCnpjLookupException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }
}
