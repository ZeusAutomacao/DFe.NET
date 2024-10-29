using System.Xml.Serialization;
using NFe.Classes.Servicos.Evento;

namespace NFe.Classes.Servicos.Consulta
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class procEventoNFe
    {
        /// <summary>
        ///     ZR02
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     ZR03
        /// </summary>
        public evento evento { get; set; }

        /// <summary>
        ///     YR05
        /// </summary>
        public retEvento retEvento { get; set; }
    }
}