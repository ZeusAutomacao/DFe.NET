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
    ///     Inscricao estadual de um CNPJ, associada a uma UF.
    /// </summary>
    public class InscricaoEstadual
    {
        /// <summary>
        ///     Numero da inscricao estadual (apenas digitos, conforme exigido no campo IE da NF-e).
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        ///     Indica se a inscricao esta ativa.
        /// </summary>
        public bool Ativa { get; set; }

        /// <summary>
        ///     Sigla da UF da inscricao.
        /// </summary>
        public string Uf { get; set; }

        /// <summary>
        ///     Codigo IBGE do estado (2 digitos). Nulo quando indisponivel.
        /// </summary>
        public int? CodigoEstadoIbge { get; set; }
    }
}
