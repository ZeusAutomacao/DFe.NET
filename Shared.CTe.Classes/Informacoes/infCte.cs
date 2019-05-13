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
using CTe.Classes.Informacoes.Complemento;
using CTe.Classes.Informacoes.Destinatario;
using CTe.Classes.Informacoes.Emitente;
using CTe.Classes.Informacoes.Expedidor;
using CTe.Classes.Informacoes.infCTeNormal;
using CTe.Classes.Informacoes.Identificacao;
using CTe.Classes.Informacoes.Impostos;
using CTe.Classes.Informacoes.Recebedor;
using CTe.Classes.Informacoes.Remetente;
using CTe.Classes.Informacoes.Valores;
using CTe.Classes.Servicos.Tipos;

namespace CTe.Classes.Informacoes
{
    public class infCte
    {
        [XmlAttribute]
        public versao versao { get; set; }

        [XmlAttribute]
        public string Id { get; set; }

        public ide ide { get; set; }

        public compl compl { get; set; }

        public emit emit { get; set; }

        public tomaCteOs toma { get; set; }

        public rem rem { get; set; }

        public exped exped { get; set; }

        public receb receb { get; set; }

        public dest dest { get; set; }

        public vPrest vPrest { get; set; }

        public imp imp { get; set; }

        public infCTeNorm infCTeNorm { get; set; }

        public infCteComp.infCteComp infCteComp { get; set; }

        public infCteAnu.infCteAnu infCteAnu { get; set; }

        [XmlElement("autXML")]
        public List<autXML> autXML { get; set; }

        public infRespTec.infRespTec infRespTec { get; set; }

    }
}
