using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Observacoes
{
    public class obsFisco
    {
        /// <summary>
        ///     Z08 - Identificação do campo
        /// </summary>
        [XmlAttribute]
        public string xCampo { get; set; }

        /// <summary>
        ///     Z09 - Conteúdo do campo
        /// </summary>
        public string xTexto { get; set; }
    }
}