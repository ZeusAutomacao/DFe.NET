using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DFe.Utils
{
    /// <summary>
    /// Classe com métodos para tratamento do CNPJ, incluindo o CNPJ alfanumérico da NT Conjunta 2025.001
    /// (12 posições [A-Z0-9] + 2 dígitos verificadores numéricos)
    /// </summary>
    public static class CnpjFiscal
    {
        private static readonly Regex FormatoCnpj = new Regex("^[A-Z0-9]{12}[0-9]{2}$", RegexOptions.Compiled);
        private static readonly Regex FormatoBaseCnpj = new Regex("^[A-Z0-9]{12}$", RegexOptions.Compiled);

        /// <summary>
        /// Calcula os dois dígitos verificadores para as 12 primeiras posições de um CNPJ,
        /// conforme a NT Conjunta 2025.001: módulo 11 sobre o valor de cada caractere (código ASCII - 48),
        /// com pesos de 2 a 9 aplicados da direita para a esquerda
        /// </summary>
        /// <param name="cnpjBase">12 primeiras posições do CNPJ ([A-Z0-9]{12})</param>
        /// <returns>Os dois dígitos verificadores, ex.: "93"</returns>
        public static string ObterDigitosVerificadores(string cnpjBase)
        {
            if (cnpjBase == null)
                throw new ArgumentNullException("cnpjBase");

            if (!FormatoBaseCnpj.IsMatch(cnpjBase))
                throw new ArgumentException(
                    string.Format("A base do CNPJ deve ter 12 posições contendo somente dígitos e letras maiúsculas; valor informado: \"{0}\".", cnpjBase),
                    "cnpjBase");

            var dv1 = CalcularDigito(cnpjBase);
            var dv2 = CalcularDigito(cnpjBase + dv1);

            return string.Concat(dv1, dv2);
        }

        /// <summary>
        /// Informa se um CNPJ de 14 posições é válido: formato [A-Z0-9]{12}[0-9]{2},
        /// dígitos verificadores corretos e diferente do CNPJ zerado (vedado pela NT Conjunta 2025.001)
        /// </summary>
        /// <param name="cnpj">CNPJ com 14 posições, sem máscara</param>
        public static bool Valido(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || !FormatoCnpj.IsMatch(cnpj))
                return false;

            //CNPJ zerado tem dígitos verificadores que conferem, mas é vedado
            if (cnpj.All(c => c == '0'))
                return false;

            return cnpj.Substring(12, 2) == ObterDigitosVerificadores(cnpj.Substring(0, 12));
        }

        private static int CalcularDigito(string valor)
        {
            var soma = 0;
            var peso = 2;

            //pesos de 2 a 9, aplicados da direita para a esquerda
            for (var i = valor.Length - 1; i != -1; i--)
            {
                //NT Conjunta 2025.001: valor do caractere = código ASCII - 48 ('0'-'9' => 0 a 9; 'A'-'Z' => 17 a 42)
                soma += (valor[i] - '0') * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }

            var resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
