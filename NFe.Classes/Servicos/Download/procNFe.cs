using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Download
{
    public class procNFe
    {
        /// <summary>
        ///     JR15 - Identificação do Schema XML Exemplo: procNFe_v1.10.xsd. 
        /// </summary>
        [XmlAttribute]
        public string schema { get; set; }

        /// <summary>
        /// JR16 - Estrutura genérica do procNFe, informada com um XML conforme consta no atributo schema acima
        /// </summary>
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public nfeProc nfeProc { get; set; }
    }
}