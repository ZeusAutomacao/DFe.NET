using System.Xml.Serialization;
using DFe.Classes.Assinatura;

namespace CTeDLL.Classes.Servicos.Evento
{
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class evento
    {
        /// <summary>
        ///     HP05 - Versão do leiaute do evento
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     HP06 - Grupo de informações do registro do Evento
        /// </summary>
        public infEventoEnv infEvento { get; set; }

        /// <summary>
        ///     HP22 - Assinatura Digital do documento XML, a assinatura deverá ser aplicada no elemento infEvento
        /// </summary>
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }
}