using CTe.Classes.Simplificado.Informacoes;
using System.Xml.Serialization;

namespace CTe.Classes.Simplificado
{
    /// <summary>
    /// Tipo Conhecimento de Transporte Eletrônico (Modelo 57) - Modelo Simplificado
    /// </summary>
    [XmlRoot("CTeSimp", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class cteSimp
    {
        /// <summary>
        /// Informações do CT-e
        /// </summary>
        [XmlElement("infCte")]
        public infCte infCte { get; set; }

        /// <summary>
        /// Versão do leiaute
        /// </summary>
        [XmlAttribute("versao")]
        public string versao { get; set; }

        /// <summary>
        /// Identificador da tag a ser assinada
        /// </summary>
        [XmlAttribute("Id")]
        public string id { get; set; }
    }
}
