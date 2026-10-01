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
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Servicos.Evento
{
    public sealed class itensAverbados
    {
        /// <summary>
        /// Data do Embarque no formato AAAA-MM-DDThh:mm:ssTZD
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset DhEmbarque { get; set; }

        [XmlElement(ElementName = "dhEmbarque")]
        public string ProxyDhEmbarque
        {
            get { return DhEmbarque.ParaDataHoraStringUtc(); }
            set { DhEmbarque = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Proxy Data da averbação no formato AAAA-MM-DDThh:mm:ssTZD
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset DhAverbacao { get; set; }

        [XmlElement(ElementName = "dhAverbacao")]
        public string ProxyDhAverbacao
        {
            get { return DhAverbacao.ParaDataHoraStringUtc(); }
            set { DhAverbacao = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Número Identificador da Declaração Única do Comércio Exterior associada - [0-9]{2}BR[0-9]{10}
        /// </summary>
        [XmlElement(ElementName = "nDue")]
        public string NDue { get; set; }

        /// <summary>
        /// Número do item da NF-e averbada - [0-9]{1,3}
        /// </summary>
        [XmlElement(ElementName = "nItem")]
        public string NItem { get; set; }

        /// <summary>
        /// Informação do número do item na Declaração de Exportação associada a averbação. - [0-9]{1,4}
        /// </summary>
        [XmlElement(ElementName = "nItemDue")]
        public string NItemDue { get; set; }

        /// <summary>
        /// Quantidade averbada do item na unidade tributária (campo uTrib) - TDec_1104Neg
        /// </summary>
        [XmlElement(ElementName = "qItem")]
        public decimal QItem { get; set; }

        /// <summary>
        ///  Motivo da Alteração
        ///    1 - Exportação Averbada;
        ///    2 - Retificação da Quantidade Averbada;
        ///    3 - Cancelamento da Exportação;
        /// </summary>
        [XmlElement(ElementName = "motAlteracao")]
        public int MotAlteracao { get; set; }
        
        /// <summary>
        /// P29 - Grupo de informações do enquadramento do item 
        /// </summary>
        [XmlElement(ElementName = "enquad")]
        public enquad Enquad { get; set; } 
    }
}
