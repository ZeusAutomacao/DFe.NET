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
    public class ICMS60 : ICMSBasico
    {
        private decimal? _vBcstRet;
        private decimal? _vIcmsstRet;
        private decimal? _vBcfcpstRet;
        private decimal? _pST;
        private decimal? _pFcpstRet;
        private decimal? _vFcpstRet;
        private decimal? _pRedBCEfet;
        private decimal? _vBCEfet;
        private decimal? _pICMSEfet;
        private decimal? _vICMSEfet;
        private decimal? _vIcmsSubstituto;

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
        ///     N26 - Valor da BC do ICMS ST retido
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal? vBCSTRet
        {
            get { return _vBcstRet.Arredondar(2); }
            set { _vBcstRet = value.Arredondar(2); }
        }

        public bool ShouldSerializevBCSTRet()
        {
            return vBCSTRet.HasValue;
        }

        /// <summary>
        ///     N26a - Alíquota suportada pelo Consumidor Final
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal? pST
        {
            get { return _pST.Arredondar(4); }
            set { _pST = value.Arredondar(4); }
        }

        public bool ShouldSerializepST()
        {
            return pST.HasValue;
        }

        [XmlElement(Order = 5)]
        public decimal? vICMSSubstituto
        {
            get { return _vIcmsSubstituto.Arredondar(2); }
            set { _vIcmsSubstituto = value.Arredondar(2); }
        }

        public bool ShouldSerializevICMSSubstituto()
        {
            return _vIcmsSubstituto.HasValue;
        }

        /// <summary>
        ///     N27 - Valor do ICMS ST retido
        /// </summary>
        [XmlElement(Order = 6)]
        public decimal? vICMSSTRet
        {
            get { return _vIcmsstRet.Arredondar(2); }
            set { _vIcmsstRet = value.Arredondar(2); }
        }

        public bool ShouldSerializevICMSSTRet()
        {
            return vICMSSTRet.HasValue;
        }

        /// <summary>
        /// N27a - Valor da Base de Cálculo do FCP retido anteriormente por ST 
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal? vBCFCPSTRet
        {
            get { return _vBcfcpstRet.Arredondar(2); }
            set { _vBcfcpstRet = value.Arredondar(2); }
        }

        public bool vBCFCPSTRetSpecified
        {
            get { { return vBCFCPSTRet.HasValue; } }
        }

        /// <summary>
        /// N27b - Percentual do FCP retido anteriormente por Substituição Tributária
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal? pFCPSTRet
        {
            get { return _pFcpstRet.Arredondar(4); }
            set { _pFcpstRet = value.Arredondar(4); }
        }

        public bool pFCPSTRetSpecified
        {
            get { return pFCPSTRet.HasValue; }
        }


        /// <summary>
        /// N27d - Valor do FCP retido por Substituição Tributária
        /// Versão 4.00
        /// </summary>
        [XmlElement(Order = 9)]
        public decimal? vFCPSTRet
        {
            get { return _vFcpstRet.Arredondar(2); }
            set { _vFcpstRet = value.Arredondar(2); }
        }

        public bool vFCPSTRetSpecified
        {
            get { return vFCPSTRet.HasValue; }
        }

        /// <summary>
        ///     N34 - Percentual de redução da base de cálculo efetiva 
        /// </summary>
        [XmlElement(Order = 10)]
        public decimal? pRedBCEfet
        {
            get { return _pRedBCEfet.Arredondar(4); }
            set { _pRedBCEfet = value.Arredondar(4); }
        }

        public bool ShouldSerializepRedBCEfet()
        {
            return pRedBCEfet.HasValue;
        }

        /// <summary>
        ///     N35 - Valor da base de cálculo efetiva 
        /// </summary>
        [XmlElement(Order = 11)]
        public decimal? vBCEfet
        {
            get { return _vBCEfet.Arredondar(2); }
            set { _vBCEfet = value.Arredondar(2); }
        }

        public bool ShouldSerializevBCEfet()
        {
            return vBCEfet.HasValue;
        }

        /// <summary>
        ///     N36 - Alíquota do ICMS efetiva 
        /// </summary>
        [XmlElement(Order = 12)]
        public decimal? pICMSEfet
        {
            get { return _pICMSEfet.Arredondar(4); }
            set { _pICMSEfet = value.Arredondar(4); }
        }

        public bool ShouldSerializepICMSEfet()
        {
            return pICMSEfet.HasValue;
        }

        /// <summary>
        ///     N37 - Valor do ICMS efetivo 
        /// </summary>
        [XmlElement(Order = 13)]
        public decimal? vICMSEfet
        {
            get { return _vICMSEfet.Arredondar(2); }
            set { _vICMSEfet = value.Arredondar(2); }
        }

        public bool ShouldSerializevICMSEfet()
        {
            return vICMSEfet.HasValue;
        }

    }
}