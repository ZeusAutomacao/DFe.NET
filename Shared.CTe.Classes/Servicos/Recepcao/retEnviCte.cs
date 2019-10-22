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
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Servicos.Recepcao
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retEnviCte : RetornoBase
    {
        /// <summary>
        ///     AR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     AR03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     AR04 - Versão do Aplicativo que recebeu o Lote. A versão deve ser iniciada com a sigla da UF nos casos de WS
        ///     próprio ou a sigla SCAN, SVAN ou SVRS nos demais casos.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     AR05 - Código do status da resposta (vide item 5.1.1)
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     AR06 - Descrição literal do status da resposta
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     AR06a - Código da UF que atendeu a solicitação.
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     AR06b - Data e Hora do Recebimento Formato = AAAA-MM-DDTHH:MM:SS Preenchido com data e hora do recebimento do lote.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxydhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     AR07 - Dados do Recibo do Lote (Só é gerado se o Lote for aceito)
        /// </summary>
        public infRec infRec { get; set; }


        public static retEnviCte LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retEnviCte>(xml);
            retorno.RetornoXmlString = xml;
            return retorno;
        }

        public static retEnviCte LoadXml(string xml, enviCTe enviCte)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(enviCte);
            return retorno;
        }
    }
}