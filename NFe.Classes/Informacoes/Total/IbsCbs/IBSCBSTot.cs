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

using System.Xml.Serialization;
using NFe.Classes.Informacoes.Total.IbsCbs.Cbs;
using NFe.Classes.Informacoes.Total.IbsCbs.Ibs;
using NFe.Classes.Informacoes.Total.IbsCbs.Monofasica;

namespace NFe.Classes.Informacoes.Total.IbsCbs
{
    public class IBSCBSTot
    {
        private decimal _vBCIBSCBS;

        /// <summary>
        ///     W35 - Valor total da BC do IBS e da CBS
        /// </summary>
        public decimal vBCIBSCBS
        {
            get => _vBCIBSCBS;
            set => _vBCIBSCBS = value.Arredondar(2);
        }
        
        /// <summary>
        ///     W36 - Grupo total do IBS
        /// </summary>
        public gIBS gIBS  { get; set; }
        
        /// <summary>
        ///     W50 - Grupo total do CBS
        /// </summary>
        [XmlElement("gCBS")]
        public gCBSTotal gCBS { get; set; }
        
        /// <summary>
        ///     W57 - Grupo total da Monofasia
        /// </summary>
        public gMono gMono { get; set; }
    }
}