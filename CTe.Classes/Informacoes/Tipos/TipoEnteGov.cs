using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum TipoEnteGov
    {
        [Description("União")]
        [XmlEnum("1")]
        Uniao = 1,

        [Description("Estado")]
        [XmlEnum("2")]
        Estado = 2,

        [Description("Distrito Federal")]
        [XmlEnum("3")]
        DistritoFederal = 3,

        [Description("Município")]
        [XmlEnum("4")]
        Municipio = 4
    }
}
