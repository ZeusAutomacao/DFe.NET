/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System;
using System.Text;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Utilitarios para normalizacao e classificacao de CPF/CNPJ.
    /// </summary>
    public static class DocumentoUtil
    {
        /// <summary>
        ///     Remove qualquer caractere de formatacao (ponto, barra, hifen, espaco) mantendo apenas
        ///     digitos. Usado em campos estritamente numericos (CPF, CEP, inscricao estadual).
        /// </summary>
        public static string SomenteDigitos(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return string.Empty;

            var sb = new StringBuilder(valor.Length);
            foreach (var c in valor)
            {
                if (c >= '0' && c <= '9')
                    sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Normaliza um CPF/CNPJ removendo a formatacao e mantendo letras (o CNPJ alfanumerico,
        ///     vigente desde 2026, tem 12 posicoes alfanumericas seguidas de 2 digitos verificadores).
        ///     As letras sao colocadas em maiusculo, como esperado pela SEFAZ e pela API.
        /// </summary>
        public static string Normalizar(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return string.Empty;

            var sb = new StringBuilder(documento.Length);
            foreach (var c in documento)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z'))
                    sb.Append(c);
                else if (c >= 'a' && c <= 'z')
                    sb.Append(char.ToUpperInvariant(c));
            }
            return sb.ToString();
        }

        /// <summary>
        ///     Determina se o documento e CPF (11 posicoes) ou CNPJ (14 posicoes). Aceita CNPJ
        ///     alfanumerico. Espera o documento ja normalizado por <see cref="Normalizar" />.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     Lancada quando o documento nao possui 11 nem 14 posicoes.
        /// </exception>
        public static TipoPessoa DeterminarTipo(string documentoNormalizado)
        {
            if (documentoNormalizado == null)
                throw new ArgumentNullException(nameof(documentoNormalizado));

            switch (documentoNormalizado.Length)
            {
                case 11:
                    return TipoPessoa.Fisica;
                case 14:
                    return TipoPessoa.Juridica;
                default:
                    throw new ArgumentException(
                        string.Format("Documento invalido: esperado 11 (CPF) ou 14 (CNPJ) posicoes, recebido {0}.",
                            documentoNormalizado.Length),
                        nameof(documentoNormalizado));
            }
        }
    }
}
