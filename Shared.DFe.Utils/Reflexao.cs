/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DFe.Utils
{
    public static class Reflexao
    {
        /// <summary>
        ///     Copia o valor das propriedades comuns entre dois objetos
        /// </summary>
        /// <typeparam name="TOrigem"></typeparam>
        /// <typeparam name="TDestino"></typeparam>
        /// <param name="objetoOrigem"></param>
        /// <param name="objetoDestino"></param>
        public static void CopiarPropriedades<TDestino, TOrigem>(this TDestino objetoDestino, TOrigem objetoOrigem) where TDestino: class where TOrigem : class
        {
            foreach (var attributo in objetoOrigem.GetType().GetProperties().Where(p => p.CanRead))
            {
                var propertyInfo = objetoDestino.GetType().GetProperty(attributo.Name, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null && propertyInfo.CanWrite)
                    propertyInfo.SetValue(objetoDestino, attributo.GetValue(objetoOrigem, null), null);
            }
        }

        /// <summary>
        ///     Obtém informações de uma propriedade de um objeto.
        ///     <example>var propinfo = Funcoes.ObterPropriedadeInfo(_cfgServico, c => c.DiretorioSalvarXml);</example>
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyLambda"></param>
        /// <returns>Retorna um objeto do tipo PropertyInfo com as informações da propriedade, como nome, tipo, etc</returns>
        public static PropertyInfo ObterPropriedadeInfo<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format("A expressão '{0}' se refere a um método, não a uma propriedade!", propertyLambda));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format("A expressão '{0}' se refere a um campo, não a uma propriedade!", propertyLambda));

            if (propInfo.ReflectedType != null && (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType)))
                throw new ArgumentException(string.Format("A expressão '{0}' refere-se a uma propriedade, mas não é do tipo {1}!", propertyLambda, type));

            return propInfo;
        }

        /// <summary>
        ///     Obtém as propriedades de um determinado objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <returns>Retorna um objeto Dictionary contendo o nome da propriedade e seu valor</returns>
        public static Dictionary<string, object> LerPropriedades<T>(this T objeto) where T : class
        {
            //A função pode ser melhorada para trazer recursivamente as propriedades dos objetos filhos
            var dicionario = new Dictionary<string, object>();

            foreach (var attributo in objeto.GetType().GetProperties())
            {
                var value = attributo.GetValue(objeto, null);
                dicionario.Add(attributo.Name, value);
            }

            return dicionario;
        }

        /// <summary>
        ///     Obtém uma lista contendo os nomes das propriedades cujo valor não foi definido ou está vazio, de um determinado objeto
        ///     passado como parâmetro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <returns>Retorna uma lista de strings</returns>
        public static List<string> ObterPropriedadesEmBranco<T>(this T objeto)
        {
            return
                (from attributo in objeto.GetType().GetProperties()
                 let value = attributo.GetValue(objeto, null)
                 where value == null || string.IsNullOrEmpty(value.ToString())
                 select attributo.Name).ToList();
        }
    }
}
