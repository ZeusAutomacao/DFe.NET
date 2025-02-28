using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CTe.Classes.Simplificado.Carga
{
    /// <summary>
    /// Componente do valor da prestação.
    /// </summary>
    public class comp
    {
        /// <summary>
        /// Nome do componente (ex: "Frete Valor", "Vr Icms").
        /// </summary>
        [XmlElement(ElementName = "xNome")]
        public string xNome { get; set; }

        /// <summary>
        /// Valor do componente.
        /// </summary>
        [XmlElement(ElementName = "vComp")]
        public decimal vComp { get; set; }
    }
}
