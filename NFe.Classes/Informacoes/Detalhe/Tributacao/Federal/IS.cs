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

using System.Xml.Serialization;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class IS
    {
        private decimal _vBcIs;
        private decimal _pIs;
        private decimal? _pIsEspec;
        private decimal _qTrib;
        private decimal _vIs;
        
        /// <summary>
        ///     UB02 - Código de Situação Tributária do Imposto Seletivo
        /// </summary>
        [XmlElement(Order = 1)]
        public CSTIS CSTIS { get; set; }

        /// <summary>
        ///     UB03 - Código de Classificação Tributária do Imposto Seletivo
        /// </summary>
        [XmlElement(Order = 2)]
        public int cClassTribIS { get; set; }

        /// <summary>
        ///     UB05 - Valor da Base de Cálculo do Imposto Seletivo 
        /// </summary>
        [XmlElement(Order = 3)]
        public decimal vBCIS
        {
            get => _vBcIs.Arredondar(2);
            set => _vBcIs = value.Arredondar(2);
        }
        
        /// <summary>
        ///     UB06 - Alíquota do Imposto Seletivo
        /// </summary>
        [XmlElement(Order = 4)]
        public decimal pIS
        {
            get => _pIs.Arredondar(4);
            set => _pIs = value.Arredondar(4);
        }

        /// <summary>
        ///     UB07 - Alíquota específica por unidade de medida apropriada
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? pISEspec
        {
            get => _pIsEspec.Arredondar(4);
            set => _pIsEspec = value.Arredondar(4);
        }

        /// <summary>
        ///     UB09 - Unidade de Medida Tributável
        /// </summary>
        [XmlElement(Order = 6)]
        public string uTrib { get; set; }

        /// <summary>
        ///     UB10 - Quantidade Tributável
        /// </summary>
        [XmlElement(Order = 7)]
        public decimal qTrib
        {
            get => _qTrib.Arredondar(4);
            set => _qTrib = value.Arredondar(4);
        }
        
        /// <summary>
        ///     UB11 - Valor do Imposto Seletivo
        /// </summary>
        [XmlElement(Order = 8)]
        public decimal vIS
        {
            get => _vIs.Arredondar(2);
            set => _vIs = value.Arredondar(2);
        }
    }
}