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
using MDFe.Classes.Contratos;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeRodo : MDFeModalContainer
    {
        [XmlElement(ElementName = "infANTT")]
        public MDFeInfANTT infANTT { get; set; }

        /// <summary>
        /// 1 - Registro Nacional de Transportadores Rodoviários de Carga
        /// </summary>
        [XmlElement(ElementName = "RNTRC")]
        public string RNTRC { get; set; }

        /// <summary>
        /// 1 - Código Identificador da Operação de Transporte
        /// </summary>
        [XmlElement(ElementName = "CIOT")]
        public string CIOT { get; set; }

        /// <summary>
        /// 1 - Dados do Veículo com a Tração
        /// </summary>
        [XmlElement(ElementName = "veicTracao")]
        public MDFeVeicTracao VeicTracao { get; set; }

        /// <summary>
        /// 1 - Dados dos reboques
        /// </summary>
        [XmlElement(ElementName = "veicReboque")]
        public List<MDFeVeicReboque> VeicReboque { get; set; }

        /// <summary>
        /// 1 - Informações de Vale Pedágio
        /// </summary>
        [XmlElement(ElementName = "valePed")]
        public MDFeValePed ValePed { get; set; }

        /// <summary>
        /// 1 - Código de Agendamento no porto 
        /// </summary>
        [XmlElement(ElementName = "codAgPorto")]
        public string CodAgPorto { get; set; }

        [XmlElement(ElementName = "lacRodo")]
        public List<MDFeLacre> lacRodo { get; set; }
    }

    [Serializable]
    public class MDFeInfANTT
    {
        [XmlElement(ElementName = "RNTRC")]
        public string RNTRC { get; set; }

        [XmlElement(ElementName = "infCIOT")]
        public List<infCIOT> infCIOT { get; set; }

        public MDFeValePed valePed { get; set; }

        [XmlElement(ElementName = "infContratante")]
        public List<infContratante> infContratante { get; set; }

        [XmlElement(ElementName = "infPag")]
        public List<infPag> infPag { get; set; }
    }
}