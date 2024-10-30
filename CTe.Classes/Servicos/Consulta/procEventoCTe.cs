using System.Xml.Serialization;
using CTe.Classes.Servicos.Evento;

namespace CTe.Classes.Servicos.Consulta
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
        [XmlElement("retEventoCTe")]
        public retEventoCTe retEvento { get; set; }
    }
}