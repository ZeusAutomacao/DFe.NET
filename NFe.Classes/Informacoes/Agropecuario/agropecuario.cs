namespace NFe.Classes.Informacoes.Agropecuario
{
    public class agropecuario
    {
#if NET5_0_OR_GREATER//o uso de tipos de referência anuláveis não é permitido até o C# 8.0.

        #nullable enable

        /// <summary>
        /// ZF02 - serieGuia
        /// </summary>
        public defensivo? defensivo { get; set; }

        /// <summary>
        /// ZF04 - Guia de Trânsito
        /// </summary>
        public guiaTransito? guiaTransito { get; set; }
        
        #nullable disable

        public bool ShouldSerializedefensivo()
        {
            return defensivo != null;
        }
        public bool ShouldSerializeguiaTransito()
        {
            return guiaTransito != null;
        }
#else
        /// <summary>
        /// ZF02 - serieGuia
        /// </summary>
        public defensivo defensivo { get; set; }

        /// <summary>
        /// ZF04 - Guia de Trânsito
        /// </summary>
        public guiaTransito guiaTransito { get; set; }
#endif
    }
}
