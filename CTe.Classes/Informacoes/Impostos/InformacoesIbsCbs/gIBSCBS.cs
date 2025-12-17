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

using CTe.Classes.Informacoes.Impostos.InformacoesIbsCbs.InformacoesCbs;
using CTe.Classes.Informacoes.Impostos.InformacoesIbsCbs.InformacoesIbs;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.InformacoesIbsCbs
{
    /// <summary>
    /// Grupo de Informações do IBS e da CBS
    /// </summary>
    public class gIBSCBS
    {
        private decimal _vBc;
        private decimal _vIbs;

        /// <summary>
        /// Base de cálculo do IBS e CBS
        /// </summary>
        public decimal vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        /// Grupo de Informações do IBS para a UF
        /// </summary>
        public gIBSUF gIBSUF { get; set; }

        /// <summary>
        /// Grupo de Informações do IBS para o município
        /// </summary>
        public gIBSMun gIBSMun { get; set; }

        /// <summary>
        /// Valor do IBS
        /// </summary>
        public decimal vIBS
        {
            get { return _vIbs.Arredondar(2); }
            set { _vIbs = value.Arredondar(2); }
        }

        /// <summary>
        /// Grupo de Informações da CBS
        /// </summary>
        public gCBS gCBS { get; set; }

        /// <summary>
        /// Grupo de Informações da Tributação Regular
        /// </summary>
        public gTribRegular gTribRegular { get; set; }

        /// <summary>
        /// Grupo de Crédito Presumido do IBS
        /// </summary>
        public gIBSCredPres gIBSCredPres { get; set; }

        /// <summary>
        /// Grupo de Crédito Presumido da CBS
        /// </summary>
        public gCBSCredPres gCBSCredPres { get; set; }

        /// <summary>
        /// Grupo de Informações da Composição do Valor do IBS e da CBS em Compras Governamentais
        /// </summary>
        public gTribCompraGov gTribCompraGov { get; set; }
    }
}
