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
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoBensServicos;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoCombustivel;
using NFe.Classes.Servicos.Evento.Informacoes.CreditoPresumido;
using NFe.Classes.Servicos.Evento.Informacoes.Imobilizacao;
using NFe.Classes.Servicos.Evento.Informacoes.ItemConsumo;
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

        #region Cancelamento Insucesso/Comprovante de Entrega NFe/ Cancelamento Evento
        
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

        #region Comprovante Entrega NFe

        /// <summary>
        /// P30 - Data e hora do final da entrega
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhEntrega { get; set; }

        /// <summary>
        /// Proxy para dhEntrega no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhEntrega")]
        public string ProxyDhEntrega
        {
            get { return dhEntrega.ParaDataHoraStringUtc(); }
            set { dhEntrega = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// P31 - Número do documento de identificação da pessoa que assinou o Comprovante de Entrega da NF-e/>
        /// </summary>
        public string nDoc { get; set; }

        /// <summary>
        /// P32 - Nome da pessoa que assinou o Comprovante de Entrega da NF-e/>
        /// </summary>
        public string xNome { get; set; }

        /// <summary>
        /// P35 - Hash SHA-1, no formato Base64, resultante da concatenação de: Chave de Acesso da NF-e + Base64
        /// da imagem capturada do Comprovante de Entrega da NFe (ex: imagem capturada da assinatura eletrônica, digital do recebedor, foto, etc).
        /// </summary>
        public string hashComprovante { get; set; }

        /// <summary>
        /// P36 - Data e hora da geração do hash da tentativa de entrega. Formato AAAA-MMDDThh:mm:ssTZD.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhHashComprovante { get; set; }

        /// <summary>
        /// Proxy para dhHashComprovante no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlElement(ElementName = "dhHashComprovante")]
        public string ProxyDhHashComprovante
        {
            get { return dhHashComprovante.ParaDataHoraStringUtc(); }
            set { dhHashComprovante = DateTimeOffset.Parse(value); }
        }

        #endregion

        #region Conciliação Financeira

        /// <summary>
        /// P21 - Grupo de detalhamento do pagamento
        /// </summary>
        [XmlElement("detPag")]
        public List<detPagEvento> detPag { get; set; }

        public bool ShouldSerializedetPag()
        {
            return detPag != null;
        }

        #endregion

        #region Ator Interessado NFe
        /// <summary>
        /// P23 - Pessoas autorizadas a acessar o XML da NF-e
        /// </summary>
        [XmlElement("autXML")]
        public List<autXML> autXML { get; set; }

        /// <summary>
        /// P26 - 0 = Não permite;
        /// 1 = Permite o transportador autorizado pelo
        /// emitente ou destinatário autorizar outros
        /// transportadores para ter acesso ao download da
        /// NF-e
        /// </summary>
        public TipoAutorizacao? tpAutorizacao { get; set; }

        public bool ShouldSerializetpAutorizacao()
        {
            return tpAutorizacao != null;
        }

        #endregion

        #region Carta de Correção

        /// <summary>
        /// HP20 - Correção a ser considerada, texto livre. A correção mais recente substitui as anteriores.
        /// </summary>
        public string xCorrecao { get; set; }

        /// <summary>
        /// HP20a - Condições de uso da Carta de Correção.
        /// P27 - Condição de uso do tipo de autorização para o transportador.
        /// </summary>
        public string xCondUso { get; set; }

        #endregion

        #region Eventos para a apuração do IBS e da CBS

        #region Informação de efetivo pagamento integral para liberar crédito presumido do adquirente 

        /// <summary>
        ///     P23 - Indicador de efetiva quitação do pagamento integral referente a NFe referenciada
        /// </summary>
        public IndicadorDeQuitacaoDoPagamento? indQuitacao { get; set; }
        
        public bool ShouldSerializeindQuitacao()
        {
            return indQuitacao.HasValue;
        }

        #endregion

        #region Solicitação de Apropriação de crédito presumido

        /// <summary>
        ///     P23 - Informações de crédito presumido por item
        /// </summary>
        public List<gCredPres> gCredPres { get; set; }
        
        public bool ShouldSerializegCredPres()
        {
            return gCredPres != null;
        }

        #endregion

        #region Destinação de item para consumo pessoal

        /// <summary>
        ///     P23 - Informações por item da NF-e de Aquisição. Nota: a quantidade de ocorrências não pode ser maior que a quantidade de itens da NF-e de aquisição
        /// </summary>
        public List<gConsumo> gConsumo { get; set; }
        
        public bool ShouldSerializegConsumo()
        {
            return gConsumo != null;
        }

        #endregion

        #region Aceite de débito na apuração por emissão de nota de crédito | Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão | Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão

        /// <summary>
        ///     Informação utilizada nos eventos "Aceite de débito na apuração por emissão de nota de crédito" e "Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão"
        ///     Para evento "Aceite de débito na apuração por emissão de nota de crédito": P23 - Indicador de concordância com o valor da nota de crédito que lançaram IBS e CBS na apuração assistida
        ///     Para evento "Manifestação sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão": P23 - Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada
        ///     Para evento "Manifestação sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão": P23 - Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada
        /// </summary>
        public IndicadorAceitacao? indAceitacao { get; set; }

        public bool ShouldSerializeindAceitacao()
        {
            return indAceitacao != null;
        }

        #endregion

        #region Imobilização de Item

        /// <summary>
        ///     P23 - Informações de itens integrados ao ativo imobilizado
        /// </summary>
        public List<gImobilizacao> gImobilizacao { get; set; }

        public bool ShouldSerializegImobilizacao()
        {
            return gImobilizacao != null;
        }
        
        #endregion

        #region Solicitação de Apropriação de Crédito de Combustível

        /// <summary>
        ///     P23 - Informações de consumo de combustíveis
        /// </summary>
        public List<gConsumoComb> gConsumoComb { get; set; }
        
        public bool ShouldSerializegConsumoComb()
        {
            return gConsumoComb != null;
        }

        #endregion

        #region Solicitação de Apropriação de Crédito para bens e serviços que dependem de atividade do adquirente

        /// <summary>
        ///     P23 - Informações de crédito
        /// </summary>
        public List<gCredito> gCredito { get; set; }

        public bool ShouldSerializegCredito()
        {
            return gCredito != null;
        }
        
        #endregion

        #region Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão | Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão

        /// <summary>
        ///     Para ambos os eventos "Manifestação do Fisco sobre Pedido de Transferência de Crédito de CBS em Operações de Sucessão"
        ///     e "Manifestação do Fisco sobre Pedido de Transferência de Crédito de IBS em Operações de Sucessão" o campo representa:
        ///     Indicador de aceitação do valor de transferência para a empresa que emitiu a nota referenciada
        /// </summary>
        public IndicadorDeferimento? indDeferimento { get; set; }

        public bool ShouldSerializeindDeferimento()
        {
            return indDeferimento != null;
        }
        
        /// <summary>
        ///     P24 - Motivo deferimento
        /// </summary>
        public MotivoDeferimento? cMotivo { get; set; }
        
        public bool ShouldSerializecMotivo()
        {
            return cMotivo != null;
        }
        
        /// <summary>
        ///     P24 - Descrição deferimento
        /// </summary>
        public string xMotivo { get; set; }
        
        #endregion
        
        #region Cancelamento Evento

        /// <summary>
        ///     P23 - Código do evento autorizado a ser cancelado
        /// </summary>
        public string tpEventoAut {get; set;}
        
        #endregion

        #endregion
    }
}