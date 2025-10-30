using System.Collections.Generic;
using System.Xml.Serialization;

namespace DFe.Classes.Assinatura
{
    public class Reference
    {
        /// <summary>
        ///     XS08 - Atributo URI da tag Reference
        /// </summary>
        [XmlAttribute]
        public string URI { get; set; }

        /// <summary>
        ///     XS10 - Grupo do algorithm de Transform
        /// </summary>
        public List<Transform> Transforms { get; set; }

        /// <summary>
        ///     XS15 - Grupo do Método de DigestMethod
        /// </summary>
        public DigestMethod DigestMethod { get; set; }

        /// <summary>
        ///     XS17 - Digest Value (Hash SHA-1 – Base64)
        /// </summary>
        public string DigestValue { get; set; }
    }
}