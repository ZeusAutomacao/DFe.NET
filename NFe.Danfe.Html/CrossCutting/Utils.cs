using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace NFe.Danfe.Html.CrossCutting
{
    internal static class Utils
    {

        /// <summary>
        /// Converter um tipo DateTime absoluto para o fuso horário do Brasil
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DateTime ConverterFusoHorarioBrasil(this DateTimeOffset data)
        {
            var utcData = data.UtcDateTime;

            return utcData.ConverterFusoHorarioBrasil();
        }

        /// <summary>
        /// Obter o fuso horário do Brasil a partir de um fuso UTC
        /// </summary>
        /// <returns></returns>
        public static DateTime ConverterFusoHorarioBrasil(this DateTime data)
        {
            try
            {
                if (data.Kind != DateTimeKind.Utc)
                    return data;

                TimeZoneInfo horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(data, horaBrasilia);
            }
            catch (Exception ex)
            { 
                return DateTime.Now;

            } 

        }

        private static readonly Hashtable CacheSerializers = new Hashtable();

        private static XmlSerializer BuscarNoCache(string chave, Type type)
        {
            if (CacheSerializers.Contains(chave)) return (XmlSerializer)CacheSerializers[chave];
            var ser = XmlSerializer.FromTypes(new[] { type })[0];
            CacheSerializers.Add(chave, ser);
            return ser;
        }

        public static T XmlStringParaClasse<T>(string input) where T : class
        {
            var keyNomeClasseEmUso = typeof(T).FullName;
            var ser = BuscarNoCache(keyNomeClasseEmUso, typeof(T));
            using (var sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        /// <summary>
        ///     Deletar arquivo
        /// </summary>
        /// <param name="caminho">Caminho do arquivo</param>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        public static void DelatarArquivo(string caminho, string nomeArquivo)
        {
            var c1 = Path.Combine(caminho, nomeArquivo);
            File.Delete(c1);
        }

        /// <summary>
        ///     Escrever arquivo em modo stream, assincronamente
        /// </summary>
        /// <param name="caminho">Caminho do arquivo</param>
        /// <param name="nomeArquivo">Nome do arquivo</param>
        /// <param name="conteudo">Conteudo</param>
        public static void EscreverArquivo(string caminho, string nomeArquivo, string conteudo)
        {
            if (caminho == null)
                throw new ArgumentNullException(nameof(caminho), "O caminho do arquivo deve ser informado");
            if (nomeArquivo == null)
                throw new ArgumentNullException(nameof(nomeArquivo), "O nome do arquivo deve ser informado");
            if (conteudo == null)
                throw new ArgumentNullException(nameof(conteudo), "O conteúdo do arquivo deve ser informado");
            CriarPastaSeNaoExistir(caminho);
            var encodedText = Encoding.UTF8.GetBytes(conteudo);
            var c1 = Path.Combine(caminho, nomeArquivo); // Combina caminho do arquivo como nome do arquivo
            using (var sourceStream = new FileStream(c1, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
            {
                sourceStream.Write(encodedText, 0, encodedText.Length);
                sourceStream.Close();
            }
        }

        /// <summary>
        ///     Criar pasta no caminho indicador
        /// </summary>
        /// <param name="caminho">caminho do arquivo</param>
        public static bool CriarPasta(string caminho)
        {
            // Tentar criar diretorio
            Directory.CreateDirectory(caminho);
            return true;
        }

        /// <summary>
        ///     Criar pasta se não existir
        /// </summary>
        /// <param name="caminho">Caminho do arquivo</param>
        /// <returns></returns>
        public static void CriarPastaSeNaoExistir(string caminho)
        {
            if (!ExisteDiretorio(caminho)) CriarPasta(caminho);
        }

        /// <summary>
        ///     Verificar se o diretorio existe
        /// </summary>
        /// <param name="caminho">caminho do arquivo</param>
        /// <returns></returns>
        public static bool ExisteDiretorio(string caminho)
        {
            // Determinar se o diretorio existe
            if (Directory.Exists(caminho))
                return true;
            return false;
        }

        /// <summary>
        /// Obter a data do Brasil
        /// </summary>
        /// <returns></returns>
        public static DateTime ObterDataBrasil()
        {

            CultureInfo.CurrentCulture.ClearCachedData();
            DateTime dateTime = DateTime.UtcNow;
            TimeZoneInfo horaBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, horaBrasilia);

        }

        /// <summary>
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lengthAfterSpace"></param>
        /// <returns></returns>
        public static string FormatarEspaco(this string text, int lengthAfterSpace)
        {
            var empty = string.Empty;
            var num = 1;
            foreach (var ch in text)
            {
                empty += ch.ToString();
                if (num == lengthAfterSpace)
                {
                    empty += " ";
                    num = 1;
                }
                else
                {
                    ++num;
                }
            }

            return empty.Trim();
        }

        /// <summary>
        /// Formatar número de telefone
        /// </summary>
        /// <param name="numeroTelefone"></param>
        /// <returns></returns>
        public static string FormatarTelefone(this string numeroTelefone)
        {
            string numeroApenasDigitos = new string(numeroTelefone.Where(char.IsDigit).ToArray());

            if (numeroApenasDigitos.Length == 10 || numeroApenasDigitos.Length == 11)
            {
                string ddd = numeroApenasDigitos.Substring(0, 2);
                string numero = numeroApenasDigitos.Substring(numeroApenasDigitos.Length - 8);

                return string.Format("({0}) {1}-{2}", ddd, numero.Substring(0, 4), numero.Substring(4));
            }

            return numeroTelefone;
        }

        /// <summary>
        ///     Formatar CNPJ ou CPF
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string FormatarCnpjCpf(string doc)
        {
            if (string.IsNullOrEmpty(doc))
                return doc;
            var empty = string.Empty;
            if (doc.Trim().Length < 12)
            {
                doc = doc.Trim();
                return Regex.Replace(doc, "(\\w{3})(\\w{3})(\\w{3})(\\w{2})", "$1.$2.$3-$4");
            }

            doc = doc.Trim();
            return Regex.Replace(doc, "(\\w{2})(\\w{3})(\\w{3})(\\w{4})(\\w{2})", "$1.$2.$3/$4-$5");
        }

        /// <summary>
        ///     Formatar numero
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatarNumero(this decimal value)
        {
            return $"{(object)value:0.00}".Replace(",", ".").Trim();
        }

        /// <summary>
        ///     Formatar numero para exibição da DANFE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatarNumeroDanfe(this decimal value)
        {
            try
            {
                var str = FormatarNumero(value);
                str = str.Replace(".", ",");
                double.TryParse(str, out var result);
                return result.ToString("N", CultureInfo.GetCultureInfo("pt-br"));
            }
            catch (Exception)
            {
                return "0,00";
            }

        }

        /// <summary>
        ///     Trunca string
        /// </summary>
        /// <param name="value">string</param>
        /// <param name="maxLength">Quantidade máxima de caracteres para exibição</param>
        /// <returns></returns>
        public static string Truncar(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        ///     Formatar numero para exibição da DANFE
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FormatarNumeroQuantidadeDanfe(this decimal value)
        {
            var str = FormatarNumero(value);
            str = str.Replace(".", ",");
            double.TryParse(str, out var result);
            return result.ToString("#.000").Trim();
        }

    }
}
