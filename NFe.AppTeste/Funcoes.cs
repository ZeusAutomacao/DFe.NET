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
using System.Windows;
using Microsoft.Win32;

namespace NFe.AppTeste
{
    public static class Funcoes
    {
        /// <summary>
        ///     Exibe um diálogo com uma mensagem para o usuário, utilizando um ModernDialog
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="titulo"></param>
        /// <param name="botoes"></param>
        public static void Mensagem(string mensagem, string titulo, MessageBoxButton botoes)
        {
            MessageBox.Show(mensagem, titulo, botoes);
        }

        /// <summary>
        ///     Obtém as propriedades de um determinado objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <returns>Retorna um objeto Dictionary contendo o nome da propriedade e seu valor</returns>
        public static Dictionary<string, object> LerPropriedades<T>(T objeto) where T : class
        {
            //A função pode ser melhorada para trazer recursivamente as proprieades dos objetos filhos
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
        public static List<string> ObterPropriedadesEmBranco<T>(T objeto)
        {
            return
                (from attributo in objeto.GetType().GetProperties()
                    let value = attributo.GetValue(objeto, null)
                    where value == null || string.IsNullOrEmpty(value.ToString())
                    select attributo.Name).ToList();
        }

        /// <summary>
        ///     Abre um dialógo para o usuário digitar algo
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="titulo"></param>
        /// <param name="descricao"></param>
        /// <param name="valorPadrao"></param>
        /// <returns>Retorna o texto digitado pelo usuário</returns>
        public static string InpuBox(Window owner, string titulo, string descricao, string valorPadrao = "")
        {
            var inputBox = new InputBoxWindow
            {
                Title = titulo,
                TxtDescricao = {Text = descricao},
                TxtValor = {Text = valorPadrao},
                Owner = owner
            };
            inputBox.ShowDialog();
            return inputBox.TxtValor.Text;
        }

        /// <summary>
        ///     Copia o valor das propriedades de um objeto para outro de estrutura igual
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objetoOrigem"></param>
        /// <param name="objetoDestino"></param>
        public static void CopiarPropriedades<T>(T objetoOrigem, T objetoDestino) where T : class
        {
            foreach (var attributo in objetoOrigem.GetType().GetProperties())
            {
                var propertyInfo = objetoDestino.GetType().GetProperty(attributo.Name, BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo != null)
                    propertyInfo.SetValue(objetoDestino, attributo.GetValue(objetoOrigem, null), null);
            }
        }

        /// <summary>
        ///     Abre o diálogo de busca de arquivo com o filtro configurado para arquivos do tipo ".xml"
        /// </summary>
        /// <returns></returns>
        public static string BuscarArquivoXml()
        {
            var dlg = new OpenFileDialog
            {
                FileName = "Arquivo XML",
                DefaultExt = ".xml",
                Filter = "Arquivo XML (.xml)|*.xml"
            };
            var result = dlg.ShowDialog();
            if (result != true)
                throw new Exception("");
            return dlg.FileName;
        }

        /// <summary>
        ///     Abre o diálogo de busca de arquivo com o filtro configurado para arquivos do tipo ".png"
        /// </summary>
        /// <returns></returns>
        public static string BuscarImagem()
        {
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".png",
                Filter = "PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp|JPEG (*.jpeg)|*.jpeg|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif"
            };
            dlg.ShowDialog();            
            return dlg.FileName;
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
        public static PropertyInfo ObterPropriedadeInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof (TSource);

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
    }
}