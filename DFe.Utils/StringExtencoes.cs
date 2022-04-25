using System.Text.RegularExpressions;

namespace Shared.DFe.Utils
{
    public static class StringExtencoes
    {
        public static string RemoverAcentos(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            valor = Regex.Replace(valor, "[áàâãª]", "a");
            valor = Regex.Replace(valor, "[ÁÀÂÃÄ]", "A");
            valor = Regex.Replace(valor, "[éèêë]", "e");
            valor = Regex.Replace(valor, "[ÉÈÊË]", "E");
            valor = Regex.Replace(valor, "[íìîï]", "i");
            valor = Regex.Replace(valor, "[ÍÌÎÏ]", "I");
            valor = Regex.Replace(valor, "[óòôõöº]", "o");
            valor = Regex.Replace(valor, "[ÓÒÔÕÖ]", "O");
            valor = Regex.Replace(valor, "[úùûü]", "u");
            valor = Regex.Replace(valor, "[ÚÙÛÜ]", "U");
            valor = Regex.Replace(valor, "[Ç]", "C");
            valor = Regex.Replace(valor, "[ç]", "c");

            return valor;
        }
    }
}
