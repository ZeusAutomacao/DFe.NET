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
namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Municipal
{
    public class ISSQN
    {
        private decimal _vBc;
        private decimal _vAliq;
        private decimal _vIssqn;
        private decimal? _vDeducao;
        private decimal? _vOutro;
        private decimal? _vDescIncond;
        private decimal? _vDescCond;
        private decimal? _vIssRet;

        /// <summary>
        ///     U02 - Valor da Base de Cálculo do ISSQN
        /// </summary>
        public decimal vBC
        {
            get { return _vBc; }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     U03 - Alíquota do ISSQN
        /// </summary>
        public decimal vAliq
        {
            get { return _vAliq; }
            set { _vAliq = value.Arredondar(2); }
        }

        /// <summary>
        ///     U04 - Valor do ISSQN
        /// </summary>
        public decimal vISSQN
        {
            get { return _vIssqn; }
            set { _vIssqn = value.Arredondar(2); }
        }

        /// <summary>
        ///     U05 - Código do município de ocorrência do fato gerador do ISSQN
        /// </summary>
        public long cMunFG { get; set; }

        /// <summary>
        ///     U06 - Item da Lista de Serviços
        /// </summary>
        public string cListServ { get; set; }

        /// <summary>
        ///     U07 - Valor dedução para redução da Base de Cálculo
        /// </summary>
        public decimal? vDeducao
        {
            get { return _vDeducao.Arredondar(2); }
            set { _vDeducao = value.Arredondar(2); }
        }

        /// <summary>
        ///     U08 - Valor outras retenções
        /// </summary>
        public decimal? vOutro
        {
            get { return _vOutro.Arredondar(2); }
            set { _vOutro = value.Arredondar(2); }
        }

        /// <summary>
        ///     U09 - Valor desconto incondicionado
        /// </summary>
        public decimal? vDescIncond
        {
            get { return _vDescIncond.Arredondar(2); }
            set { _vDescIncond = value.Arredondar(2); }
        }

        /// <summary>
        ///     U10 - Valor desconto condicionado
        /// </summary>
        public decimal? vDescCond
        {
            get { return _vDescCond.Arredondar(2); }
            set { _vDescCond = value.Arredondar(2); }
        }

        /// <summary>
        ///     U11 - Valor retenção ISS
        /// </summary>
        public decimal? vISSRet
        {
            get { return _vIssRet.Arredondar(2); }
            set { _vIssRet = value.Arredondar(2); }
        }

        /// <summary>
        ///     U12 - Indicador da exigibilidade do ISS
        /// </summary>
        public IndicadorISS? indISS{ get; set; }

        /// <summary>
        ///     U13 - Código do serviço prestado dentro do município
        /// </summary>
        public string cServico { get; set; }

        /// <summary>
        ///     U14 - Código do Município de incidência do imposto
        /// </summary>
        public long? cMun { get; set; }

        /// <summary>
        ///     U15 - Código do País onde o serviço foi prestado
        /// </summary>
        public int? cPais { get; set; }

        /// <summary>
        ///     U16 - Número do processo judicial ou administrativo de suspensão da exigibilidade
        /// </summary>
        public string nProcesso { get; set; }

        /// <summary>
        ///     U17 - Indicador de incentivo Fiscal
        /// </summary>
        public indIncentivo? indIncentivo { get; set; }

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

        public bool ShouldSerializecMun()
        {
            return cMun.HasValue;
        }

        public bool ShouldSerializecPais()
        {
            return cPais.HasValue;
        }
    }
}