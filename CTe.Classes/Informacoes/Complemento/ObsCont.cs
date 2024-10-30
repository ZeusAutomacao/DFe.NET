using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Complemento
{
    public class ObsCont
    {
        [XmlAttribute]
        public string xCampo { get; set; }

        public string xTexto { get; set; }
    }
}