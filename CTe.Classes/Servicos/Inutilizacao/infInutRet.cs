using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTeDLL.Classes.Servicos.Inutilizacao
{
    public class infInutRet
    {
        /// <summary>
        ///     DR04 - Identificador da TAG a ser assinada, somente precisa ser informado se a UF assinar a resposta.
        ///     Em caso de assinatura da resposta pela SEFAZ preencher o campo com o Nro do Protocolo, precedido com o literal
        ///     “ID”.
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        ///     DR05 - Identificação do Ambiente: 1 – Produção / 2 – Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Versão do Aplicativo que processou o pedido de inutilização.
        ///     A versão deve ser iniciada com a sigla da UF nos casos de WS próprio ou a sigla SCAN, SVAN ou SVRS nos demais
        ///     casos.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     DR07 - Código do status da resposta (vide item 5.1.1).
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     DR08 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     DR09 - Código da UF que atendeu a solicitação
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     DR10 - Ano de inutilização da numeração
        /// </summary>
        public int? ano { get; set; }

        /// <summary>
        ///     DR11 - CNPJ do emitente
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        ///     DR12 - Modelo da NF-e
        /// </summary>
        public ModeloDocumento? mod { get; set; }

        /// <summary>
        ///     DR13 - Série da NF-e
        /// </summary>
        public int? serie { get; set; }

        /// <summary>
        ///     DR14 - Número da NF-e inicial a ser inutilizada
        /// </summary>
        public int? nNFIni { get; set; }

        /// <summary>
        ///     DR15 - Número da NF-e final a ser inutilizada
        /// </summary>
        public int? nNFFin { get; set; }

        /// <summary>
        ///     DR16 - Data e hora de processamento
        ///     Formato = AAAA-MM-DDTHH:MM:SS Preenchido com data e hora da gravação no Banco de Dados em caso de Confirmação.
        ///     Em caso de Rejeição, com data e hora do recebimento do Pedido.
        /// </summary>
        public DateTime? dhRecbto { get; set; }

        /// <summary>
        ///     DR17 - Número do Protocolo de Inutilização (vide item 5.6).
        /// </summary>
        public string nProt { get; set; }

        public bool ShouldSerializeano()
        {
            return ano.HasValue;
        }

        public bool ShouldSerializemod()
        {
            return mod.HasValue;
        }

        public bool ShouldSerializeserie()
        {
            return serie.HasValue;
        }

        public bool ShouldSerializenNFIni()
        {
            return nNFIni.HasValue;
        }

        public bool ShouldSerializenNFFin()
        {
            return nNFFin.HasValue;
        }

        public bool ShouldSerializedhRecbto()
        {
            return dhRecbto.HasValue;
        }
    }
}