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
namespace NFe.Classes.Informacoes.Total
{
    public class ICMSTot
    {
        private decimal _vBc;
        private decimal _vIcms;
        private decimal? _vIcmsDeson;
        private decimal _vBcst;
        private decimal _vSt;
        private decimal _vProd;
        private decimal _vFrete;
        private decimal _vSeg;
        private decimal _vDesc;
        private decimal _vIi;
        private decimal _vIpi;
        private decimal _vPis;
        private decimal _vCofins;
        private decimal _vOutro;
        private decimal _vNf;
        private decimal _vTotTrib;

        /// <summary>
        ///     W03 - Base de Cálculo do ICMS
        /// </summary>
        public decimal vBC
        {
            get { return _vBc; }
            set { _vBc = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W04 - Valor Total do ICMS
        /// </summary>
        public decimal vICMS
        {
            get { return _vIcms; }
            set { _vIcms = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W04a - Valor Total do ICMS desonerado
        /// </summary>
        public decimal? vICMSDeson
        {
            get { return _vIcmsDeson; }
            set { _vIcmsDeson = Valor.Arredondar(value, 2); }
        } //Nulable por conta da v2.00

        /// <summary>
        ///     W05 - Base de Cálculo do ICMS ST
        /// </summary>
        public decimal vBCST
        {
            get { return _vBcst; }
            set { _vBcst = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W06 - Valor Total do ICMS ST
        /// </summary>
        public decimal vST
        {
            get { return _vSt; }
            set { _vSt = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W07 - Valor Total dos produtos e serviços
        /// </summary>
        public decimal vProd
        {
            get { return _vProd; }
            set { _vProd = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W08 - Valor Total do Frete
        /// </summary>
        public decimal vFrete
        {
            get { return _vFrete; }
            set { _vFrete = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W09 - Valor Total do Seguro
        /// </summary>
        public decimal vSeg
        {
            get { return _vSeg; }
            set { _vSeg = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W10 - Valor Total do Desconto
        /// </summary>
        public decimal vDesc
        {
            get { return _vDesc; }
            set { _vDesc = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W11 - Valor Total do II
        /// </summary>
        public decimal vII
        {
            get { return _vIi; }
            set { _vIi = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W12 - Valor Total do IPI
        /// </summary>
        public decimal vIPI
        {
            get { return _vIpi; }
            set { _vIpi = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W13 - Valor do PIS
        /// </summary>
        public decimal vPIS
        {
            get { return _vPis; }
            set { _vPis = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W14 - Valor da COFINS
        /// </summary>
        public decimal vCOFINS
        {
            get { return _vCofins; }
            set { _vCofins = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W15 - Outras Despesas acessórias
        /// </summary>
        public decimal vOutro
        {
            get { return _vOutro; }
            set { _vOutro = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     w16 - Valor Total da NF-e
        /// </summary>
        public decimal vNF
        {
            get { return _vNf; }
            set { _vNf = Valor.Arredondar(value, 2); }
        }

        /// <summary>
        ///     W16a - Valor aproximado total de tributos federais, estaduais e municipais.
        /// </summary>
        public decimal vTotTrib
        {
            get { return _vTotTrib; }
            set { _vTotTrib = Valor.Arredondar(value, 2); }
        }

        public bool ShouldSerializevICMSDeson()
        {
            return vICMSDeson.HasValue;
        }
    }
}