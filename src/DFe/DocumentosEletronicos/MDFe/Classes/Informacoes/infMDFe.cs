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
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Adicionais;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.AutorizadoDownloadXml;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.DocumentosFiscais;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Emitente;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Identificacao;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Lacres;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Modal;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Seguro;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Totalizadores;

namespace DFe.DocumentosEletronicos.MDFe.Classes.Informacoes
{
    [Serializable]
    public class infMDFe
    {
        public infMDFe()
        {
            ide = new ide();
            emit = new emit();
            infModal = new infModal();
            infDoc = new infDoc();
            tot = new tot();
        }
        /// <summary>
        /// 1 - Versão do leiaute 
        /// </summary>
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico versao { get; set; }

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
        public ide ide { get; set; }

        /// <summary>
        /// 1 - Identificação do Emitente do Manifesto
        /// </summary>
        [XmlElement(ElementName = "emit")]
        public emit emit { get; set; }

        /// <summary>
        /// 1 - Informações do modal
        /// </summary>
        [XmlElement(ElementName = "infModal")]
        public infModal infModal { get; set; }

        /// <summary>
        /// 1 - Informações dos Documentos fiscais vinculados ao manifesto
        /// </summary>
        [XmlElement(ElementName = "infDoc")]
        public infDoc infDoc { get; set; }

        /// <summary>
        /// 1 - Informações de Seguro da carga
        /// MDF-e 3.0
        /// </summary>
        [XmlElement(ElementName = "seg")]
        public List<seg> Seg { get; set; }

        /// <summary>
        /// 1 - Totalizadores da carga transportada e seus documentos fiscais
        /// </summary>
        [XmlElement(ElementName = "tot")]
        public tot tot { get; set; }

        /// <summary>
        /// 1 - Lacres do MDF-e
        /// </summary>
        [XmlElement(ElementName = "lacres")]
        public List<lacres> lacres { get; set; }

        /// <summary>
        /// 1 - Autorizados para download do XML do DF-e
        /// </summary>
        [XmlElement(ElementName = "autXML")]
        public List<autXML> autXml { get; set; }

        /// <summary>
        /// 1 - Informações Adicionais
        /// </summary>
        [XmlElement(ElementName = "infAdic")]
        public infAdic infAdic { get; set; }
    }
}
