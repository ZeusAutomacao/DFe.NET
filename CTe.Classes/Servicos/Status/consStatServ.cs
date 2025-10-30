using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTe.Classes.Servicos.Status
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class consStatServCte
    {
        public consStatServCte()
        {
            xServ = "STATUS";
        }

        /// <summary>
        ///     FP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     FP03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     FP04 - Serviço solicitado 'STATUS'
        /// </summary>
        public string xServ { get; set; }
    }

    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class consStatServCTe
    {
        public consStatServCTe()
        {
            xServ = "STATUS";
        }

        /// <summary>
        ///     FP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     FP03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        public Estado cUF { get; set; }

        /// <summary>
        ///     FP04 - Serviço solicitado 'STATUS'
        /// </summary>
        public string xServ { get; set; }
    }
}