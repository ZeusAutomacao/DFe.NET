using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace NFe.Classes.Servicos.Status
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class consStatServ
    {
        public consStatServ()
        {
            xServ = "STATUS";
        }

        /// <summary>
        ///     FP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     FP03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     FP04 - Código da UF consultada
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     Serviço solicitado 'STATUS'
        /// </summary>
        public string xServ { get; set; }
    }
}