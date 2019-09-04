using System.Xml.Serialization;
using CTe.Classes.Protocolo;
using CTe.Classes.Servicos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Servicos.Autorizacao
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retCTeOS : RetornoBase
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
        ///     AR06a - Código da UF que atendeu a solicitação.
        /// </summary>
        public Estado cUF { get; set; }

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
        ///     AR07 - Dados do Recibo do Lote (Só é gerado se o Lote for aceito)
        /// </summary>
        public protCTe protCTe { get; set; }

        public static retCTeOS LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retCTeOS>(xml);
            retorno.RetornoXmlString = xml;
            return retorno;
        }

        public static retCTeOS LoadXml(string xml, CTeOSClasses.CTeOS cteOS)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(cteOS);
            return retorno;
        }

        public void LoadXml(CTeOSClasses.CTeOS cteOS)
        {
            EnvioXmlString = FuncoesXml.ClasseParaXmlString(cteOS);
        }
    }
}