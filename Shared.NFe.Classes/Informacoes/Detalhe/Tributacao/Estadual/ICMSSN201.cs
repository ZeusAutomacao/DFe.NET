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
    public class ICMSSN201 : ICMSBasico
    {
        private decimal? _pMvast;
        private decimal? _pRedBcst;
        private decimal _vBcst;
        private decimal _pIcmsst;
        private decimal _vIcmsst;
        private decimal _pCredSn;
        private decimal _vCredIcmssn;
        private decimal? _vBcfcpst;
        private decimal? _pFcpst;
        private decimal? _vFcpst;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        [XmlElement(Order = 1)]
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12a - Código de Situação da Operação – Simples Nacional
        /// </summary>
        [XmlElement(Order = 2)]
        public Csosnicms CSOSN { get; set; }

        /// <summary>
        ///     N18 - Modalidade de determinação da BC do ICMS ST
        /// </summary>
        [XmlElement(Order = 3)]
        public DeterminacaoBaseIcmsSt modBCST { get; set; }

        /// <summary>
        ///     N19 - Percentual da margem de valor Adicionado do ICMS ST
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? pMVAST
        {
            get { return _pMvast.Arredondar(4); }
            set { _pMvast = value.Arredondar(4); }
        }

        /// <summary>
        ///     N20 - Percentual da Redução de BC do ICMS ST
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? pRedBCST
        {
            get { return _pRedBcst.Arredondar(4); }
            set { _pRedBcst = value.Arredondar(4); }
        }

        /// <summary>
        ///     N21 - Valor da BC do ICMS ST
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal vBCST
        {
            get { return _vBcst; }
            set { _vBcst = value.Arredondar(2); }
        }

        /// <summary>
        ///     N22 - Alíquota do imposto do ICMS ST
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal pICMSST
        {
            get { return _pIcmsst; }
            set { _pIcmsst = value.Arredondar(4); }
        }

        /// <summary>
        ///     N23 - Valor do ICMS ST
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal vICMSST
        {
            get { return _vIcmsst; }
            set { _vIcmsst = value.Arredondar(2); }
        }

        /// <summary>
        /// N23a - Valor da Base de Cálculo do FCP retido por Substituição Tributária
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 9)]
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
        [XmlElement(Order = 10)]
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
        [XmlElement(Order = 11)]
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
        ///     N29 - pCredSN - Alíquota aplicável de cálculo do crédito (Simples Nacional).
        /// </summary>
        [XmlElement(Order = 12)]
        public decimal pCredSN
        {
            get { return _pCredSn; }
            set { _pCredSn = value.Arredondar(4); }
        }

        /// <summary>
        ///     N30 - Valor crédito do ICMS que pode ser aproveitado nos termos do art. 23 da LC 123 (Simples Nacional)
        /// </summary>
        [XmlElement(Order = 13)]
        public decimal vCredICMSSN
        {
            get { return _vCredIcmssn; }
            set { _vCredIcmssn = value.Arredondar(2); }
        }

        public bool ShouldSerializepMVAST()
        {
            return pMVAST.HasValue;
        }

        public bool ShouldSerializepRedBCST()
        {
            return pRedBCST.HasValue;
        }
    }
}