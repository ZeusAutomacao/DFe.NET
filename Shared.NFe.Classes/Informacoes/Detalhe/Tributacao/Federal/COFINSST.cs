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
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class COFINSST : COFINSBasico
    {
        private decimal? _vBc;
        private decimal? _pCofins;
        private decimal? _qBcProd;
        private decimal? _vAliqProd;
        private decimal? _vCofins;

        /// <summary>
        ///     T02 - Valor da Base de Cálculo da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 1)]
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     T03 - Alíquota da COFINS (em percentual)
        /// </summary>
        /// 
        [XmlElement(Order = 2)]
        public decimal? pCOFINS
        {
            get { return _pCofins.Arredondar(4); }
            set { _pCofins = value.Arredondar(4); }
        }

        /// <summary>
        ///     T04 - Quantidade Vendida
        /// </summary>
        /// 
        [XmlElement(Order = 3)]
        public decimal? qBCProd
        {
            get { return _qBcProd.Arredondar(4); }
            set { _qBcProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     T05 - Alíquota da COFINS (em reais)
        /// </summary>
        /// 
        [XmlElement(Order = 4)]
        public decimal? vAliqProd
        {
            get { return _vAliqProd.Arredondar(4); }
            set { _vAliqProd = value.Arredondar(4); }
        }

        /// <summary>
        ///     T06 - Valor da COFINS
        /// </summary>
        /// 
        [XmlElement(Order = 5)]
        public decimal? vCOFINS
        {
            get { return _vCofins.Arredondar(2); }
            set { _vCofins = value.Arredondar(2); }
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepCOFINS()
        {
            return pCOFINS.HasValue;
        }

        public bool ShouldSerializeqBCProd()
        {
            return qBCProd.HasValue;
        }

        public bool ShouldSerializevAliqProd()
        {
            return vAliqProd.HasValue;
        }

        public bool ShouldSerializevCOFINS()
        {
            return vCOFINS.HasValue;
        }
    }
}