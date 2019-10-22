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
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Servicos.Evento
{
    public class infEventoEnv
    {
        public infEventoEnv(ConfiguracaoServico configuracaoServico = null)
        {
            _configuracaoServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
        }

        public infEventoEnv()
        {
        }

        [XmlIgnore]
        private ConfiguracaoServico _configuracaoServico;

        /// <summary>
        ///     HP07 - Grupo de informações do registro do Evento
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        ///     HP08 - Código do órgão de recepção do Evento.
        /// </summary>
        public Estado cOrgao { get; set; }

        /// <summary>
        ///     HP09 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     HP10 - CNPJ do autor do Evento
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     HP12 - Chave de Acesso da NF-e vinculada ao Evento
        /// </summary>
        public string chCTe { get; set; }

        /// <summary>
        ///     HP13 - Data e hora do evento no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhEvento { get; set; }

        [XmlElement(ElementName = "dhEvento")]
        public string ProxydhEvento
        {
            get
            {
                if (_configuracaoServico == null)
                    _configuracaoServico = ConfiguracaoServico.Instancia;
                switch (_configuracaoServico.VersaoLayout)
                {
                    case versao.ve200:
                        return dhEvento.ParaDataHoraStringSemUtc();
                    case versao.ve300:
                        return dhEvento.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão Inválida para CT-e");
                }
            }

            set { dhEvento = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     HP14 - Código do evento
        /// </summary>
        public CTeTipoEvento tpEvento { get; set; }

        /// <summary>
        ///     HP15 - Sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        public int nSeqEvento { get; set; }

        /// <summary>
        ///     HP17 - Informações do Pedido de Cancelamento
        /// </summary>
        public detEvento detEvento { get; set; }
    }
}