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

using DFe.Utils;
using CTe.Classes.Servicos.DistribuicaoDFe;

namespace CTe.Utils.DistribuicaoDFe
{
    public static class ExtretDistDFeInt
    {
        /// <summary>
        /// Carrega um objeto do tipo retDistDFeInt a partir de uma string no formato XML
        /// </summary>
        /// <param name="retDistDFeInt">Objeto do tipo retDistDFeInt</param>
        /// <param name="xmlString">String com uma estrutura XML</param>
        /// <returns>Retorna um objeto retDistDFeInt com as informações da string XML</returns>
        public static retDistDFeInt CarregarDeXmlString(this retDistDFeInt retDistDFeInt, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retDistDFeInt>(xmlString);
        }

        /// <summary>
        /// Converte um objeto do tipo retDistDFeInt para uma string no formato XML com os dados do objeto
        /// </summary>
        /// <param name="retDistDFeInt">Objeto do tipo retDistDFeInt</param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retDistDFeInt</returns>
        public static string ObterXmlString(this retDistDFeInt retDistDFeInt)
        {
            return FuncoesXml.ClasseParaXmlString(retDistDFeInt);
        }
    }
}
