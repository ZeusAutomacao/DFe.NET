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

using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs
{
    public class gTribRegular
    {
        private decimal _pAliqEfetRegIbsUf;
        private decimal _vTribRegIbsUf;
        private decimal _pAliqEfetRegIbsMun;
        private decimal _vTribRegIbsMun;
        private decimal _pAliqEfetRegCbs;
        private decimal _vTribRegCbs;
        
        /// <summary>
        ///     UB69 - Código de Situação Tributária do IBS e CBS
        /// </summary>
        public CST CSTReg { get; set; }
        
        /// <summary>
        ///     UB70 - Código de Classificação Tributária do IBS e CBS
        /// </summary>
        public string cClassTribReg { get; set; }

        /// <summary>
        ///     UB71 - Valor da alíquota do IBS da UF (em percentual)
        /// </summary>
        public decimal pAliqEfetRegIBSUF
        {
            get => _pAliqEfetRegIbsUf.Arredondar(4);
            set => _pAliqEfetRegIbsUf = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB72 - Valor do Tributo do IBS da UF
        /// </summary>
        public decimal vTribRegIBSUF
        {
            get => _vTribRegIbsUf.Arredondar(2);
            set => _vTribRegIbsUf = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB72a - Valor da alíquota do IBS do Município (em percentual)
        /// </summary>
        public decimal pAliqEfetRegIBSMun
        {
            get => _pAliqEfetRegIbsMun.Arredondar(4);
            set => _pAliqEfetRegIbsMun = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB72b - Valor do Tributo do IBS do Município
        /// </summary>
        public decimal vTribRegIBSMun
        {
            get => _vTribRegIbsMun.Arredondar(2);
            set => _vTribRegIbsMun = value.Arredondar(2);
        }

        /// <summary>
        ///     UB72c - Valor da alíquota da CBS (em percentual)
        /// </summary>
        public decimal pAliqEfetRegCBS
        {
            get => _pAliqEfetRegCbs.Arredondar(4);
            set => _pAliqEfetRegCbs = value.Arredondar(4);
        }

        /// <summary>
        ///     UB72d - Valor do Tributo da CBS
        /// </summary>
        public decimal vTribRegCBS
        {
            get => _vTribRegCbs.Arredondar(2);
            set => _vTribRegCbs = value.Arredondar(2);
        }
    }
}