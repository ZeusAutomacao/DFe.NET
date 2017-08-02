using System.Xml.Serialization;

namespace DFe.MDFe.Classes.Informacoes.Modal.Rodoviario
{
    public class lacRodo
    {
        /// <summary>
        /// 2 - número do lacre
        /// </summary>
        [XmlElement(ElementName = "nLacre")]
        public string nLacre { get; set; }
    }
}