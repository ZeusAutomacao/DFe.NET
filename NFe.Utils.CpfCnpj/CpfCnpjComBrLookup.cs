/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Implementacao de <see cref="IPessoaLookup" /> que consome a API REST da cpfcnpj.com.br.
    ///     <para>
    ///         Padrao de rota: <c>{urlBase}/{token}/{pacote}/{documento}</c>. Pacotes usados:
    ///         3 (CPF nome + endereco), 5 (CNPJ razao + endereco) e 16 (inscricoes estaduais).
    ///     </para>
    /// </summary>
    public class CpfCnpjComBrLookup : IPessoaLookup, IDisposable
    {
        /// <summary>URL base padrao da API.</summary>
        public const string UrlBasePadrao = "https://api.cpfcnpj.com.br";

        private readonly HttpClient _httpClient;
        private readonly bool _httpClientProprio;
        private readonly string _token;
        private readonly string _urlBase;
        private readonly int _pacoteCpf;
        private readonly int _pacoteCnpj;
        private readonly int _pacoteInscricaoEstadual;

        /// <summary>
        ///     Cria o cliente com um token de acesso. Um <see cref="HttpClient" /> proprio pode ser
        ///     informado (recomendado reutiliza-lo na aplicacao); caso contrario um e criado internamente.
        /// </summary>
        /// <param name="token">Token de acesso da conta cpfcnpj.com.br.</param>
        /// <param name="httpClient">HttpClient opcional a reutilizar.</param>
        /// <param name="urlBase">URL base opcional (util para testes/homologacao).</param>
        /// <param name="pacoteCpf">Pacote usado para CPF (padrao 3: nome + endereco).</param>
        /// <param name="pacoteCnpj">Pacote usado para CNPJ (padrao 5: razao + endereco).</param>
        /// <param name="pacoteInscricaoEstadual">Pacote usado para inscricoes estaduais (padrao 16).</param>
        public CpfCnpjComBrLookup(
            string token,
            HttpClient httpClient = null,
            string urlBase = UrlBasePadrao,
            int pacoteCpf = 3,
            int pacoteCnpj = 5,
            int pacoteInscricaoEstadual = 16)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token e obrigatorio.", nameof(token));

            _token = token;
            _httpClientProprio = httpClient == null;
            _httpClient = httpClient ?? new HttpClient();
            _urlBase = string.IsNullOrWhiteSpace(urlBase) ? UrlBasePadrao : urlBase.TrimEnd('/');
            _pacoteCpf = pacoteCpf;
            _pacoteCnpj = pacoteCnpj;
            _pacoteInscricaoEstadual = pacoteInscricaoEstadual;
        }

        /// <inheritdoc />
        public async Task<PessoaResultado> ConsultarAsync(string documento, CancellationToken cancellationToken = default(CancellationToken))
        {
            var normalizado = DocumentoUtil.Normalizar(documento);
            var tipo = DocumentoUtil.DeterminarTipo(normalizado);
            var pacote = tipo == TipoPessoa.Fisica ? _pacoteCpf : _pacoteCnpj;

            using (var doc = await ObterJsonAsync(pacote, normalizado, cancellationToken).ConfigureAwait(false))
            {
                var raiz = doc.RootElement;
                var resultado = new PessoaResultado
                {
                    Documento = normalizado,
                    Tipo = tipo,
                    Encontrado = LerStatus(raiz) == 1
                };

                if (!resultado.Encontrado)
                    return resultado;

                if (tipo == TipoPessoa.Fisica)
                    PreencherPessoaFisica(raiz, resultado);
                else
                    PreencherPessoaJuridica(raiz, resultado);

                return resultado;
            }
        }

        /// <inheritdoc />
        public async Task<IList<InscricaoEstadual>> ConsultarInscricoesEstaduaisAsync(string cnpj, CancellationToken cancellationToken = default(CancellationToken))
        {
            var normalizado = DocumentoUtil.Normalizar(cnpj);
            if (DocumentoUtil.DeterminarTipo(normalizado) != TipoPessoa.Juridica)
                throw new ArgumentException("Inscricao estadual so se aplica a CNPJ.", nameof(cnpj));

            var lista = new List<InscricaoEstadual>();
            using (var doc = await ObterJsonAsync(_pacoteInscricaoEstadual, normalizado, cancellationToken).ConfigureAwait(false))
            {
                var raiz = doc.RootElement;
                if (LerStatus(raiz) != 1)
                    return lista;

                JsonElement inscricoes;
                if (!raiz.TryGetProperty("inscricoesEstaduais", out inscricoes) ||
                    inscricoes.ValueKind != JsonValueKind.Array)
                    return lista;

                foreach (var item in inscricoes.EnumerateArray())
                {
                    var estado = item.LerObjeto("estado");
                    lista.Add(new InscricaoEstadual
                    {
                        Numero = DocumentoUtil.SomenteDigitos(item.LerString("inscricao_estadual", "inscricaoEstadual")),
                        Ativa = LerBool(item, "ativo", "ativa"),
                        Uf = estado.LerString("sigla", "uf"),
                        CodigoEstadoIbge = (int?)estado.LerInt64("ibge_id")
                    });
                }
            }
            return lista;
        }

        private static void PreencherPessoaFisica(JsonElement raiz, PessoaResultado resultado)
        {
            resultado.Nome = raiz.LerString("nome");

            var endereco = new EnderecoResultado
            {
                Logradouro = raiz.LerString("logradouro", "endereco"),
                Numero = raiz.LerString("numero"),
                Complemento = raiz.LerString("complemento"),
                Bairro = raiz.LerString("bairro"),
                Cep = DocumentoUtil.SomenteDigitos(raiz.LerString("cep")),
                Municipio = raiz.LerString("cidade", "municipio"),
                Uf = raiz.LerString("uf"),
                CodigoMunicipioIbge = raiz.LerInt64("ibge")
            };

            // Alguns cadastros trazem o endereco apenas no vetor "enderecos".
            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                JsonElement enderecos;
                if (raiz.TryGetProperty("enderecos", out enderecos) &&
                    enderecos.ValueKind == JsonValueKind.Array)
                {
                    foreach (var e in enderecos.EnumerateArray())
                    {
                        endereco.Logradouro = e.LerString("logradouro", "endereco");
                        endereco.Numero = e.LerString("numero");
                        endereco.Complemento = e.LerString("complemento");
                        endereco.Bairro = e.LerString("bairro");
                        endereco.Cep = DocumentoUtil.SomenteDigitos(e.LerString("cep"));
                        endereco.Municipio = e.LerString("cidade", "municipio");
                        endereco.Uf = e.LerString("uf");
                        endereco.CodigoMunicipioIbge = e.LerInt64("ibge");
                        break;
                    }
                }
            }

            if (TemDadosDeEndereco(endereco))
                resultado.Endereco = endereco;
        }

        private static void PreencherPessoaJuridica(JsonElement raiz, PessoaResultado resultado)
        {
            resultado.Nome = raiz.LerString("razao", "nome");
            resultado.NomeFantasia = raiz.LerString("fantasia");

            // O pacote de CNPJ traz o endereco da matriz em "matrizEndereco" e o codigo IBGE
            // da mesma cidade da matriz em "ibge.cidade.ibge_id" (o proprio "matrizEndereco" nao
            // carrega o codigo). Ambos descrevem a matriz, portanto sao consistentes entre si.
            var ibge = raiz.LerObjeto("ibge");
            var cidadeIbge = ibge.LerObjeto("cidade");
            var codigoMunicipio = cidadeIbge.LerInt64("ibge_id");

            var matriz = raiz.LerObjeto("matrizEndereco");
            if (matriz.ValueKind == JsonValueKind.Object)
            {
                var endereco = new EnderecoResultado
                {
                    Logradouro = matriz.LerString("logradouro", "endereco"),
                    Numero = matriz.LerString("numero"),
                    Complemento = matriz.LerString("complemento"),
                    Bairro = matriz.LerString("bairro"),
                    Cep = DocumentoUtil.SomenteDigitos(matriz.LerString("cep")),
                    Municipio = matriz.LerString("cidade", "municipio"),
                    Uf = matriz.LerString("uf"),
                    CodigoMunicipioIbge = codigoMunicipio
                };
                if (TemDadosDeEndereco(endereco))
                    resultado.Endereco = endereco;
            }

            var simples = raiz.LerObjeto("simplesNacional");
            var optante = simples.LerString("optante");
            if (!string.IsNullOrEmpty(optante))
                resultado.OptanteSimplesNacional = optante.Trim().StartsWith("S", StringComparison.OrdinalIgnoreCase);
        }

        private static bool TemDadosDeEndereco(EnderecoResultado e)
        {
            return !string.IsNullOrEmpty(e.Logradouro) ||
                   !string.IsNullOrEmpty(e.Municipio) ||
                   !string.IsNullOrEmpty(e.Cep) ||
                   e.CodigoMunicipioIbge.HasValue;
        }

        private async Task<JsonDocument> ObterJsonAsync(int pacote, string documento, CancellationToken cancellationToken)
        {
            var url = string.Format("{0}/{1}/{2}/{3}",
                _urlBase,
                Uri.EscapeDataString(_token),
                pacote,
                Uri.EscapeDataString(documento));

            HttpResponseMessage resposta;
            try
            {
                resposta = await _httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);
            }
            catch (HttpRequestException ex)
            {
                throw new CpfCnpjLookupException("Falha de comunicacao com a API cpfcnpj.com.br.", ex);
            }
            catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
            {
                // Timeout do HttpClient (nao um cancelamento do chamador).
                throw new CpfCnpjLookupException("Tempo esgotado ao consultar a API cpfcnpj.com.br.");
            }

            using (resposta)
            {
                var conteudo = await resposta.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!resposta.IsSuccessStatusCode)
                    throw new CpfCnpjLookupException(
                        string.Format("A API respondeu HTTP {0} para o pacote {1}. {2}",
                            (int)resposta.StatusCode, pacote, Resumir(conteudo)),
                        ExtrairErroCodigo(conteudo));

                try
                {
                    return JsonDocument.Parse(conteudo);
                }
                catch (JsonException ex)
                {
                    throw new CpfCnpjLookupException("Resposta da API nao e um JSON valido.", ex);
                }
            }
        }

        private static string Resumir(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
                return string.Empty;
            conteudo = conteudo.Trim();
            return conteudo.Length > 300 ? conteudo.Substring(0, 300) : conteudo;
        }

        private static int? ExtrairErroCodigo(string conteudo)
        {
            if (string.IsNullOrWhiteSpace(conteudo))
                return null;
            try
            {
                using (var doc = JsonDocument.Parse(conteudo))
                    return (int?)doc.RootElement.LerInt64("erroCodigo", "erro_codigo");
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private static int LerStatus(JsonElement raiz)
        {
            var status = raiz.LerInt64("status");
            return status.HasValue ? (int)status.Value : 0;
        }

        private static bool LerBool(JsonElement elemento, params string[] nomes)
        {
            foreach (var nome in nomes)
            {
                JsonElement prop;
                if (elemento.ValueKind == JsonValueKind.Object && elemento.TryGetProperty(nome, out prop))
                {
                    if (prop.ValueKind == JsonValueKind.True) return true;
                    if (prop.ValueKind == JsonValueKind.False) return false;
                    if (prop.ValueKind == JsonValueKind.String)
                    {
                        bool valor;
                        if (bool.TryParse(prop.GetString(), out valor))
                            return valor;
                    }
                }
            }
            return false;
        }

        /// <summary>
        ///     Libera o <see cref="HttpClient" /> apenas quando ele foi criado internamente.
        ///     Um HttpClient informado pelo chamador permanece sob a responsabilidade dele.
        /// </summary>
        public void Dispose()
        {
            if (_httpClientProprio)
                _httpClient.Dispose();
        }
    }
}
