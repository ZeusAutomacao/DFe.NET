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
using DFe.Classes.Flags;
using DFe.Utils;

namespace NFe.Classes.Protocolo
{
    public class infProt
    {
        /// <summary>
        ///     PR04 - Identificador da TAG a ser assinada, somente precisa ser informado se a UF assinar a resposta.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     PR05 - Identificação do Ambiente
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     PR06 - Versão do Aplicativo que processou a consulta.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     PR07 - Chave de Acesso da NF-e
        /// </summary>
        public string chNFe { get; set; }

        /// <summary>
        ///     PR08 - Data e hora de recebimento
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxyDhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     PR09 - Número do Protocolo da NF-e
        /// </summary>
        public string nProt { get; set; }

        /// <summary>
        ///     PR10 - Digest Value da NF-e processada Utilizado para conferir a integridade da NFe original.
        /// </summary>
        public string digVal { get; set; }

        /// <summary>
        ///     PR11 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     PR12 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        [XmlElement(ElementName = "cMsg")]
        public string ProxyccMsg
        {
            get
            {
                if (cMsg == null) return null;
                return cMsg.Value.ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    cMsg = null;
                    return;
                }
                cMsg = int.Parse(value);
            }
        }

        [XmlIgnore]
        public int? cMsg { get; set; }

        public string xMsg { get; set; }

        /// <summary>
        ///     PR13 - Assinatura XML do grupo identificado pelo atributo “Id”
        ///     A decisão de assinar a mensagem fica a critério da UF interessada.
        /// </summary>
        public Signature Signature { get; set; }
    }
}