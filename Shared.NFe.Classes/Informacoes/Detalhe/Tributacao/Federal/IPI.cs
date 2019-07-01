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
    public class IPI
    {
        /// <summary>
        ///     O02 - Classe de enquadramento do IPI para Cigarros e Bebidas
        /// Versão 3.10
        /// </summary>
        public string clEnq { get; set; }

        /// <summary>
        ///     O03 - CNPJ do produtor da mercadoria, quando diferente do emitente. Somente para os casos de exportação direta ou
        ///     indireta.
        /// </summary>
        public string CNPJProd { get; set; }

        /// <summary>
        ///     O04 - Código do selo de controle IPI
        /// </summary>
        public string cSelo { get; set; }

        /// <summary>
        ///     O05 - Quantidade de selo de controle
        /// </summary>
        public int? qSelo { get; set; }

        /// <summary>
        ///     O06 - Código de Enquadramento Legal do IPI
        /// </summary>
        [XmlIgnore]
        public int cEnq { get; set; }

        [XmlElement(ElementName = "cEnq")]
        public string ProxycEnq
        {
            get { return cEnq.ToString("D3"); }
            set { cEnq = int.Parse(value); }
        }

        /// <summary>
        ///     O07 (IPITrib) - Grupo do CST 00, 49, 50 e 99
        ///     O08 (IPINT) - Grupo CST 01, 02, 03, 04, 51, 52, 53, 54 e 55
        /// </summary>
        [XmlElement("IPITrib", typeof (IPITrib))]
        [XmlElement("IPINT", typeof (IPINT))]
        public IPIBasico TipoIPI { get; set; }

        public bool ShouldSerializeqSelo()
        {
            return qSelo.HasValue;
        }
    }
}