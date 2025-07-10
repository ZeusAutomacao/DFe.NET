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
    public class gTribCompraGov
    {
        private decimal _pIbsUf;
        private decimal _vIbsUf;
        private decimal _pIbsMun;
        private decimal _vIbsMun;
        private decimal _pCbs;
        private decimal _vCbs;
        
        /// <summary>
        ///     UB82b - Alíquota do IBS de competência do Estado
        /// </summary>
        public decimal pIBSUF
        {
            get => _pIbsUf.Arredondar(4);
            set => _pIbsUf = value.Arredondar(4);
        }

        /// <summary>
        ///     UB82c - Valor do Tributo do IBS da UF calculado
        /// </summary>
        public decimal vIBSUF
        {
            get => _vIbsUf.Arredondar(2);
            set => _vIbsUf = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB82d - Alíquota do IBS de competência do Município
        /// </summary>
        public decimal pIBSMun
        {
            get => _pIbsMun.Arredondar(4);
            set => _pIbsMun = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB82e - Valor do Tributo do IBS do Município calculado
        /// </summary>
        public decimal vIBSMun
        {
            get => _vIbsMun.Arredondar(2);
            set => _vIbsMun = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB82f - Alíquota da CBS
        /// </summary>
        public decimal pCBS
        {
            get => _pCbs.Arredondar(4);
            set => _pCbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB82g - Valor do Tributo da CBS calculado
        /// </summary>
        public decimal vCBS
        {
            get => _vCbs.Arredondar(2);
            set => _vCbs = value.Arredondar(2);
        }
    }
}