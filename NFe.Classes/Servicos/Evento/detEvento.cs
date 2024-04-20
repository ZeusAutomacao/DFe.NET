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
using DFe.Classes.Entidades;
using DFe.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using Shared.NFe.Classes.Servicos.Evento;

namespace NFe.Classes.Servicos.Evento
{
    public class detEvento
    {
        /// <summary>
        ///     HP18 - Versão do Pedido de Cancelamento, da carta de correção ou EPEC, deve ser informado com a mesma informação da
        ///     tag verEvento (HP16)
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     HP19 - "Cancelamento", "Carta de Correção", "Carta de Correcao" ou "EPEC"
        /// </summary>
        public string descEvento { get; set; }

        #region Carta de Correção

        /// <summary>
        ///     HP20 - Correção a ser considerada, texto livre. A correção mais recente substitui as anteriores.
        /// </summary>
        public string xCorrecao { get; set; }

        /// <summary>
        ///     HP20a - Condições de uso da Carta de Correção
        /// </summary>
        public string xCondUso { get; set; }

        #endregion

        #region EPEC

        /// <summary>
        ///     P20 - Código do Órgão do Autor do Evento.
        ///     Nota: Informar o código da UF do Emitente para este evento.
        /// </summary>
        public Estado? cOrgaoAutor { get; set; }

        /// <summary>
        ///     P21 - Informar "1=Empresa Emitente" para este evento.
        ///     Nota: 1=Empresa Emitente; 2=Empresa Destinatária;
        ///     3=Empresa; 5=Fisco; 6=RFB; 9=Outros Órgãos.
        /// </summary>
        public TipoAutor? tpAutor { get; set; }

        /// <summary>
        ///     P22 - Versão do aplicativo do Autor do Evento.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     P23 - Data e hora
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhEmi { get; set; }

        /// <summary>
        /// Proxy para dhEmi no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhEmi")]
        public string ProxyDhEmi
        {
            get { return dhEmi.ParaDataHoraStringUtc(); }
            set { dhEmi = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     P24 - 0=Entrada; 1=Saída;
        /// </summary>
        public TipoNFe? tpNF { get; set; }

        /// <summary>
        ///     P25 - IE do Emitente
        /// </summary>
        public string IE { get; set; }

        /// <summary>
        ///     P26
        /// </summary>
        public dest dest { get; set; }

        public bool ShouldSerializecOrgaoAutor()
        {
            return cOrgaoAutor.HasValue;
        }

        public bool ShouldSerializetpAutor()
        {
            return tpAutor.HasValue;
        }

        public bool ShouldSerializetpNF()
        {
            return tpNF.HasValue;
        }

        #endregion

        #region Cancelamento

        /// <summary>
        ///     HP20 - Informar o número do Protocolo de Autorização da NF-e a ser Cancelada.
        /// </summary>
        public string nProt { get; set; }

        /// <summary>
        ///     HP21 - Informar a justificativa do cancelamento
        /// </summary>
        public string xJust { get; set; }

        #endregion

        #region Cancelamento por substituição

        /// <summary>
        /// P31 - Chave de acesso da NF-e substituta da NF-e a ser cancelada
        /// </summary>
        public string chNFeRef { get; set; }

        #endregion

        #region Averbação para Exportação
        [XmlElement("itensAverbados")]
        public List<itensAverbados> ItensAverbados { get; set; }

        public bool ShouldSerializeItensAverbados()
        {
            return ItensAverbados != null;
        }
        #endregion

        #region Cancelamento Insucesso NFe
        
        /// <summary>
        ///     P22 - Informar o número do Protocolo de Autorização do 
        ///           Evento da NF-e a que se refere este cancelamento. 
        /// </summary>
        public string nProtEvento { get; set; }

        #endregion
        
        #region Insucesso NFe
        [XmlIgnore]
        public DateTimeOffset? dhTentativaEntrega { get; set; }

        /// <summary>
        /// Proxy para dhTentativaEntrega no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhTentativaEntrega")]
        public string ProxyDhTentativaEntrega
        {
            get { return dhTentativaEntrega.ParaDataHoraStringUtc(); }
            set { dhTentativaEntrega = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// P31 - Número da tentativa de entrega que não teve sucesso 
        /// </summary>
        public int? nTentativa { get; set; }

        /// <summary>
        /// P32 - Motivo do insucesso
        /// </summary>
        public MotivoInsucesso? tpMotivo { get; set; }

        /// <summary>
        /// P33 - Justificativa do motivo do insucesso. Informar apenas para tpMotivo = <see cref="MotivoInsucesso.Outros"/>
        /// </summary>
        public string xJustMotivo { get; set; }

        /// <summary>
        /// P33 - Latitude do ponto de entrega 
        /// </summary>
        public decimal? latGPS { get; set; }

        /// <summary>
        /// P34 - Longitude do ponto de entrega
        /// </summary>
        public decimal? longGPS { get; set; }

        /// <summary>
        /// P35 - Hash SHA-1, no formato Base64, resultante da concatenação de: Chave de Acesso da NF-e + Base64
        /// da imagem capturada na tentativa da entrega(ex: imagem capturada da assinatura eletrônica, digital do recebedor, foto, etc).
        /// </summary>
        public string hashTentativaEntrega { get; set; }

        /// <summary>
        /// Data e hora da geração do hash da tentativa de entrega. Formato AAAA-MMDDThh:mm:ssTZD.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhHashTentativaEntrega { get; set; }

        /// <summary>
        /// Proxy para dhHashTentativaEntrega no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhHashTentativaEntrega")]
        public string ProxyDhHashTentativaEntrega
        {
            get { return dhHashTentativaEntrega.ParaDataHoraStringUtc(); }
            set { dhHashTentativaEntrega = DateTimeOffset.Parse(value); }
        }

        public bool ShouldSerializenTentativa()
        {
            return nTentativa.HasValue;
        }

        public bool ShouldSerializetpMotivo()
        {
            return tpMotivo.HasValue;
        }

        public bool ShouldSerializelatGPS()
        {
            return latGPS.HasValue;
        }

        public bool ShouldSerializelongGPS()
        {
            return longGPS.HasValue;
        }

        #endregion

    }
}