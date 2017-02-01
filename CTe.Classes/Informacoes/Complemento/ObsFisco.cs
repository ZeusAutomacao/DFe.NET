using System.Collections.Generic;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class ObsFisco
    {
        private string _xCampo;
        private string _xTexto;
        [XmlAttribute]
        public string xCampo { get { return _xCampo; } set { _xCampo = value; } }
        public string xTexto { get { return _xTexto; } set { _xTexto = value; } }
    }
}
