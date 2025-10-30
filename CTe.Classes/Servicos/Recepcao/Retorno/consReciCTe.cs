using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Flags;

namespace CTe.Classes.Servicos.Recepcao.Retorno
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class consReciCTe
    {
        /// <summary>
        ///     BP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     BP03 - Identificação do Ambiente: 1 – Produção / 2 – Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     BP04 - Número do Recibo Número gerado pelo Portal da Secretaria de Fazenda Estadual (vide item 5.5).
        /// </summary>
        public string nRec { get; set; }
    }
}