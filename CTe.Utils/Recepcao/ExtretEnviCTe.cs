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
using CTe.Classes.Servicos.Recepcao;
using DFe.Utils;

namespace CTe.Utils.Recepcao
{
    public static class ExtretEnviCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retEnviCte
        /// </summary>
        /// <param name="retEnviCte"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retEnviCte</returns>
        public static retEnviCte CarregarDeXmlString(this retEnviCte retEnviCte, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retEnviCte>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retEnviCte para uma string no formato XML
        /// </summary>
        /// <param name="retEnviCte"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnviCte</returns>
        public static string ObterXmlString(this retEnviCte retEnviCte)
        {
            return FuncoesXml.ClasseParaXmlString(retEnviCte);
        }

        public static void SalvarXmlEmDisco(this retEnviCte retEnviCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, retEnviCte.infRec.nRec + "-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }
    }
}