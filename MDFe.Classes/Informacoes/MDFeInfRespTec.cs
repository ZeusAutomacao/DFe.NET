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

namespace MDFe.Classes.Informacoes
{
    public class MDFeInfRespTec
    {
        /// <summary>
        /// 2 - CNPJ da pessoa jurídica responsável técnica pelo sistema
        /// utilizado na emissão do documento fiscal eletrônico
        /// </summary>
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// 2 - Nome da pessoa a ser contatada 
        /// </summary>
        [XmlElement(ElementName = "xContato")]
        public string XContato { get; set; }

        /// <summary>
        /// 2 - E-mail da pessoa jurídica a ser contatada
        /// </summary>
        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// 2 - Telefone da pessoa jurídica a ser contatada
        /// </summary>
        [XmlElement(ElementName = "fone")]
        public string Fone { get; set; }

        [XmlIgnore]
        private int? IdCSRT { get; set; }

        public bool IdCSRTSpecified
        {
            get { return IdCSRT.HasValue; }
        }

        /// <summary>
        /// 2 - Identificador do código de segurança do responsável técnico
        /// </summary>
        [XmlElement(ElementName = "idCSRT")]
        public string ProxyIdCSRT
        {
            get { return IdCSRT != null ? IdCSRT.Value.ToString("D3") : null; }
            set { IdCSRT = int.Parse(value); }
        }

        /// <summary>
        /// 2 - Hash do token do código de segurança do responsável técnico
        /// </summary>
        [XmlElement(ElementName = "hashCSRT")]
        public string HashCSRT { get; set; }
    }
}