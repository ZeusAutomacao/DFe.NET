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
using CTe.Classes.Protocolo;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Servicos.Consulta
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retConsSitCTe : RetornoBase
    {
        /// <summary>
        ///     ER02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     ER03 - Identificação do Ambiente: 1 – Produção / 2 – Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Versão do Aplicativo que processou a consulta. A versão deve ser iniciada com a sigla da UF nos casos de WS próprio
        ///     ou a sigla SCAN, SVAN ou SVRS nos demais casos
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     ER05 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     ER06 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     ER07 - Código da UF que atendeu a solicitação
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     ER08 - Protocolo de autorização ou denegação de uso da CT-e (vide item 4.2.2).
        ///     Informar se localizado uma CT-e com cStat = 100 (uso autorizado) ou 110 (uso denegado).
        /// </summary>
        public protCTe protCTe { get; set; }

        /// <summary>
        ///     ER10 - Informação do evento e respectivo Protocolo de registro de Evento
        /// </summary>
        [XmlElement("procEventoCTe")]
        public List<procEventoCTe> procEventoCTe { get; set; }


        public static retConsSitCTe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retConsSitCTe>(xml);
            retorno.RetornoXmlString = xml;
            return retorno;
        }

        public static retConsSitCTe LoadXml(string xml, consSitCTe consSitCTe)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(consSitCTe);
            return retorno;
        }
    }
}