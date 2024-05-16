using System;
using System.Text.RegularExpressions;

namespace Shared.DFe.Utils
{
    public static class StringExtensoes
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

        public static string RemoverDeclaracaoXml(this string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return xml;

            var posIni = xml.IndexOf("<?", StringComparison.Ordinal);
            if (posIni < 0) 
                return xml;

            var posFinal = xml.IndexOf("?>", StringComparison.Ordinal);
            return posFinal < 0 ? xml : xml.Remove(posIni, (posFinal + 2) - posIni);
        }

    }
}
