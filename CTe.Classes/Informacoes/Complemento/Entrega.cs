using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Complemento.Tipos;

namespace CTeDLL.Classes.Informacoes.Complemento
{
    public class Entrega
    {
        [XmlElement("semData", typeof(semData))]
        [XmlElement("comData", typeof(comData))]
        [XmlElement("noPeriodo", typeof(noPeriodo))]
        [XmlElement("semHora", typeof(semHora))]
        [XmlElement("comHora", typeof(comHora))]
        [XmlElement("noInter", typeof(noInter))]
        public EntregaTipos TipoEntrega { get; set; }
    }
}
