/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using NFe.Classes.Informacoes.Destinatario;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Preenche um <see cref="dest" /> (e o <see cref="enderDest" /> associado) a partir de um CPF/CNPJ,
    ///     usando uma fonte <see cref="IPessoaLookup" />. Foco em NFC-e e destinatario consumidor.
    ///     <para>
    ///         Regras de seguranca: preenche apenas campos vazios (nunca sobrescreve dado informado pelo
    ///         integrador); define somente CPF ou CNPJ (nunca ambos); e nunca marca o destinatario como Isento.
    ///     </para>
    /// </summary>
    public class DestinatarioResolver
    {
        private const int CodigoPaisBrasil = 1058;
        private const string NomePaisBrasil = "Brasil";

        private readonly IPessoaLookup _lookup;

        /// <summary>Cria o resolver com uma fonte de consulta.</summary>
        public DestinatarioResolver(IPessoaLookup lookup)
        {
            if (lookup == null)
                throw new ArgumentNullException(nameof(lookup));
            _lookup = lookup;
        }

        /// <summary>
        ///     Preenche o destinatario a partir do documento informado. Retorna o proprio objeto para encadeamento.
        /// </summary>
        /// <param name="destinatario">Destinatario a preencher (nao nulo).</param>
        /// <param name="documento">CPF ou CNPJ, com ou sem formatacao.</param>
        /// <param name="opcoes">Opcoes de preenchimento; quando nulo usa os padroes seguros.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        public async Task<dest> PreencherAsync(
            dest destinatario,
            string documento,
            DestinatarioResolverOpcoes opcoes = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (destinatario == null)
                throw new ArgumentNullException(nameof(destinatario));

            opcoes = opcoes ?? new DestinatarioResolverOpcoes();

            var documentoNormalizado = DocumentoUtil.Normalizar(documento);
            var tipo = DocumentoUtil.DeterminarTipo(documentoNormalizado);

            var pessoa = await _lookup.ConsultarAsync(documentoNormalizado, cancellationToken).ConfigureAwait(false);
            if (pessoa == null || !pessoa.Encontrado)
                return destinatario;

            DefinirDocumento(destinatario, documentoNormalizado, tipo);

            if (string.IsNullOrEmpty(destinatario.xNome) && !string.IsNullOrEmpty(pessoa.Nome))
                destinatario.xNome = pessoa.Nome;

            if (pessoa.Endereco != null)
                PreencherEndereco(destinatario, pessoa.Endereco, opcoes);

            await DefinirIndicadorIeAsync(destinatario, documentoNormalizado, tipo, opcoes, cancellationToken).ConfigureAwait(false);

            return destinatario;
        }

        private static void DefinirDocumento(dest destinatario, string documento, TipoPessoa tipo)
        {
            // So define quando nenhuma identidade foi informada, evitando o ArgumentException do setter.
            if (!string.IsNullOrEmpty(destinatario.CPF) ||
                !string.IsNullOrEmpty(destinatario.CNPJ) ||
                !string.IsNullOrEmpty(destinatario.idEstrangeiro))
                return;

            if (tipo == TipoPessoa.Fisica)
                destinatario.CPF = documento;
            else
                destinatario.CNPJ = documento;
        }

        private static void PreencherEndereco(dest destinatario, EnderecoResultado origem, DestinatarioResolverOpcoes opcoes)
        {
            var endereco = destinatario.enderDest;
            if (endereco == null)
            {
                endereco = new enderDest();
                destinatario.enderDest = endereco;
            }

            if (string.IsNullOrEmpty(endereco.xLgr) && !string.IsNullOrEmpty(origem.Logradouro))
                endereco.xLgr = origem.Logradouro;
            if (string.IsNullOrEmpty(endereco.nro) && !string.IsNullOrEmpty(origem.Numero))
                endereco.nro = origem.Numero;
            if (string.IsNullOrEmpty(endereco.xCpl) && !string.IsNullOrEmpty(origem.Complemento))
                endereco.xCpl = origem.Complemento;
            if (string.IsNullOrEmpty(endereco.xBairro) && !string.IsNullOrEmpty(origem.Bairro))
                endereco.xBairro = origem.Bairro;
            if (string.IsNullOrEmpty(endereco.xMun) && !string.IsNullOrEmpty(origem.Municipio))
                endereco.xMun = origem.Municipio;
            if (string.IsNullOrEmpty(endereco.UF) && !string.IsNullOrEmpty(origem.Uf))
                endereco.UF = origem.Uf;

            // cMun e long; 0 significa nao informado.
            if (endereco.cMun == 0 && origem.CodigoMunicipioIbge.HasValue && origem.CodigoMunicipioIbge.Value > 0)
                endereco.cMun = origem.CodigoMunicipioIbge.Value;

            // O setter de CEP exige exatamente 8 digitos; so atribui quando valido.
            if (string.IsNullOrEmpty(endereco.CEP))
            {
                var cep = DocumentoUtil.SomenteDigitos(origem.Cep);
                if (cep.Length == 8)
                    endereco.CEP = cep;
            }

            // Este resolver trata apenas de documentos brasileiros (CPF/CNPJ), portanto o pais
            // e sempre Brasil quando ha um endereco a preencher, independente da UF ter vindo.
            if (opcoes.DefinirPaisBrasil)
            {
                if (!endereco.cPais.HasValue)
                    endereco.cPais = CodigoPaisBrasil;
                if (string.IsNullOrEmpty(endereco.xPais))
                    endereco.xPais = NomePaisBrasil;
            }
        }

        private async Task DefinirIndicadorIeAsync(
            dest destinatario,
            string documento,
            TipoPessoa tipo,
            DestinatarioResolverOpcoes opcoes,
            CancellationToken cancellationToken)
        {
            if (!opcoes.DefinirIndIEDest || destinatario.indIEDest.HasValue)
                return;

            // Pessoa fisica e sempre nao contribuinte.
            if (tipo == TipoPessoa.Fisica)
            {
                destinatario.indIEDest = indIEDest.NaoContribuinte;
                return;
            }

            if (opcoes.ResolverInscricaoEstadual)
            {
                var uf = ObterUfPreferida(destinatario, opcoes);
                var inscricao = await SelecionarInscricaoAtivaAsync(documento, uf, cancellationToken).ConfigureAwait(false);
                if (inscricao != null)
                {
                    destinatario.indIEDest = indIEDest.ContribuinteICMS;
                    if (string.IsNullOrEmpty(destinatario.IE))
                        destinatario.IE = inscricao.Numero;
                    return;
                }
            }

            // Sem IE ativa confirmada na UF: trata como nao contribuinte (nunca Isento).
            destinatario.indIEDest = indIEDest.NaoContribuinte;
        }

        private async Task<InscricaoEstadual> SelecionarInscricaoAtivaAsync(string cnpj, string uf, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(uf))
                return null;

            var inscricoes = await _lookup.ConsultarInscricoesEstaduaisAsync(cnpj, cancellationToken).ConfigureAwait(false);
            if (inscricoes == null)
                return null;

            // So habilita indIEDest = 1 quando ha uma inscricao ATIVA na UF do destinatario.
            foreach (var ie in inscricoes)
            {
                if (ie == null || string.IsNullOrEmpty(ie.Numero) || !ie.Ativa)
                    continue;
                if (string.Equals(ie.Uf, uf, StringComparison.OrdinalIgnoreCase))
                    return ie;
            }

            return null;
        }

        private static string ObterUfPreferida(dest destinatario, DestinatarioResolverOpcoes opcoes)
        {
            if (!string.IsNullOrWhiteSpace(opcoes.UfPreferidaInscricaoEstadual))
                return opcoes.UfPreferidaInscricaoEstadual.Trim();
            if (destinatario.enderDest != null && !string.IsNullOrWhiteSpace(destinatario.enderDest.UF))
                return destinatario.enderDest.UF.Trim();
            return null;
        }
    }
}
