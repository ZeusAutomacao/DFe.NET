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
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Municipal;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public class imposto
    {
        private decimal? _vTotTrib;

        /// <summary>
        ///     M02 - Valor estimado total de impostos federais, estaduais e municipais
        /// </summary>
        public decimal? vTotTrib
        {
            get { return _vTotTrib.Arredondar(2); }
            set { _vTotTrib = value.Arredondar(2); }
        }

        /// <summary>
        ///     N01 - Dados do ICMS Normal e ST
        /// </summary>
        public ICMS ICMS { get; set; }

        /// <summary>
        ///     U01 - Grupo ISSQN
        /// </summary>
        public ISSQN ISSQN { get; set; }

        /// <summary>
        ///     O01 - Grupo IPI
        /// </summary>
        public IPI IPI { get; set; }

        /// <summary>
        ///     P01 - Grupo Imposto de Importação
        /// </summary>
        public II II { get; set; }

        /// <summary>
        ///     Q01 - Grupo PIS
        /// </summary>
        public PIS PIS { get; set; }

        /// <summary>
        ///     R01 - Grupo PIS Substituição Tributária
        /// </summary>
        public PISST PISST { get; set; }

        /// <summary>
        ///     S01 - Grupo COFINS
        /// </summary>
        public COFINS COFINS { get; set; }

        /// <summary>
        ///     T01 - Grupo COFINS Substituição Tributária
        /// </summary>
        public COFINSST COFINSST { get; set; }

        /// <summary>
        /// NA01 - Informação do ICMS Interestadua
        /// </summary>
        public ICMSUFDest ICMSUFDest { get; set; }

        public bool ShouldSerializevTotTrib()
        {
            return vTotTrib.HasValue;
        }
    }
}