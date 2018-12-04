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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMSSN500 : ICMSBasico
    {
        private decimal? _vBcstRet;
        private decimal? _pST;
        private decimal? _vIcmsstRet;
        private decimal? _vBCFCPSTRet;
        private decimal? _pFCPSTRet;
        private decimal? _vFCPSTRet;
        private decimal? _pRedBcEfet;
        private decimal? _vBcEfet;
        private decimal? _pIcmsEfet;
        private decimal? _vIcmsEfet;

        /// <summary>
        ///     N11 - Origem da Mercadoria
        /// </summary>
        public OrigemMercadoria orig { get; set; }

        /// <summary>
        ///     N12a - Código de Situação da Operação – Simples Nacional
        /// </summary>
        public Csosnicms CSOSN { get; set; }

        /// <summary>
        /// N27d - Valor do FCP retido por Substituição Tributária
        /// </summary>
        public decimal? vFCPSTRet
        {
            get { return _vFCPSTRet.Arredondar(2); }
            set { _vFCPSTRet = value.Arredondar(2); }
        }

        public bool ShouldSerializevFCPSTRet()
        {
            return vFCPSTRet.HasValue && vFCPSTRet.Value > 0;
        }

        /// <summary>
        /// N27e - Alíquota suportada pelo Consumidor Final
        /// </summary>
        public decimal? pST
        {
            get { return _pST.Arredondar(2); }
            set { _pST = value.Arredondar(2); }
        }

        public bool ShouldSerializepST()
        {
            return pST.HasValue && pST.Value > 0;
        }

        /// <summary>
        ///     N27 - Valor do ICMS ST retido
        /// </summary>
        public decimal? vICMSSTRet
        {
            get { return _vIcmsstRet.Arredondar(2); }
            set { _vIcmsstRet = value.Arredondar(2); }
        }

        public bool ShouldSerializevBCSTRet()
        {
            return vBCSTRet.HasValue && vBCSTRet.Value > 0;
        }

        /// <summary>
        ///     N26 - Valor da BC do ICMS ST retido
        /// </summary>
        public decimal? vBCSTRet
        {
            get { return _vBcstRet.Arredondar(2); }
            set { _vBcstRet = value.Arredondar(2); }
        }

        public bool ShouldSerializevICMSSTRet()
        {
            return vICMSSTRet.HasValue && vICMSSTRet.Value > 0;
        }

        /// <summary>
        /// N27a - Valor da Base de Cálculo do FCP retido anteriormente
        /// </summary>
        public decimal? vBCFCPSTRet
        {
            get { return _vBCFCPSTRet.Arredondar(2); }
            set { _vBCFCPSTRet = value.Arredondar(2); }
        }

        public bool ShouldSerializevBCFCPSTRet()
        {
            return vBCFCPSTRet.HasValue && vBCFCPSTRet.Value > 0;
        }

        /// <summary>
        /// N27b - Percentual do FCP retido anteriormente por Substituição Tributária
        /// </summary>
        public decimal? pFCPSTRet
        {
            get { return _pFCPSTRet.Arredondar(2); }
            set { _pFCPSTRet = value.Arredondar(2); }
        }

        public bool ShouldSerializepFCPSTRet()
        {
            return pFCPSTRet.HasValue && pFCPSTRet.Value > 0;
        }

        /// <summary>
        /// N34 - Percentual de redução da base de cálculo efetiva
        /// </summary>
        public decimal? pRedBCEfet
        {
            get { return _pRedBcEfet.Arredondar(4); }
            set { _pRedBcEfet = value.Arredondar(4); }
        }

        public bool ShouldSerializepRedBCEfet()
        {
            return _pRedBcEfet.GetValueOrDefault() > 0;
        }

        /// <summary>
        /// N35 - Valor da base de cálculo efetiva 
        /// </summary>
        public decimal? vBCEfet
        {
            get { return _vBcEfet.Arredondar(2); }
            set { _vBcEfet = value.Arredondar(2); }
        }

        public bool ShouldSerializevBCEfet()
        {
            return _vBcEfet.GetValueOrDefault() > 0;
        }

        /// <summary>
        /// N36 - Alíquota do ICMS efetiva
        /// </summary>
        public decimal? pICMSEfet
        {
            get { return _pIcmsEfet.Arredondar(4); }
            set { _pIcmsEfet = value.Arredondar(4); }
        }

        public bool ShouldSerializepICMSEfet()
        {
            return _pIcmsEfet.GetValueOrDefault() > 0;
        }

        /// <summary>
        /// N37 - Valor do ICMS efetivo
        /// </summary>
        public decimal? vICMSEfet
        {
            get { return _vIcmsEfet.Arredondar(2); }
            set { _vIcmsEfet = value.Arredondar(2); }
        }

        public bool ShouldSerializevICMSEfet()
        {
            return _vIcmsEfet.GetValueOrDefault() > 0;
        }
    }
}