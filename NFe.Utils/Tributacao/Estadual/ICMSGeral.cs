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

using System;
using DFe.Utils;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Emitente;

namespace NFe.Utils.Tributacao.Estadual
{
    /// <summary>
    /// Classe com todos os campos da tributação de ICMS e CSOSN
    /// </summary>
    public class ICMSGeral
    {
        /// <summary>
        /// Cria um objeto ICMSGeral com os dados do ICMSBasico passado
        /// </summary>
        /// <param name="icmsBasico">Um objeto que implemente a classe abstrata ICMSBasico. Ex: ICMS00, ICMS10, ICMSSN101, etc.</param>
        public ICMSGeral(ICMSBasico icmsBasico)
        {
            this.CopiarPropriedades(icmsBasico);
        }

        public ICMSGeral()
        {
            
        }

        /// <summary>
        /// Obtém um objeto ICMSBasico com base nos dados do objeto ICMSGeral e da CRT informada no parâmetro.
        /// Esse método pode devolver, por exemplo, um objeto ICMS00, ICMS10, ICMSSN101, etc.
        /// </summary>
        /// <param name="crt"></param>
        /// <returns></returns>
        public ICMSBasico ObterICMSBasico(CRT crt)
        {
            ICMSBasico icmsBasico;

            switch (crt)
            {
                case CRT.SimplesNacional:
                    switch (CSOSN)
                    {
                        case Csosnicms.Csosn101:
                            icmsBasico = new ICMSSN101();
                            break;
                        case Csosnicms.Csosn102:
                        case Csosnicms.Csosn103:
                        case Csosnicms.Csosn300:
                        case Csosnicms.Csosn400:
                            icmsBasico = new ICMSSN102();
                            break;
                        case Csosnicms.Csosn201:
                            icmsBasico = new ICMSSN201();
                            break;
                        case Csosnicms.Csosn202:
                        case Csosnicms.Csosn203:
                            icmsBasico = new ICMSSN202();
                            break;
                        case Csosnicms.Csosn500:
                            icmsBasico = new ICMSSN500();
                            break;
                        case Csosnicms.Csosn900:
                            icmsBasico = new ICMSSN900();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case CRT.SimplesNacionalExcessoSublimite:
                case CRT.RegimeNormal:
                    switch (CST)
                    {
                        case Csticms.Cst00:
                            icmsBasico = new ICMS00();
                            break;
                        case Csticms.Cst10:
                            icmsBasico = new ICMS10();
                            break;
                        case Csticms.CstPart10:
                        case Csticms.CstPart90:
                            icmsBasico = new ICMSPart();
                            break;
                        case Csticms.Cst20:
                            icmsBasico = new ICMS20();
                            break;
                        case Csticms.Cst30:
                            icmsBasico = new ICMS30();
                            break;
                        case Csticms.Cst40:
                        case Csticms.Cst41:
                        case Csticms.Cst50:
                            icmsBasico = new ICMS40();
                            break;
                        case Csticms.CstRep41:
                        case Csticms.CstRep60:
                            icmsBasico = new ICMSST();
                            break;
                        case Csticms.Cst51:
                            icmsBasico = new ICMS51();
                            break;
                        case Csticms.Cst60:
                            icmsBasico = new ICMS60();
                            break;
                        case Csticms.Cst70:
                            icmsBasico = new ICMS70();
                            break;
                        case Csticms.Cst90:
                            icmsBasico = new ICMS90();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("crt", crt, null);
            }

            icmsBasico.CopiarPropriedades(this);
            return icmsBasico;
        }

        /// <summary>
        ///     Origem da Mercadoria
        /// </summary>
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     Situação Tributária
        /// </summary>
        public Csticms CST { get; set; }

        /// <summary>
        ///     Modalidade de determinação da BC do ICMS
        /// </summary>
        public DeterminacaoBaseIcms modBC { get; set; }

        /// <summary>
        ///     Valor da BC do ICMS
        /// </summary>
        public decimal vBC { get; set; }

        /// <summary>
        ///     Alíquota do imposto
        /// </summary>
        public decimal pICMS { get; set; }

        /// <summary>
        ///     Valor do ICMS
        /// </summary>
        public decimal vICMS { get; set; }

        /// <summary>
        ///     Modalidade de determinação da BC do ICMS ST
        /// </summary>
        public DeterminacaoBaseIcmsSt modBCST { get; set; }

        /// <summary>
        ///     Percentual da margem de valor Adicionado do ICMS ST
        /// </summary>
        public decimal? pMVAST { get; set; }

        /// <summary>
        ///     Percentual da Redução de BC do ICMS ST
        /// </summary>
        public decimal? pRedBCST { get; set; }

        /// <summary>
        ///     Valor da BC do ICMS ST
        /// </summary>
        public decimal vBCST { get; set; }

        /// <summary>
        ///     Alíquota do imposto do ICMS ST
        /// </summary>
        public decimal pICMSST { get; set; }

        /// <summary>
        ///     Valor do ICMS ST
        /// </summary>
        public decimal vICMSST { get; set; }

        /// <summary>
        ///     Percentual de redução da BC
        /// </summary>
        public decimal pRedBC { get; set; }

        /// <summary>
        ///     Valor do ICMS desonerado
        /// </summary>
        public decimal? vICMSDeson { get; set; }

        /// <summary>
        ///     Motivo da desoneração do ICMS
        /// </summary>
        public MotivoDesoneracaoIcms? motDesICMS { get; set; }

       /// <summary>
        ///     Valor do ICMS da Operação
        /// </summary>
        public decimal? vICMSOp { get; set; }

        /// <summary>
        ///     Percentual do diferimento
        /// </summary>
        public decimal? pDif { get; set; }

        /// <summary>
        ///     Valor do ICMS diferido
        /// </summary>
        public decimal? vICMSDif { get; set; }

        /// <summary>
        ///     Valor da BC do ICMS ST retido
        /// </summary>
        public decimal? vBCSTRet { get; set; }
        
        /// <summary>
        ///     Valor do ICMS ST retido
        /// </summary>
        public decimal? vICMSSTRet { get; set; }

        /// <summary>
        ///     Percentual da BC operação própria
        /// </summary>
        public decimal pBCOp { get; set; }

        /// <summary>
        ///     UF para qual é devido o ICMS ST
        /// </summary>
        public string UFST { get; set; }

        /// <summary>
        ///     Valor da BC do ICMS ST da UF destino
        /// </summary>
        public decimal vBCSTDest { get; set; }


        /// <summary>
        ///     Valor do ICMS ST da UF destino
        /// </summary>
        public decimal vICMSSTDest { get; set; }

        #region Campos exclusivos simples nacional

        /// <summary>
        ///     Código de Situação da Operação – Simples Nacional
        /// </summary>
        public Csosnicms CSOSN { get; set; }

        /// <summary>
        ///     pCredSN - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        /// </summary>
        public decimal pCredSN { get; set; }

        /// <summary>
        ///     Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
        /// </summary>
        public decimal vCredICMSSN { get; set; }

        #endregion

        #region Fundo de combate a pobreza

        /// <summary>
        ///     Valor da Base de Cálculo do Fundo de Combate à Pobreza (FCP)
        /// </summary>
        public decimal? vBCFCP { get; set; }

        /// <summary>
        ///     Percentual do Fundo de Combate à Pobreza (FCP)
        /// </summary>
        public decimal? pFCP { get; set; }

        /// <summary>
        ///     Valor do Fundo de Combate à Pobreza (FCP)
        /// </summary>
        public decimal? vFCP { get; set; }

        /// <summary>
        ///     Valor da Base de Cálculo do FCP retido por Substituição Tributária
        /// </summary>
        public decimal? vBCFCPST { get; set; }

        /// <summary>
        ///     Percentual do Fundo de Combate à Pobreza (FCP) retido por Substituição Tributária
        /// </summary>
        public decimal? pFCPST { get; set; }

        /// <summary>
        ///     Valor do Fundo de Combate à Pobreza (FCP) retido por Substituição Tributária
        /// </summary>
        public decimal? vFCPST { get; set; }

        #endregion

        /// <summary>
        ///     Alíquota suportada pelo Consumidor Final
        /// </summary>
        public decimal? pST { get; set; }

        /// <summary>
        ///     Valor da Base de Cálculo do FCP retido anteriormente por ST 
        /// </summary>
        public decimal? vBCFCPSTRet { get; set; }

        /// <summary>
        ///     Percentual do FCP retido anteriormente por Substituição Tributária
        /// </summary>
        public decimal? pFCPSTRet { get; set; }

        /// <summary>
        ///     Valor do FCP retido por Substituição Tributária
        /// </summary>
        public decimal? vFCPSTRet { get; set; }
		
		/// <summary>
        ///     Valor do ICMS próprio do Substituto (tag: vICMSSubstituto)
        /// </summary>
        public decimal? vICMSSubstituto { get; set; }

        #region Icms Efetivo
        /// <summary>
        ///     Percentual de redução da base de cálculo efetiva 
        /// </summary>
        public decimal? pRedBCEfet { get; set; }

        /// <summary>
        ///     Valor da base de cálculo efetiva 
        /// </summary>
        public decimal? vBCEfet { get; set; }

        /// <summary>
        ///     Alíquota do ICMS efetiva 
        /// </summary>
        public decimal? pICMSEfet { get; set; }

        /// <summary>
        ///     Valor do ICMS efetivo 
        /// </summary>
        public decimal? vICMSEfet { get; set; }
        #endregion

    }
}