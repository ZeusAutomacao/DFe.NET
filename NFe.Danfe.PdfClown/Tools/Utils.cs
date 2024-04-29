using System.Text.RegularExpressions;

namespace NFe.Danfe.PdfClown.Tools
{
    public static class Utils
    {
        /// <summary>
        /// Verifica se uma string contém outra string no formato chave: valor.
        /// </summary>
        public static bool StringContemChaveValor(string str, string chave, string valor)
        {
            if (string.IsNullOrWhiteSpace(chave)) throw new ArgumentException(nameof(chave));
            if (string.IsNullOrWhiteSpace(str)) return false;

            return Regex.IsMatch(str, $@"({chave}):?\s*{valor}", RegexOptions.IgnoreCase);
        }

        public static string TipoDFeDeChaveAcesso(string chaveAcesso)
        {
            if (string.IsNullOrWhiteSpace(chaveAcesso)) throw new ArgumentException(nameof(chaveAcesso));

            if (chaveAcesso.Length == 44)
            {
                string f = chaveAcesso.Substring(20, 2);

                if (f == "55") return "NF-e";
                else if (f == "57") return "CT-e";
                else if (f == "65") return "NFC-e";
            }

            return "DF-e Desconhecido";
        }
    }
}
