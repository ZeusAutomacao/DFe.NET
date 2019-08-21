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
using DFe.Classes.Assinatura;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Servicos.Evento
{
    public class infEventoRet
    {
        /// <summary>
        ///     HR12 - Identificador da TAG a ser assinada
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        ///     HR13 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     HR14 - Versão da aplicação que registrou o Evento
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     HR15 - Código da UF que registrou o Evento. Utilizar 91 para o Ambiente Nacional.
        /// </summary>
        public Estado cOrgao { get; set; }

        /// <summary>
        ///     HR16 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     HR17 - Descrição do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     HR18 - Chave de Acesso da NF-e vinculada ao evento.
        /// </summary>
        public string chCTe { get; set; }
        /// <summary>
        ///     HR19 - Código do Tipo do Evento.
        /// </summary>
        public int? tpEvento { get; set; }

        /// <summary>
        ///     HR20 - Descrição do Evento – “Cancelamento homologado”
        /// </summary>
        public string xEvento { get; set; }

        /// <summary>
        ///     HR21 - Sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        public int? nSeqEvento { get; set; }

        /// <summary>
        ///     HR25 - Data e hora de registro do evento no formato AAAA-MM-DDTHH:MM:SSTZD (formato UTC, onde TZD é +HH:MM ou
        ///     –HH:MM), se o evento for rejeitado informar a data e hora de recebimento do evento.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhRegEvento { get; set; }

        [XmlElement(ElementName = "dhRegEvento")]
        public string ProxydhRegEvento
        {
            get { return dhRegEvento.ParaDataHoraStringUtc(); }
            set { dhRegEvento = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     HR26 - Número do Protocolo da NF-e
        /// </summary>
        public string nProt { get; set; }

        /// <summary>
        ///     HR27 - Assinatura Digital do documento XML, a assinatura deverá ser aplicada no elemento infEvento. A decisão de
        ///     assinar a mensagem fica a critério da UF.
        /// </summary>
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        public bool ShouldSerializetpEvento()
        {
            return tpEvento.HasValue;
        }

        public bool ShouldSerializenSeqEvento()
        {
            return nSeqEvento.HasValue;
        }
    }
}