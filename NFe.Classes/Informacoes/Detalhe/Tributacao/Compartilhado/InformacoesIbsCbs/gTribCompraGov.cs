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
        private decimal _pAliqIbsUf;
        private decimal _vTribIbsUf;
        private decimal _pAliqIbsMun;
        private decimal _vTribIbsMun;
        private decimal _pAliqCbs;
        private decimal _vTribCbs;
        
        /// <summary>
        ///     UB82b - Alíquota do IBS de competência do Estado
        /// </summary>
        public decimal pAliqIBSUF
        {
            get => _pAliqIbsUf.Arredondar(4);
            set => _pAliqIbsUf = value.Arredondar(4);
        }

        /// <summary>
        ///     UB82c - Valor do Tributo do IBS da UF calculado
        /// </summary>
        public decimal vTribIBSUF
        {
            get => _vTribIbsUf.Arredondar(2);
            set => _vTribIbsUf = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB82d - Alíquota do IBS de competência do Município
        /// </summary>
        public decimal pAliqIBSMun
        {
            get => _pAliqIbsMun.Arredondar(4);
            set => _pAliqIbsMun = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB82e - Valor do Tributo do IBS do Município calculado
        /// </summary>
        public decimal vTribIBSMun
        {
            get => _vTribIbsMun.Arredondar(2);
            set => _vTribIbsMun = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB82f - Alíquota da CBS
        /// </summary>
        public decimal pAliqCBS
        {
            get => _pAliqCbs.Arredondar(4);
            set => _pAliqCbs = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB82g - Valor do Tributo da CBS calculado
        /// </summary>
        public decimal vTribCBS
        {
            get => _vTribCbs.Arredondar(2);
            set => _vTribCbs = value.Arredondar(2);
        }
    }
}