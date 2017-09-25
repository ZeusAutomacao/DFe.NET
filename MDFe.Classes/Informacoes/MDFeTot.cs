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
using MDFe.Classes.Flags;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class MDFeTot
    {
        private decimal _vCarga;
        private decimal _qCarga;

        /// <summary>
        /// 2 - Quantidade total de CT-e relacionados no Manifesto
        /// </summary>
        [XmlElement(ElementName = "qCTe")]
        public int? QCTe { get; set; }

        /// <summary>
        /// 2 - Quantidade total de NF-e relacionadas no Manifesto
        /// </summary>
        [XmlElement(ElementName = "qNFe")]
        public int? QNFe { get; set; }

        /// <summary>
        /// 2 - Quantidade total de MDF-e relacionados no Manifesto Aquaviário
        /// </summary>
        [XmlElement(ElementName = "qMDFe")]
        public int? QMDFe { get; set; }

        /// <summary>
        /// 2 - Valor total da carga / mercadorias transportadas
        /// </summary>
        [XmlElement(ElementName = "vCarga")]
        public decimal vCarga
        {
            get { return _vCarga; }
            set { _vCarga = value.Arredondar(2); }
        }

        /// <summary>
        /// 2 - Codigo da unidade de medida do Peso Bruto da Carga / Mercadorias transportadas
        /// </summary>
        [XmlElement(ElementName = "cUnid")]
        public MDFeCUnid CUnid { get; set; }

        /// <summary>
        /// 2 - Peso Bruto Total da Carga / Mercadorias transportadas
        /// </summary>
        [XmlElement(ElementName = "qCarga")]
        public decimal QCarga
        {
            get { return _qCarga; }
            set { _qCarga = value.Arredondar(4); }
        }


        /// <summary>
        /// Se null não aparece no xml
        /// </summary>
        public bool QCTeSpecified { get { return QCTe.HasValue; } }

        /// <summary>
        /// Se null não aparece no xml
        /// </summary>
        public bool QNFeSpecified { get { return QNFe.HasValue; } }

        /// <summary>
        /// Se null não aparece no xml
        /// </summary>
        public bool QMDFeSpecified { get { return QMDFe.HasValue; } }
    }
}