/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeProdPred
    {
        public MDFeProdPred()
        {
            InfLotacao = new MDFeInfLotacao();
        }

        /// <summary>
        /// 1 - Tipo da Carga.
        /// Conforme Rosulação ANTT
        /// </summary>
        [XmlElement(ElementName = "tpCarga")]
        public MDFeTpCarga TpCarga { get; set; }

        /// <summary>
        /// 1 - Descrição do produto predominante.
        /// </summary>
        [XmlElement(ElementName = "xProd")]
        public string XProd { get; set; }

        /// <summary>
        /// 1- GTIN (Global Trade Item Number) do produto, antigo código EAN ou código de barras.
        /// </summary>
        [XmlElement(ElementName = "cEAN")]
        public string CEan { get; set; }

        /// <summary>
        /// 1 - Código NCM
        /// </summary>
        [XmlElement(ElementName = "NCM")]
        public string Ncm { get; set; }

        /// <summary>
        /// 1 - Informações da carga lotação. Informar somente quando MDF-e for de carga lotação
        /// </summary>
        [XmlElement(ElementName = "infLotacao")]
        public MDFeInfLotacao InfLotacao { get; set; }
    }
}