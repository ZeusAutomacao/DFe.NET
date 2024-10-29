using DFe.Classes.Entidades;
using DFe.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Evento;
using Shared.NFe.Classes.Servicos.Evento;

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
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
    }
}