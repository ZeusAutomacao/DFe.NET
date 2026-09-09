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
    ///     Opcoes de preenchimento do destinatario.
    /// </summary>
    public class DestinatarioResolverOpcoes
    {
        /// <summary>
        ///     Quando verdadeiro, consulta as inscricoes estaduais (pacote 16) do CNPJ e, havendo uma
        ///     inscricao ATIVA na UF do destinatario, define <c>indIEDest = ContribuinteICMS (1)</c> e a IE.
        ///     Padrao: <c>false</c> (evita custo/consulta adicional; o caso tipico de NFC-e e consumidor).
        /// </summary>
        public bool ResolverInscricaoEstadual { get; set; }

        /// <summary>
        ///     Quando verdadeiro (padrao), define <c>indIEDest</c> automaticamente: <c>NaoContribuinte (9)</c>
        ///     para CPF e para CNPJ sem IE ativa na UF; nunca define <c>Isento (2)</c>.
        /// </summary>
        public bool DefinirIndIEDest { get; set; }

        /// <summary>
        ///     Quando verdadeiro (padrao), preenche pais como Brasil (cPais = 1058) ao preencher um endereco.
        /// </summary>
        public bool DefinirPaisBrasil { get; set; }

        /// <summary>
        ///     UF preferida para selecionar a inscricao estadual. Quando vazia, usa a UF do endereco resolvido.
        /// </summary>
        public string UfPreferidaInscricaoEstadual { get; set; }

        /// <summary>
        ///     Cria as opcoes com os padroes seguros (nao consulta IE; define indIEDest e pais Brasil).
        /// </summary>
        public DestinatarioResolverOpcoes()
        {
            ResolverInscricaoEstadual = false;
            DefinirIndIEDest = true;
            DefinirPaisBrasil = true;
        }
    }
}
