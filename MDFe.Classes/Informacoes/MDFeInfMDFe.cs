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
using MDFe.Utils.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfMDFe
    {
        public MDFeInfMDFe()
        {
            Ide = new MDFeIde();
            Emit = new MDFeEmit();
            InfModal = new MDFeInfModal();
            InfDoc = new MDFeInfDoc();
            Tot = new MDFeTot();
            Versao = VersaoServico.Versao100;
        }
        /// <summary>
        /// 1 - Versão do leiaute 
        /// </summary>
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        /// <summary>
        /// 1 - Identificador da tag a ser assinada. 
        /// Informar a chave de acesso do MDF-e e
        /// precedida do literal "MDFe" 
        /// </summary>
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        /// <summary>
        /// 1 - Identificação do MDF-e
        /// </summary>
        [XmlElement(ElementName = "ide")]
        public MDFeIde Ide { get; set; }

        /// <summary>
        /// 1 - Identificação do Emitente do Manifesto
        /// </summary>
        [XmlElement(ElementName = "emit")]
        public MDFeEmit Emit { get; set; }

        /// <summary>
        /// 1 - Informações do modal
        /// </summary>
        [XmlElement(ElementName = "infModal")]
        public MDFeInfModal InfModal { get; set; }

        /// <summary>
        /// 1 - Informações dos Documentos fiscais vinculados ao manifesto
        /// </summary>
        [XmlElement(ElementName = "infDoc")]
        public MDFeInfDoc InfDoc { get; set; }

        /// <summary>
        /// 1 - Informações de Seguro da carga
        /// MDF-e 3.0
        /// </summary>
        [XmlElement(ElementName = "seg")]
        public List<MDFeSeg> Seg { get; set; }

        [XmlElement(ElementName = "prodPred")]
        public prodPred prodPred { get; set; }

        /// <summary>
        /// 1 - Totalizadores da carga transportada e seus documentos fiscais
        /// </summary>
        [XmlElement(ElementName = "tot")]
        public MDFeTot Tot { get; set; }

        /// <summary>
        /// 1 - Lacres do MDF-e
        /// </summary>
        [XmlElement(ElementName = "lacres")]
        public List<MDFeLacre> Lacres { get; set; }

        /// <summary>
        /// 1 - Autorizados para download do XML do DF-e
        /// </summary>
        [XmlElement(ElementName = "autXML")]
        public List<MDFeAutXML> AutXml { get; set; }

        /// <summary>
        /// 1 - Informações Adicionais
        /// </summary>
        [XmlElement(ElementName = "infAdic")]
        public MDFeInfAdic InfAdic { get; set; }

        [XmlElement(ElementName = "infRespTec")]
        public infRespTec infRespTec { get; set; }
    }
}
