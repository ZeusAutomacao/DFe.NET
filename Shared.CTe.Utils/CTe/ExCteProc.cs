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

using System.IO;
using CTe.Classes;
using DFe.Utils;
using cteProc = CTe.Classes.cteProc;

namespace CTe.Utils.CTe
{
    public static class ExtCteProc
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe cteProc
        /// </summary>
        /// <param name="cteProc"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna um cteProc carregada com os dados do XML</returns>
        public static cteProc CarregarDeArquivoXml(this cteProc cteProc, string arquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<cteProc>(arquivoXml);
        }

        /// <summary>
        ///     Converte o objeto cteProc para uma string no formato XML
        /// </summary>
        /// <param name="cteProc"></param>
        /// <returns>Retorna uma string no formato XML com os dados do cteProc</returns>
        public static string ObterXmlString(this cteProc cteProc)
        {
            return FuncoesXml.ClasseParaXmlString(cteProc);
        }

        /// <summary>
        ///     Coverte uma string XML no formato cteProc para um objeto cteProc
        /// </summary>
        /// <param name="cteProc"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo cteProc</returns>
        public static cteProc CarregarDeXmlString(this cteProc cteProc, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(cteProc).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<cteProc>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto cteProc em um arquivo XML
        /// </summary>
        /// <param name="cteProc">Objeto cteProc</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this cteProc cteProc, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(cteProc, arquivoXml);
        }

        public static void SalvarXmlEmDisco(this cteProc cteProc, ConfiguracaoServico configuracaoServico = null)
        {
            if (cteProc == null) return;

            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cteProc.CTe.Chave() + "-cteproc.xml");

            FuncoesXml.ClasseParaArquivoXml(cteProc, arquivoSalvar);
        }
    }
}