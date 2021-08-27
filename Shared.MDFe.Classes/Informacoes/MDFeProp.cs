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
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeProp
    {
        /// <summary>
        /// 3 - Número do CPF 
        /// </summary>
        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }

        /// <summary>
        /// 3 - Número do CNPJ 
        /// </summary>
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// 3 - Registro Nacional dos Transportadores Rodoviários de Carga
        /// </summary>
        [XmlElement(ElementName = "RNTRC")]
        public string RNTRC { get; set; }

        /// <summary>
        /// 3 - Razão Social ou Nome do proprietário 
        /// </summary>
        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        /// <summary>
        /// 3 - Inscrição Estadual 
        /// </summary>
        [XmlElement(ElementName = "IE")]
        public string IE { get; set; }

        /// <summary>
        /// 3 - UF
        /// </summary>
        [XmlIgnore]
        public Estado UF { get; set; }

        /// <summary>
        /// Proxy para obter a sigla uf 
        /// </summary>
        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get
            {
                return UF.GetSiglaUfString();
            }
            set { UF = UF.SiglaParaEstado(value); }
        }

        /// <summary>
        /// 3 - Tipo Proprietário 
        /// </summary>
        [XmlElement(ElementName = "tpProp")]
        public MDFeTpProp MDFeTpProp { get; set; }
    }
}