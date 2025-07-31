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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs
{
    public class gMonoDif
    {
        private decimal _pDifIbs;
        private decimal _vIbsMonoDif;
        private decimal _pDifCbs;
        private decimal _vCbsMonoDif;
        
        /// <summary>
        ///     UB100 - Percentual do diferimento do imposto monofásico
        /// </summary>
        public decimal pDifIBS
        {
            get => _pDifIbs.Arredondar(4);
            set => _pDifIbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB101 - Valor do IBS monofásico diferido
        /// </summary>
        public decimal vIBSMonoDif
        {
            get => _vIbsMonoDif.Arredondar(2);
            set => _vIbsMonoDif = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB102 - Percentual do diferimento do imposto monofásico
        /// </summary>
        public decimal pDifCBS
        {
            get => _pDifCbs.Arredondar(4);
            set => _pDifCbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB103 - Valor da CBS Monofásica diferida
        /// </summary>
        public decimal vCBSMonoDif
        {
            get => _vCbsMonoDif.Arredondar(2);
            set => _vCbsMonoDif = value.Arredondar(2);
        }
    }
}