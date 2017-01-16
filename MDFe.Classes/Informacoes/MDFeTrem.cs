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
using DFe.Utils;
using MDFe.Classes.Contratos;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeTrem : MDFeModalContainer
    {
        /// <summary>
        /// 2 - Prefixo do Trem 
        /// </summary>
        [XmlElement(ElementName = "xPref")]
        public string XPref { get; set; }

        /// <summary>
        /// 2 - Data e hora de liberação do trem na origem
        /// </summary>
        [XmlIgnore]
        public DateTime? DhTrem { get; set; }

        /// <summary>
        /// Proxy para covnerter dhTrem em string yyyy-MM-ddTHH:mm:dd
        /// </summary>
        [XmlElement(ElementName = "dhTrem")]
        public string ProxyDhTrem {
            get { return DhTrem.ParaDataHoraStringUtc(); }
            set { DhTrem = DateTime.Parse(value); }
        }

        /// <summary>
        /// 2 - Origem do Trem 
        /// </summary>
        [XmlElement(ElementName = "xOri")]
        public string XOri { get; set; }

        /// <summary>
        /// 2 - Destino do Trem 
        /// </summary>
        [XmlElement(ElementName = "xDest")]
        public string XDest { get; set; }

        /// <summary>
        /// 2 - Quantidade de vagões carregados 
        /// </summary>
        [XmlElement(ElementName = "qVag")]
        public short QVag { get; set; }
    }
}