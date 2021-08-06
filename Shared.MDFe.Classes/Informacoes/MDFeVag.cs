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
using DFe.Classes;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeVag
    {
        private decimal _pesoBc;
        private decimal _pesoR;
        private decimal _tu;

        public decimal pesoBC
        {
            get { return _pesoBc.Arredondar(3); }
            set { _pesoBc = value.Arredondar(3); }
        }

        public decimal pesoR
        {
            get { return _pesoR.Arredondar(3); }
            set { _pesoR = value.Arredondar(3); }
        }

        public string tpVag { get; set; }

        /// <summary>
        /// 2 - Serie de Identificação do vagão
        /// </summary>
        [XmlElement(ElementName = "serie")]
        public short Serie { get; set; }

        /// <summary>
        /// 2 - Número de Identificação do vagão 
        /// </summary>
        [XmlElement(ElementName = "nVag")]
        public long NVag { get; set; }

        /// <summary>
        /// 2 - Sequencia do vagão na composição
        /// </summary>
        [XmlElement(ElementName = "nSeq")]
        public short? NSeq { get; set; }

        public bool NSeqSpecified { get { return NSeq.HasValue; } }

        /// <summary>
        /// 2 - Tonelada Útil 
        /// </summary>
        [XmlElement(ElementName = "TU")]
        public decimal TU
        {
            get { return _tu.Arredondar(3); }
            set { _tu = value.Arredondar(3); }
        }
    }
}