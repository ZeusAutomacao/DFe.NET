using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infCTeNormal.infDocumentos
{
    /// <summary>
    ///     Informações das DCe (Declaração de Conteúdo eletrônica, modelo 99)
    ///     transportadas pelo CT-e.
    ///     <para>
    ///         Grupo criado pela NT 2025.001 (item "Criação do grupo de informações da DCe nos
    ///         documentos originários"), disponível somente no leiaute 4.00.
    ///     </para>
    /// </summary>
    public class infDCe
    {
        /// <summary>
        ///     Chave de acesso da DCe
        /// </summary>
        [XmlElement(Order = 1)]
        public string chave { get; set; }
    }
}
