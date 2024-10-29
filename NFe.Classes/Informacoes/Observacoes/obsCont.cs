using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Observacoes
{
    public class obsCont
    {
        /// <summary>
        ///     Z05 - Identificação do campo
        /// </summary>
        [XmlAttribute]
        public string xCampo { get; set; }

        /// <summary>
        ///     Z06 - Conteúdo do campo
        /// </summary>
        public string xTexto { get; set; }
    }
}