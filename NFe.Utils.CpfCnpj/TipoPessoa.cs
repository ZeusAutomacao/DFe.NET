/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Natureza do documento consultado.
    /// </summary>
    public enum TipoPessoa
    {
        /// <summary>
        ///     Pessoa fisica (CPF, 11 digitos).
        /// </summary>
        Fisica,

        /// <summary>
        ///     Pessoa juridica (CNPJ, 14 digitos).
        /// </summary>
        Juridica
    }
}
