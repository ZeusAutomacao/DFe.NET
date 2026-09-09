/* Projeto: Biblioteca ZeusNFe
 * Integracao com a API cpfcnpj.com.br para preenchimento do destinatario de NF-e/NFC-e.
 *
 * Esta biblioteca e software livre; voce pode redistribui-la e/ou modifica-la sob os
 * termos da Licenca Publica Geral Menor do GNU (LGPL), versao 2.1 ou (a seu criterio)
 * qualquer versao posterior, nos mesmos termos dos demais arquivos deste projeto.
 */

using System.Text.Json;

namespace NFe.Utils.CpfCnpj
{
    /// <summary>
    ///     Leitura tolerante de propriedades opcionais de um <see cref="JsonElement" />.
    /// </summary>
    internal static class JsonElementExtensions
    {
        /// <summary>
        ///     Le uma string por qualquer um dos nomes informados (o primeiro presente e nao vazio vence).
        ///     Aceita tambem valores numericos, convertidos para o texto bruto.
        /// </summary>
        public static string LerString(this JsonElement elemento, params string[] nomes)
        {
            if (elemento.ValueKind != JsonValueKind.Object)
                return null;

            foreach (var nome in nomes)
            {
                JsonElement prop;
                if (!elemento.TryGetProperty(nome, out prop))
                    continue;

                switch (prop.ValueKind)
                {
                    case JsonValueKind.String:
                        var texto = prop.GetString();
                        if (!string.IsNullOrWhiteSpace(texto))
                            return texto.Trim();
                        break;
                    case JsonValueKind.Number:
                        return prop.GetRawText();
                }
            }
            return null;
        }

        /// <summary>
        ///     Le um inteiro longo por qualquer um dos nomes informados. Aceita numero ou string numerica.
        /// </summary>
        public static long? LerInt64(this JsonElement elemento, params string[] nomes)
        {
            if (elemento.ValueKind != JsonValueKind.Object)
                return null;

            foreach (var nome in nomes)
            {
                JsonElement prop;
                if (!elemento.TryGetProperty(nome, out prop))
                    continue;

                if (prop.ValueKind == JsonValueKind.Number)
                {
                    long valor;
                    if (prop.TryGetInt64(out valor))
                        return valor;
                }
                else if (prop.ValueKind == JsonValueKind.String)
                {
                    long valor;
                    if (long.TryParse(prop.GetString(), out valor))
                        return valor;
                }
            }
            return null;
        }

        /// <summary>
        ///     Devolve um objeto filho pelo nome, ou <c>default</c> caso nao exista ou nao seja objeto.
        /// </summary>
        public static JsonElement LerObjeto(this JsonElement elemento, string nome)
        {
            JsonElement prop;
            if (elemento.ValueKind == JsonValueKind.Object &&
                elemento.TryGetProperty(nome, out prop) &&
                prop.ValueKind == JsonValueKind.Object)
                return prop;
            return default(JsonElement);
        }
    }
}
