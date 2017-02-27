using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Flags;

namespace CTeDLL.Classes.Servicos.Status
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
}