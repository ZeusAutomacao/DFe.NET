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
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class aquav : ContainerModal
    {
        private decimal _vPrest;
        private decimal _vAfrmm;

        public decimal vPrest
        {
            get { return _vPrest.Arredondar(2); }
            set { _vPrest = value.Arredondar(2); }
        }

        public decimal vAFRMM
        {
            get { return _vAfrmm.Arredondar(2); }
            set { _vAfrmm = value.Arredondar(2); }
        }

        public string nBooking { get; set; }
        public string nCtrl { get; set; }
        public string xNavio { get; set; }

        [XmlElement(ElementName = "balsa")]
        public List<balsa> balsa { get; set; }

        public string nViag { get; set; }
        public string direc { get; set; }
        public string prtEmb { get; set; }
        public string prtTrans { get; set; }
        public string prtDest { get; set; }
        public tpNav? tpNav { get; set; }
        public bool tpNavSpecified { get { return tpNav.HasValue; } }
        public string irin { get; set; }

        [XmlElement(ElementName = "detCont")]
        public List<detCont> detcont { get; set; }
    }
}