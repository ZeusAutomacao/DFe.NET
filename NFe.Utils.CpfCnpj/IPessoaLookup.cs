/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Abstracao de consulta de dados cadastrais por documento. Permite trocar a fonte
    ///     (API cpfcnpj.com.br, cache, mock de testes) sem alterar o preenchimento do destinatario.
    /// </summary>
    public interface IPessoaLookup
    {
        /// <summary>
        ///     Consulta os dados cadastrais (nome/razao, endereco) de um documento.
        /// </summary>
        /// <param name="documento">CPF ou CNPJ, com ou sem formatacao.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        Task<PessoaResultado> ConsultarAsync(string documento, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     Consulta as inscricoes estaduais de um CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ, com ou sem formatacao.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        Task<IList<InscricaoEstadual>> ConsultarInscricoesEstaduaisAsync(string cnpj, CancellationToken cancellationToken = default(CancellationToken));
    }
}
