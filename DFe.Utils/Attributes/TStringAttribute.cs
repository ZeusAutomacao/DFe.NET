using System;
using System.Text.RegularExpressions;

namespace DFe.Utils.Attributes
{
    /// <summary>
    /// Indica que esta propriedade, corresponde ao tipo 'TString' (Tipo string genérico), do schema da NF-e ([!-ÿ]{1}[ -ÿ]{0,}[!-ÿ]{1}|[!-ÿ]{1})
    /// <para></para>
    /// 
    /// Utilize este atributo, para forçar a correção dos dados enviados nesta propriedade
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TStringAttribute : Attribute
    {
        /// <summary>
        /// Remove caracteres inválidos, de acordo com o tipo: TString (Tipo string genérico) do arquivo 'tiposBasico_v4.00.xsd'
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static string CorrigirValor(string valor)
        {
            // Qualquer string que comece e termine com um ou mais caracteres no intervalo de ! (33) a ÿ (255) ou espaços em branco.
            var regex = new Regex(@"[^!-ÿ\s]+");

            // Substitui todos os caracteres que não correspondem ao padrão por uma string vazia,
            // removendo todos os caracteres inválidos
            valor = regex.Replace(valor, "");

            // Remove espaços no início e no final da string
            valor = valor.Trim();

            return valor;
        }
    }
}