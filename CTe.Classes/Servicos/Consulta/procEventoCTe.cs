using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Evento;

namespace CTeDLL.Classes.Servicos.Consulta
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class procEventoCTe
    {
        /// <summary>
        ///     ZR02
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     ZR03
        /// </summary>
        public eventoCTe eventoCTe { get; set; }

        /// <summary>
        ///     YR05
        /// </summary>
        /// 
        [XmlElement("retEvento")]
        public List<retEventoCTe> retEvento { get; set; }
    }
}