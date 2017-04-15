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
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfMDFeTransp
    {
        /// <summary>
        ///  4 - Manifesto Eletrônico de Documentos Fiscais
        /// </summary>
        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }

        /// <summary>
        /// Indicador de Reentrega
        /// Versão 3.0
        /// </summary>
        [XmlElement(ElementName = "indReentrega")]
        public byte? IndReentrega { get; set; }

        public bool IndReentregaSpecified { get { return IndReentrega.HasValue; } }

        /// <summary>
        /// 4 - Informações das Unidades de Transporte (Carreta/Reboque/Vagão) 
        /// </summary>
        [XmlElement(ElementName = "infUnidTransp")]
        public List<MDFeInfUnidTransp> InfUnidTransp { get; set; }

        /// <summary>
        /// Preenchido quando for transporte de produtos classificados
        /// pela ONU como perigosos.
        /// MDF-e 3.0
        /// </summary>
        [XmlElement(ElementName = "peri")]
        public List<MDFePeri> Peri { get; set; }
    }
}