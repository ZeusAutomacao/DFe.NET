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
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Utils.Configuracoes;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Classes.Informacoes.Evento
{
    [Serializable]
    public class MDFeInfEvento
    {
        [XmlIgnore]
        private readonly VersaoServico _versaoServico = MDFeConfiguracao.VersaoWebService.VersaoLayout;

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlIgnore]
        public Estado COrgao { get; set; }

        [XmlElement(ElementName = "cOrgao")]
        public string COrgaoProxy
        {
            get
            {
                return COrgao.GetCodigoIbgeEmString();
            }
            set { COrgao = COrgao.CodigoIbgeParaEstado(value); }
        }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }

        [XmlIgnore]
        public DateTime DhEvento { get; set; }

        [XmlElement(ElementName = "dhEvento")]
        public string ProxyDhEvento
        {
            get
            {
                switch (_versaoServico)
                {
                    case VersaoServico.Versao100:
                        return DhEvento.ParaDataHoraStringSemUtc();
                    case VersaoServico.Versao300:
                        return DhEvento.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão inválida do mdf-e");
                }
            }
            set { DhEvento = DateTime.Parse(value); }
        }

        [XmlElement(ElementName = "tpEvento")]
        public MDFeTipoEvento TpEvento { get; set; }

        [XmlElement(ElementName = "nSeqEvento")]
        public byte NSeqEvento { get; set; }

        [XmlElement(ElementName = "detEvento")]
        public MDFeDetEvento DetEvento { get; set; }
    }
}