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
    public class ISSQNtot
    {
        private decimal? _vServ;
        private decimal? _vBc;
        private decimal? _vIss;
        private decimal? _vPis;
        private decimal? _vCofins;
        private decimal? _vDeducao;
        private decimal? _vOutro;
        private decimal? _vDescIncond;
        private decimal? _vDescCond;
        private decimal? _vIssRet;

        /// <summary>
        ///     W18 - Valor total dos Serviços sob não-incidência ou não tributados pelo ICMS
        /// </summary>
        public decimal? vServ
        {
            get { return _vServ.Arredondar(2); }
            set { _vServ = value.Arredondar(2); }
        }

        /// <summary>
        ///     W19 - Valor total Base de Cálculo do ISS
        /// </summary>
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     W20 - Valor total do ISS
        /// </summary>
        public decimal? vISS
        {
            get { return _vIss.Arredondar(2); }
            set { _vIss = value.Arredondar(2); }
        }

        /// <summary>
        ///     W21 - Valor total do PIS sobre serviços
        /// </summary>
        public decimal? vPIS
        {
            get { return _vPis.Arredondar(2); }
            set { _vPis = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22 - Valor total da COFINS sobre serviços
        /// </summary>
        public decimal? vCOFINS
        {
            get { return _vCofins.Arredondar(2); }
            set { _vCofins = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22a - Data da prestação do serviço
        /// </summary>
        public string dCompet { get; set; }

        /// <summary>
        ///     W22b - Valor total dedução para redução da Base de Cálculo
        /// </summary>
        public decimal? vDeducao
        {
            get { return _vDeducao.Arredondar(2); }
            set { _vDeducao = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22c - Valor total outras retenções
        /// </summary>
        public decimal? vOutro
        {
            get { return _vOutro.Arredondar(2); }
            set { _vOutro = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22d - Valor total desconto incondicionado
        /// </summary>
        public decimal? vDescIncond
        {
            get { return _vDescIncond.Arredondar(2); }
            set { _vDescIncond = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22e - Valor total desconto condicionado
        /// </summary>
        public decimal? vDescCond
        {
            get { return _vDescCond.Arredondar(2); }
            set { _vDescCond = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22f - Valor total retenção ISS
        /// </summary>
        public decimal? vISSRet
        {
            get { return _vIssRet.Arredondar(2); }
            set { _vIssRet = value.Arredondar(2); }
        }

        /// <summary>
        ///     W22g - Código do Regime Especial de Tributação
        /// </summary>
        public RegTribISSQN? cRegTrib { get; set; }

        public bool ShouldSerializevServ()
        {
            return vServ.HasValue;
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializevISS()
        {
            return vISS.HasValue;
        }

        public bool ShouldSerializevPIS()
        {
            return vPIS.HasValue;
        }

        public bool ShouldSerializevCOFINS()
        {
            return vCOFINS.HasValue;
        }

        public bool ShouldSerializevDeducao()
        {
            return vDeducao.HasValue;
        }

        public bool ShouldSerializevOutro()
        {
            return vOutro.HasValue;
        }

        public bool ShouldSerializevDescIncond()
        {
            return vDescIncond.HasValue;
        }

        public bool ShouldSerializevDescCond()
        {
            return vDescCond.HasValue;
        }

        public bool ShouldSerializevISSRet()
        {
            return vISSRet.HasValue;
        }

        public bool ShouldSerializecRegTrib()
        {
            return cRegTrib.HasValue;
        }
    }
}