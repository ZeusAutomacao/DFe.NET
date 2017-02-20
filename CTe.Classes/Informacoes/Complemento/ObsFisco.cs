using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class ObsFisco
    {
        [XmlAttribute]
        public string xCampo { get; set; }

        public string xTexto { get; set; }
    }
}
