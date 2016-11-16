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
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;

namespace NFe.Utils.Tributacao.Estadual
{
    public class ICMSGeral
    {
        public ICMSGeral(ICMSBasico icmsBasico)
        {
            this.CopiarPropriedades(icmsBasico);
        }

        /// <summary>
        ///     Origem da Mercadoria
        /// </summary>
        public OrigemMercadoria orig { get; private set; }

        /// <summary>
        ///     Situação Tributária
        /// </summary>
        public Csticms CST { get; private set; }

        /// <summary>
        ///     Modalidade de determinação da BC do ICMS
        /// </summary>
        public DeterminacaoBaseIcms modBC { get; private set; }

        /// <summary>
        ///     Valor da BC do ICMS
        /// </summary>
        public decimal vBC { get; private set; }

        /// <summary>
        ///     Alíquota do imposto
        /// </summary>
        public decimal pICMS { get; private set; }

        /// <summary>
        ///     Valor do ICMS
        /// </summary>
        public decimal vICMS { get; private set; }

        /// <summary>
        ///     Modalidade de determinação da BC do ICMS ST
        /// </summary>
        public DeterminacaoBaseIcmsSt modBCST { get; private set; }

        /// <summary>
        ///     Percentual da margem de valor Adicionado do ICMS ST
        /// </summary>
        public decimal? pMVAST { get; private set; }

        /// <summary>
        ///     Percentual da Redução de BC do ICMS ST
        /// </summary>
        public decimal? pRedBCST { get; private set; }

        /// <summary>
        ///     Valor da BC do ICMS ST
        /// </summary>
        public decimal vBCST { get; private set; }

        /// <summary>
        ///     Alíquota do imposto do ICMS ST
        /// </summary>
        public decimal pICMSST { get; private set; }

        /// <summary>
        ///     Valor do ICMS ST
        /// </summary>
        public decimal vICMSST { get; private set; }

        /// <summary>
        ///     Percentual de redução da BC
        /// </summary>
        public decimal pRedBC { get; private set; }

        /// <summary>
        ///     Valor do ICMS desonerado
        /// </summary>
        public decimal? vICMSDeson { get; private set; }

        /// <summary>
        ///     Motivo da desoneração do ICMS
        /// </summary>
        public MotivoDesoneracaoIcms? motDesICMS { get; private set; }

       /// <summary>
        ///     Valor do ICMS da Operação
        /// </summary>
        public decimal? vICMSOp { get; private set; }

        /// <summary>
        ///     Percentual do diferimento
        /// </summary>
        public decimal? pDif { get; private set; }

        /// <summary>
        ///     Valor do ICMS diferido
        /// </summary>
        public decimal? vICMSDif { get; private set; }

        /// <summary>
        ///     Valor da BC do ICMS ST retido
        /// </summary>
        public decimal? vBCSTRet { get; private set; }

        /// <summary>
        ///     Valor do ICMS ST retido
        /// </summary>
        public decimal? vICMSSTRet { get; private set; }

        /// <summary>
        ///     Percentual da BC operação própria
        /// </summary>
        public decimal pBCOp { get; private set; }

        /// <summary>
        ///     UF para qual é devido o ICMS ST
        /// </summary>
        public string UFST { get; private set; }

        #region Campos exclusivos simples nacional

        /// <summary>
        ///     Código de Situação da Operação – Simples Nacional
        /// </summary>
        public Csosnicms CSOSN { get; private set; }

        /// <summary>
        ///     pCredSN - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        /// </summary>
        public decimal pCredSN { get; private set; }

        /// <summary>
        ///     Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
        /// </summary>
        public decimal vCredICMSSN { get; private set; }

        #endregion
    }
}
