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
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.MDFe.Classes.Servicos.Autorizacao;
using DFe.Utils;

namespace DFe.MDFe.Classes.Retorno.Autorizacao
{
    [Serializable]
    [XmlRoot(ElementName = "retEnviMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class retEnviMDFe : RetornoBase
    {
        [XmlAttribute(AttributeName = "versao")]
        public string versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente tpAmb { get; set; }

        [XmlIgnore]
        public Estado cUF { get; set; }

        [XmlElement(ElementName = "cUF")]
        public string cUFProxy
        {
            get
            {
                return cUF.GetCodigoIbgeEmString();
            }
            set { cUF = cUF.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "verAplic")]
        public string verAplic { get; set; }

        [XmlElement(ElementName = "cStat")]
        public short cStat { get; set; }

        [XmlElement(ElementName = "xMotivo")]
        public string xMotivo { get; set; }

        [XmlElement(ElementName = "infRec")]
        public infRec infRec { get; set; }

        [XmlIgnore]
        public string RetornoCompleto { get; set; }

        public static retEnviMDFe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retEnviMDFe>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }

        public static retEnviMDFe LoadXml(string xml, enviMDFe enviMDFe)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(enviMDFe.MDFe);
            retorno.RetornoCompleto = FuncoesXml.ClasseParaXmlString(enviMDFe);

            return retorno;
        }

        public bool IsAutorizado()
        {
            return cStat == 103;
        }

        public bool IsRejeicao()
        {
            return cStat != 103;
        }
    }
}