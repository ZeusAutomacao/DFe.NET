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
    public class MDFeVeicReboque
    {
        /// <summary>
        /// 2 - Código interno do veículo
        /// </summary>
        [XmlElement(ElementName = "cInt")]
        public string CInt { get; set; }

        /// <summary>
        /// 2 - Placa do veículo 
        /// </summary>
        [XmlElement(ElementName = "placa")]
        public string Placa { get; set; }

        /// <summary>
        /// 2 - RENAVAM do veículo 
        /// </summary>
        [XmlElement(ElementName = "RENAVAM")]
        public string RENAVAM { get; set; }

        /// <summary>
        /// 2 - Tara em KG 
        /// </summary>
        [XmlElement(ElementName = "tara")]
        public int? Tara { get; set; }

        /// <summary>
        /// 2 - Capacidade em KG 
        /// </summary>
        [XmlElement(ElementName = "capKG")]
        public int? CapKG { get; set; }

        /// <summary>
        /// 2 - Capacidade em M3 
        /// </summary>
        [XmlElement(ElementName = "capM3")]
        public int? CapM3 { get; set; }

        /// <summary>
        /// 2 - Proprietários do Veículo. Só preenchido quando o veículo não pertencer à empresa emitente do MDF-e
        /// </summary>
        [XmlElement(ElementName = "prop")]
        public MDFeProp Prop { get; set; }

        [XmlElement(ElementName = "tpCar")]
        public MDFeTpCar TpCar { get; set; }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get
            {
                return UF.GetSiglaUfString();
            }
            set { UF = UF.SiglaParaEstado(value); }
        }
    }
}