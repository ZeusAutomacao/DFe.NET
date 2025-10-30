using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Agropecuario
{
    public class agropecuario
    {
#if NET5_0_OR_GREATER//o uso de tipos de referência anuláveis não é permitido até o C# 8.0.

        /// <summary>
        /// ZF02 - Defensivos Agrícolas
        /// </summary>
        [XmlElement("defensivo")]
        public List<defensivo>? defensivo { get; set; }

        /// <summary>
        /// ZF04 - Guia de Trânsito
        /// </summary>
        [XmlElement("guiaTransito")]
        public guiaTransito? guiaTransito { get; set; }

        public bool ShouldSerializeguiaTransito()
        {
            return guiaTransito != null;
        }
#else
        /// <summary>
        /// ZF02 - Defensivos Agrícolas
        /// </summary>
        [XmlElement("defensivo")]
        public List<defensivo> defensivo { get; set; }

        /// <summary>
        /// ZF04 - Guia de Trânsito
        /// </summary>
        [XmlElement("guiaTransito")]
        public guiaTransito guiaTransito { get; set; }
#endif
    }
}
