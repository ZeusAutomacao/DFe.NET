using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using CTeDLL.Classes.Protocolo;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTeDLL.Classes.Servicos.Recepcao.Retorno
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retConsReciCTe : RetornoBase
    {
        /// <summary>
        ///     BR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Versão do Aplicativo que recebeu a Consulta. A versão deve ser iniciada com a sigla da UF nos casos de WS próprio
        ///     ou a sigla SCAN, SVAN ou SVRS nos demais casos.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     BR04a - Número do Recibo consultado. Será preenchido com zeros se for impossível de obter o valor da mensagem de
        ///     entrada (Ex. mensagem inválida).
        /// </summary>
        public string nRec { get; set; }

        /// <summary>
        ///     BR05 - Se cStatus = 215, 516, 517 ou 545 significa que a mensagem de consulta é inválida.
        ///     Se cStatus = 225, 565. 567 ou 568, significa que o lote de NF-e consultado é inválido
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     BR06 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     BR06a - Código da UF que atendeu a solicitação.
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     BR06b - Código da Mensagem (v2.0) Campo de uso da SEFAZ para enviar mensagem de interesse da SEFAZ para o emissor.
        ///     (NT 2011/004)
        /// </summary>
        public int? cMsg { get; set; }

        /// <summary>
        ///     BR06c - Mensagem da SEFAZ para o emissor. (v2.0)
        /// </summary>
        public string xMsg { get; set; }

        /// <summary>
        ///     BR07 - Conjunto de resultado do processamento de cada NF-e (vide leiaute abaixo).
        ///     Estas informações são retornadas apenas para o código do status do lote = 104 (Lote processado)
        /// </summary>
        [XmlElement("protCTe")]
        public List<protCTe> protCTe { get; set; }

        public bool ShouldSerializecMsg()
        {
            return cMsg.HasValue;
        }

        public static retConsReciCTe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retConsReciCTe>(xml);
            retorno.RetornoXmlString = xml;
            return retorno;
        }

        public static retConsReciCTe LoadXml(string xml, consReciCTe consSitCTe)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(consSitCTe);
            return retorno;
        }
    }
}