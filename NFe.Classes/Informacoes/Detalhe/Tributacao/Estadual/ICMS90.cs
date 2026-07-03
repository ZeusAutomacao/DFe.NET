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
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS90 : ICMSBasico
    {
        private decimal? _vBc;
        private decimal? _pRedBc;
        private decimal? _pIcms;
        private decimal? _vIcmsOp;
        private decimal? _pDif;
        private decimal? _vIcmsDif;
        private decimal? _vIcms;
        private decimal? _pMvast;
        private decimal? _pRedBcst;
        private decimal? _vBcst;
        private decimal? _pIcmsst;
        private decimal? _vIcmsst;
        private decimal? _vIcmsDeson;
        private decimal? _vBcfcp;
        private decimal? _pFcp;
        private decimal? _vFcp;
        private decimal? _pFCPDif;
        private decimal? _vFCPDif;
        private decimal? _vFCPEfet;
        private decimal? _vBcfcpst;
        private decimal? _pFcpst;
        private decimal? _vFcpst;
        private decimal? _vICMSSTDeson;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [XmlElement(Order = 1)]
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12- Situação Tributária
        /// </summary>
        [XmlElement(Order = 2)]
        public Csticms CST { get; set; }

        /// <summary>
        ///     N13 - Modalidade de determinação da BC do ICMS
        /// </summary>
        [XmlElement(Order = 3)]
        public DeterminacaoBaseIcms? modBC { get; set; }

        /// <summary>
        ///     N15 - Valor da BC do ICMS
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     N14 - Percentual de redução da BC
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? pRedBC
        {
            get { return _pRedBc.Arredondar(4); }
            set { _pRedBc = value.Arredondar(4); }
        }

        /// <summary>
        ///     N14a - Código de Benefício Fiscal na UF aplicado ao item quando houver RBC.
        /// </summary>
        [XmlElement(Order = 6)]
        public string cBenefRBC { get; set; }

        /// <summary>
        ///     N16 - Alíquota do imposto
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal? pICMS
        {
            get { return _pIcms.Arredondar(4); }
            set { _pIcms = value.Arredondar(4); }
        }

        /// <summary>
        ///     N16b - Valor do ICMS da Operação
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal? vICMSOp
        {
            get { return _vIcmsOp.Arredondar(2); }
            set { _vIcmsOp = value.Arredondar(2); }
        }

        /// <summary>
        ///     N16c - Percentual do diferimento
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal? pDif
        {
            get { return _pDif.Arredondar(4); }
            set { _pDif = value.Arredondar(4); }
        }

        /// <summary>
        ///     N16d - Valor do ICMS diferido
        /// </summary>
        [XmlElement(Order = 10)]
        public decimal? vICMSDif
        {
            get { return _vIcmsDif.Arredondar(2); }
            set { _vIcmsDif = value.Arredondar(2); }
        }

        /// <summary>
        ///     N17 - Valor do ICMS
        /// </summary>
        [XmlElement(Order = 11)]
        public decimal? vICMS
        {
            get { return _vIcms.Arredondar(2); }
            set { _vIcms = value.Arredondar(2); }
        }

        /// <summary>
        /// N17a - Valor da Base de Cálculo do FCP
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 12)]
        public decimal? vBCFCP
        {
            get { return _vBcfcp.Arredondar(2); }
            set { _vBcfcp = value.Arredondar(2); }
        }

        public bool vBCFCPSpecified
        {
            get { return vBCFCP.HasValue; }
        }

        /// <summary>
        /// N17b - Percentual do Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 13)]
        public decimal? pFCP
        {
            get { return _pFcp.Arredondar(4); }
            set { _pFcp = value.Arredondar(4); }
        }

        public bool pFCPSpecified
        {
            get { return pFCP.HasValue; }
        }

        /// <summary>
        /// N17c - Valor do Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 14)]
        public decimal? vFCP
        {
            get { return _vFcp.Arredondar(2); }
            set { _vFcp = value.Arredondar(2); }
        }

        public bool vFCPSpecified
        {
            get { return vFCP.HasValue; }
        }

        /// <summary>
        /// N17d - Percentual do diferimento do ICMS relativo ao Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 15)]
        public decimal? pFCPDif
        {
            get { return _pFCPDif.Arredondar(4); }
            set { _pFCPDif = value.Arredondar(4); }
        }

        /// <summary>
        /// N17e - Valor do ICMS relativo ao Fundo de Combate à Pobreza (FCP) diferido
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 16)]
        public decimal? vFCPDif
        {
            get { return _vFCPDif.Arredondar(2); }
            set { _vFCPDif = value.Arredondar(2); }
        }

        /// <summary>
        /// N17f - Valor efetivo do ICMS relativo ao Fundo de Combate à Pobreza (FCP)
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 17)]
        public decimal? vFCPEfet
        {
            get { return _vFCPEfet.Arredondar(2); }
            set { _vFCPEfet = value.Arredondar(2); }
        }

        /// <summary>
        ///     N18 - Modalidade de determinação da BC do ICMS ST
        /// </summary>
        [XmlElement(Order = 18)]
        public DeterminacaoBaseIcmsSt? modBCST { get; set; }

        /// <summary>
        ///     N19 - Percentual da margem de valor Adicionado do ICMS ST
        /// </summary>
        [XmlElement(Order = 19)]
        public decimal? pMVAST
        {
            get { return _pMvast.Arredondar(4); }
            set { _pMvast = value.Arredondar(4); }
        }

        /// <summary>
        ///     N20 - Percentual da Redução de BC do ICMS ST
        /// </summary>
        [XmlElement(Order = 20)]
        public decimal? pRedBCST
        {
            get { return _pRedBcst.Arredondar(4); }
            set { _pRedBcst = value.Arredondar(4); }
        }

        /// <summary>
        ///     N21 - Valor da BC do ICMS ST
        /// </summary>
        [XmlElement(Order = 21)]
        public decimal? vBCST
        {
            get { return _vBcst.Arredondar(2); }
            set { _vBcst = value.Arredondar(2); }
        }

        /// <summary>
        ///     N22 - Alíquota do imposto do ICMS ST
        /// </summary>
        [XmlElement(Order = 22)]
        public decimal? pICMSST
        {
            get { return _pIcmsst.Arredondar(4); }
            set { _pIcmsst = value.Arredondar(4); }
        }

        /// <summary>
        ///     N23 - Valor do ICMS ST
        /// </summary>
        [XmlElement(Order = 23)]
        public decimal? vICMSST
        {
            get { return _vIcmsst.Arredondar(2); }
            set { _vIcmsst = value.Arredondar(2); }
        }

        /// <summary>
        /// N23a - Valor da Base de Cálculo do FCP retido por Substituição Tributária
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 24)]
        public decimal? vBCFCPST
        {
            get { return _vBcfcpst.Arredondar(2); }
            set { _vBcfcpst = value.Arredondar(2); }
        }

        public bool vBCFCPSTSpecified
        {
            get { return vBCFCPST.HasValue; }
        }

        /// <summary>
        /// N23b - Percentual do FCP retido por Substituição Tributária
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 25)]
        public decimal? pFCPST
        {
            get { return _pFcpst.Arredondar(4); }
            set { _pFcpst = value.Arredondar(4); }
        }

        public bool pFCPSTSpecified
        {
            get { return pFCPST.HasValue; }
        }

        /// <summary>
        /// N23d - Valor do FCP retido por Substituição Tributária
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 26)]
        public decimal? vFCPST
        {
            get { return _vFcpst.Arredondar(2); }
            set { _vFcpst = value.Arredondar(2); }
        }

        public bool vFCPSTSpecified
        {
            get { return vFCPST.HasValue; }
        }

        /// <summary>
        ///     N27a - Valor do ICMS desonerado
        /// </summary>
        [XmlElement(Order = 27)]
        public decimal? vICMSDeson
        {
            get { return _vIcmsDeson.Arredondar(2); }
            set { _vIcmsDeson = value.Arredondar(2); }
        }

        /// <summary>
        ///     N28 - Motivo da desoneração do ICMS
        /// </summary>
        [XmlElement(Order = 28)]
        public MotivoDesoneracaoIcms? motDesICMS { get; set; }
        
        /// <summary>
        /// N28b - Indica se o valor do ICMS desonerado (vICMSDeson) deduz 
        /// do valor do item (vProd). (NT 2023.004) 
        /// </summary>
        [XmlElement(Order = 29)]
        public DeduzDesoneracaoNoProduto? indDeduzDeson { get; set; }

        /// <summary>
        /// N33a - Valor do ICMS- ST desonerado
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 30)]
        public decimal? vICMSSTDeson
        {
            get { return _vICMSSTDeson.Arredondar(2); }
            set { _vICMSSTDeson = value.Arredondar(2); }
        }

        public bool ShouldSerializevICMSSTDeson()
        {
            return vICMSSTDeson.HasValue;
        }

        /// <summary>
        /// N33b - Motivo da desoneração do ICMS- ST 
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 31)]
        public MotivoDesoneracaoIcmsSt? motDesICMSST { get; set; }

        public bool ShouldSerializemotDesICMSST()
        {
            return motDesICMSST.HasValue;
        }

        public bool ShouldSerializemodBC()
        {
            return modBC.HasValue;
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepRedBC()
        {
            return pRedBC.HasValue;
        }

        public bool ShouldSerializepICMS()
        {
            return pICMS.HasValue;
        }

        public bool ShouldSerializevICMSOp()
        {
            return vICMSOp.HasValue;
        }

        public bool ShouldSerializepDif()
        {
            return pDif.HasValue;
        }

        public bool ShouldSerializevICMSDif()
        {
            return vICMSDif.HasValue;
        }

        public bool ShouldSerializevICMS()
        {
            return vICMS.HasValue;
        }

        public bool ShouldSerializepFCPDif()
        {
            return pFCPDif.HasValue;
        }

        public bool ShouldSerializevFCPDif()
        {
            return vFCPDif.HasValue;
        }

        public bool ShouldSerializevFCPEfet()
        {
            return vFCPEfet.HasValue;
        }

        public bool ShouldSerializemodBCST()
        {
            return modBCST.HasValue;
        }

        public bool ShouldSerializepMVAST()
        {
            return pMVAST.HasValue;
        }

        public bool ShouldSerializepRedBCST()
        {
            return pRedBCST.HasValue;
        }

        public bool ShouldSerializevBCST()
        {
            return vBCST.HasValue;
        }

        public bool ShouldSerializepICMSST()
        {
            return pICMSST.HasValue;
        }

        public bool ShouldSerializevICMSST()
        {
            return vICMSST.HasValue;
        }

        public bool ShouldSerializevICMSDeson()
        {
            return vICMSDeson.HasValue;
        }

        public bool ShouldSerializemotDesICMS()
        {
            return motDesICMS.HasValue;
        }

        public bool ShouldSerializeindDeduzDeson()
        {
            return indDeduzDeson.HasValue;
        }
    }
}
