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
    ///     Dados cadastrais de uma pessoa (fisica ou juridica) retornados pela consulta,
    ///     ja normalizados e independentes do formato bruto da API.
    /// </summary>
    public class PessoaResultado
    {
        /// <summary>
        ///     Indica se a consulta localizou o documento (status = 1 na API).
        /// </summary>
        public bool Encontrado { get; set; }

        /// <summary>
        ///     Documento consultado, apenas digitos.
        /// </summary>
        public string Documento { get; set; }

        /// <summary>
        ///     CPF ou CNPJ.
        /// </summary>
        public TipoPessoa Tipo { get; set; }

        /// <summary>
        ///     Nome (pessoa fisica) ou razao social (pessoa juridica).
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        ///     Nome fantasia (apenas pessoa juridica). Nulo para pessoa fisica.
        /// </summary>
        public string NomeFantasia { get; set; }

        /// <summary>
        ///     Indica se a pessoa juridica e optante pelo Simples Nacional, quando o pacote
        ///     consultado fornece o dado. Nulo quando indisponivel.
        /// </summary>
        public bool? OptanteSimplesNacional { get; set; }

        /// <summary>
        ///     Endereco principal. Pode ser nulo quando o pacote consultado nao devolve endereco.
        /// </summary>
        public EnderecoResultado Endereco { get; set; }
    }

    /// <summary>
    ///     Endereco normalizado retornado pela consulta.
    /// </summary>
    public class EnderecoResultado
    {
        /// <summary>Logradouro.</summary>
        public string Logradouro { get; set; }

        /// <summary>Numero.</summary>
        public string Numero { get; set; }

        /// <summary>Complemento.</summary>
        public string Complemento { get; set; }

        /// <summary>Bairro.</summary>
        public string Bairro { get; set; }

        /// <summary>CEP, apenas digitos (pode vir vazio ou incompleto).</summary>
        public string Cep { get; set; }

        /// <summary>Nome do municipio.</summary>
        public string Municipio { get; set; }

        /// <summary>Sigla da UF.</summary>
        public string Uf { get; set; }

        /// <summary>
        ///     Codigo IBGE do municipio (7 digitos). Nulo quando indisponivel.
        /// </summary>
        public long? CodigoMunicipioIbge { get; set; }
    }
}
