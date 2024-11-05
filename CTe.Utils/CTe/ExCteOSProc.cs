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
using cteOSProc = CTe.Classes.cteOSProc;

namespace CTe.Utils.CTe
{
    public static class ExtCteOSProc
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe cteOSProc
        /// </summary>
        /// <param name="cteOSProc"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna um cteOSProc carregada com os dados do XML</returns>
        public static cteOSProc CarregarDeArquivoXml(this cteOSProc cteOSProc, string arquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<cteOSProc>(arquivoXml);
        }

        /// <summary>
        ///     Converte o objeto cteOSProc para uma string no formato XML
        /// </summary>
        /// <param name="cteOSProc"></param>
        /// <returns>Retorna uma string no formato XML com os dados do cteOSProc</returns>
        public static string ObterXmlString(this cteOSProc cteOSProc)
        {
            return FuncoesXml.ClasseParaXmlString(cteOSProc);
        }

        /// <summary>
        ///     Coverte uma string XML no formato cteOSProc para um objeto cteOSProc
        /// </summary>
        /// <param name="cteOSProc"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo cteOSProc</returns>
        public static cteOSProc CarregarDeXmlString(this cteOSProc cteOSProc, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(cteOSProc).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<cteOSProc>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto cteOSProc em um arquivo XML
        /// </summary>
        /// <param name="cteOSProc">Objeto cteOSProc</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this cteOSProc cteOSProc, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(cteOSProc, arquivoXml);
        }

        public static void SalvarXmlEmDisco(this cteOSProc cteOSProc, ConfiguracaoServico configuracaoServico = null)
        {
            if (cteOSProc == null) return;

            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cteOSProc.CTeOS.Chave() + "-cteOSProc.xml");

            FuncoesXml.ClasseParaArquivoXml(cteOSProc, arquivoSalvar);
        }
    }
}