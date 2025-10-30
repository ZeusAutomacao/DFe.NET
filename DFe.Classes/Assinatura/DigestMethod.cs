using System.Xml.Serialization;

namespace DFe.Classes.Assinatura
{
    public class DigestMethod
    {
        /// <summary>
        ///     XS16 - Atributo Algorithm de DigestMethod: http://www.w3.org/2000/09/xmldsig#sha1
        /// </summary>
        [XmlAttribute]
        public string Algorithm { get; set; }
    }
}