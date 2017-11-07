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

using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviario;
using CTe.Classes.Informacoes.infCTeNormal.infModals.rodoviarioOS;
using CTe.Classes.Informacoes.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class rodo : ContainerModal
    {
        public string RNTRC { get; set; }

        [XmlIgnore]
        public DateTime? dPrev { get; set; }

        [XmlElement(ElementName = "dPrev")]
        public string ProxydPrev {
            get
            {

                if (dPrev == null) return null;

                return dPrev.Value.ParaDataString();
            }
            set
            {
                dPrev = Convert.ToDateTime(value);
            }
        }

        public lota? lota { get; set; }
        public bool lotaSpecified { get { return lota.HasValue; } }

        public string CIOT { get; set; }

        [XmlElement("occ")]
        public List<occ> occ;

        [XmlElement(ElementName = "valePed")]
        public List<valePed> valePed { get; set; }

        [XmlElement(ElementName = "veic")]
        public List<veic> veic { get; set; }

        [XmlElement(ElementName = "lacRodo")]
        public List<lacRodo> lacRodo { get; set; }

        [XmlElement(ElementName = "moto")]
        public List<moto> moto { get; set; }

        public bool ShouldSerializeveic()
        {
            return veic != null;
        }

        public bool ShouldSerializemoto()
        {
            return moto != null;
        }
    }
}