/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
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

using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infCargas;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal
{
    public class infCarga
    {
        private decimal? _vCarga;
        private decimal? _vCargaAverb;

        [XmlElement(Order = 1)]
        public decimal? vCarga
        {
            get { return _vCarga.Arredondar(2); }
            set { _vCarga = value.Arredondar(2); }
        }

        [XmlElement(Order = 2)]
        public string proPred { get; set; }

        [XmlElement(Order = 3)]
        public string xOutCat { get; set; }

        [XmlElement(ElementName = "infQ", Order = 4)]
        public List<infQ> infQ { get; set; }

        /// <summary>
        /// Versão 3.0 - Opcional
        /// </summary>
        [XmlElement(Order = 5)]
        public decimal? vCargaAverb
        {
            get { return _vCargaAverb.Arredondar(2); }
            set { _vCargaAverb = value.Arredondar(2); }
        }

        [XmlElement(Order = 6)]
        public infDoc infDoc { get; set; }

        [XmlElement(Order = 7)]
        public docAnt docAnt { get; set; }

        public bool vCargaSpecified { get { return vCarga.HasValue; } }
        public bool vCargaAverbSpecified { get { return vCargaAverb.HasValue; } }
    }
}
