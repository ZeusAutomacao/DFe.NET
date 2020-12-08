/********************************************************************************/
/* Projeto: Biblioteca ZeusDFe                                                  */
/* Biblioteca C# para auxiliar no desenvolvimento das demais bibliotecas DFe    */
/*                                                                              */
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
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DFe.Utils
{
    public static class FuncoesXml
    {

        // https://github.com/ZeusAutomacao/DFe.NET/issues/610
        private static readonly Hashtable CacheSerializers = new Hashtable();

        /// <summary>
        ///     Serializa a classe passada para uma string no form
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static string ClasseParaXmlString<T>(T objeto)
        {
            XElement xml;
            var keyNomeClasseEmUso = typeof(T).FullName;
            var ser = BuscarNoCache(keyNomeClasseEmUso, typeof(T));

            using (var memory = new MemoryStream())
            {
                using (TextReader tr = new StreamReader(memory, Encoding.UTF8))
                {
                    ser.Serialize(memory, objeto);
                    memory.Position = 0;
                    xml = XElement.Load(tr);
                    xml.Attributes().Where(x => x.Name.LocalName.Equals("xsd") || x.Name.LocalName.Equals("xsi")).Remove();
                }
            }
            return XElement.Parse(xml.ToString()).ToString(SaveOptions.DisableFormatting);
        }

        /// <summary>
        ///     Deserializa a classe a partir de uma String contendo a estrutura XML daquela classe
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T XmlStringParaClasse<T>(string input) where T : class
        {
            var keyNomeClasseEmUso = typeof(T).FullName;
            var ser = BuscarNoCache(keyNomeClasseEmUso, typeof(T));

            using (var sr = new StringReader(input))
                return (T)ser.Deserialize(sr);
        }

        /// <summary>
        ///     Carrega o objeto da classe com dados do arquivo XML (Deserializa a classe). Atenção o XML deve ter a mesma
        ///     estrutura da classe
        /// </summary>
        /// <typeparam name="T">Classe</typeparam>
        /// <param name="arquivo">Arquivo XML</param>
        /// <returns>Retorna a classe</returns>
        public static T ArquivoXmlParaClasse<T>(string arquivo) where T : class
        {
            if (!File.Exists(arquivo))
            {
                throw new FileNotFoundException("Arquivo " + arquivo + " não encontrado!");
            }

            var keyNomeClasseEmUso = typeof(T).FullName;
            var serializador = BuscarNoCache(keyNomeClasseEmUso, typeof(T));
            var stream = new FileStream(arquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            try
            {
                return (T)serializador.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
        }

        /// <summary>
        ///     Copia a estrutura e os dados da classe passada para um arquivo XML (Serializa a classe). Use try catch para tratar
        ///     a possível exceção "DirectoryNotFoundException"
        /// </summary>
        /// <typeparam name="T">Classe</typeparam>
        /// <param name="objeto">Objeto da Classe</param>
        /// <param name="arquivo">Arquivo XML</param>
        public static void ClasseParaArquivoXml<T>(T objeto, string arquivo)
        {
            var dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }

            var xml = ClasseParaXmlString(objeto);
            try
            {
                var stw = new StreamWriter(arquivo);
                stw.WriteLine(xml);
                stw.Close();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível criar o arquivo " + arquivo + "!");
            }
        }

        public static void SalvarStringXmlParaArquivoXml(string xml, string arquivo)
        {
            var dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }

            try
            {
                var stw = new StreamWriter(arquivo);
                stw.WriteLine(xml);
                stw.Close();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível criar o arquivo " + arquivo + "!");
            }
        }

        /// <summary>
        ///     Obtém um node XML no formato string de um arquivo XML. Util por exemplo, para extrair uma NFe de um XML contendo um
        ///     nfeproc, enviNFe, etc.
        /// </summary>
        /// <param name="nomeDoNode"></param>
        /// <param name="stream"></param>
        /// <returns>Retorna a string contendo o node XML cujo strem foi passado no parâmetro nomeDoNode</returns>
        public static string ObterNodeDeStream(string nomeDoNode, StreamReader stream)
        {
            var xmlDoc = XDocument.Load(stream);

            var xmlString = (from d in xmlDoc.Descendants()
                             where d.Name.LocalName == nomeDoNode
                             select d).FirstOrDefault();

            if (xmlString == null)
                throw new Exception(String.Format("Nenhum objeto {0} encontrado no stream!", nomeDoNode));
            return xmlString.ToString();
        }

        /// <summary>
        ///     Obtém um node XML no formato string de um arquivo XML. Util por exemplo, para extrair uma NFe de um XML contendo um
        ///     nfeproc, enviNFe, etc.
        /// </summary>
        /// <param name="nomeDoNode"></param>
        /// <param name="arquivoXml"></param>
        /// <returns>Retorna a string contendo o node XML cujo nome foi passado no parâmetro nomeDoNode</returns>
        public static string ObterNodeDeArquivoXml(string nomeDoNode, string arquivoXml)
        {
            var xmlDoc = XDocument.Load(arquivoXml);
            var xmlString = (from d in xmlDoc.Descendants()
                             where d.Name.LocalName == nomeDoNode
                             select d).FirstOrDefault();

            if (xmlString == null)
                throw new Exception(String.Format("Nenhum objeto {0} encontrado no arquivo {1}!", nomeDoNode, arquivoXml));
            return xmlString.ToString();
        }

        /// <summary>
        ///     Obtém um node XML no formato string de um arquivo XML. Util por exemplo, para extrair uma NFe de um XML contendo um
        ///     nfeproc, enviNFe, etc.
        /// </summary>
        /// <param name="nomeDoNode"></param>
        /// <param name="stringXml"></param>
        /// <returns>Retorna a string contendo o node XML cujo nome foi passado no parâmetro nomeDoNode</returns>
        public static string ObterNodeDeStringXml(string nomeDoNode, string stringXml)
        {
            var s = stringXml;
            var xmlDoc = XDocument.Parse(s);
            var xmlString = (from d in xmlDoc.Descendants()
                             where d.Name.LocalName == nomeDoNode
                             select d).FirstOrDefault();

            if (xmlString == null)
                throw new Exception(String.Format("Nenhum objeto {0} encontrado no xml!", nomeDoNode));
            return xmlString.ToString();
        }

        // https://github.com/ZeusAutomacao/DFe.NET/issues/610
        private static XmlSerializer BuscarNoCache(string chave, Type type)
        {
            //codigo performatico pois na maioria das vezes já vai estar no cache.
            //https://github.com/ZeusAutomacao/DFe.NET/issues/1146
            if (CacheSerializers.Contains(chave))
            {
                return (XmlSerializer)CacheSerializers[chave];
            }

            //lock por conta de ambientes de alta concorrência (lock no type pois se for type diferente pode ser outro lock separado que não vai prejudicar)
            //https://github.com/ZeusAutomacao/DFe.NET/issues/1146
            lock (type)
            {
                if (CacheSerializers.Contains(chave))
                {
                    return (XmlSerializer)CacheSerializers[chave];
                }

                var ser = XmlSerializer.FromTypes(new[] { type })[0];
                CacheSerializers.Add(chave, ser);

                return ser;
            }
        }
    }
}