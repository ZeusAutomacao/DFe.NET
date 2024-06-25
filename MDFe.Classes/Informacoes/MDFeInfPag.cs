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

using DFe.Classes;
using MDFe.Classes.Flags;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeInfPag
    {
        public MDFeInfPag()
        {
            InfPrazo = new List<MDFeInfPrazo>();
        }

        /// <summary>
        /// 3 - Nome do responsável pelo pagamento.
        /// </summary>
        [XmlElement(ElementName = "xNome")]
        public string XNome { get; set; }

        /// <summary>
        /// 3 - Número do CPF do responsável pelo pagamento.
        /// </summary>
        [XmlElement(ElementName = "CPF")]
        public string CPF { get; set; }

        /// <summary>
        /// 3 - Número do CNPJ do responsável pelo pagamento.
        /// </summary>
        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }

        /// <summary>
        /// 3 - Identificador do responsável pelo pagamento 
        /// em caso de ser estrangeiro.
        /// </summary>
        [XmlElement(ElementName = "idEstrangeiro")]
        public string IdEstrangeiro { get; set; }

        /// <summary>
        /// 3 - Componentes do pagamento do frete.
        /// </summary>
        [XmlElement(ElementName = "Comp")]
        public List<MDFeComp> Comp { get; set; }

        [XmlIgnore]
        private decimal _vContrato { get; set; }

        /// <summary>
        /// 3 - Valor total do Contrato.
        /// </summary>
        [XmlElement("vContrato")]
        public decimal VContratoProxy
        {
            get { return _vContrato.Arredondar(2); }
            set { _vContrato = value.Arredondar(2); }
        }

        /// <summary>
        /// 3 - Indicador da forma de pagamento.
        /// </summary>
        [XmlElement(ElementName = "indPag")]
        public MDFeIndPag IndPag { get; set; }

        /// <summary>
        /// 3 - Informações do pagamento a prazo. Informar somente se indPag for à Prazo.
        /// </summary>
        [XmlElement(ElementName = "infPrazo")]
        public List<MDFeInfPrazo> InfPrazo { get; set; }

        /// <summary>
        /// 3 - Informações Bancárias.
        /// </summary>
        [XmlElement(ElementName = "infBanc")]
        public MDFeInfBanc InfBanc { get; set; }

        /// <summary>
        /// 3 - Indicador de operação de transporte de 
        /// alto desempenho
        /// </summary>
        [XmlElement(ElementName = "indAltoDesemp")]
        public MDFeIndAltoDesemp IndAltoDesemp { get; set; }

        public bool ShouldSerializeIndAltoDesemp()
        {
            return IndAltoDesemp == MDFeIndAltoDesemp.AltoDesempenho;
        }
    }
}