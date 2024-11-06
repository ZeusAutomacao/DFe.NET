namespace NFe.Classes.Informacoes.Agropecuario
{
    public class guiaTransito
    {
#if NET5_0_OR_GREATER//o uso de tipos de referência anuláveis não é permitido até o C# 8.0.
        /// <summary>
        /// ZF05 - Tipo da Guia
        /// </summary>
        public TipoGuia tpGuia { get; set; }

        /// <summary>
        /// ZF06 - UF de emissão
        /// </summary>
        public string? UFGuia { get; set; }

        /// <summary>
        /// ZF07 - Série da Guia
        /// </summary>
        public string? serieGuia { get; set; }

        /// <summary>
        /// ZF08 - Número da Guia
        /// </summary>
        public string nGuia { get; set; }

        public bool ShouldSerializeUFGuia()
        {
            return UFGuia != null;
        }
        public bool ShouldSerializeserieGuia()
        {
            return serieGuia != null;
        }
#else
        /// <summary>
        /// ZF05 - Tipo da Guia
        /// </summary>
        public TipoGuia tpGuia { get; set; }

        /// <summary>
        /// ZF06 - UF de emissão
        /// </summary>
        public string UFGuia { get; set; }

        /// <summary>
        /// ZF07 - Série da Guia
        /// </summary>
        public string serieGuia { get; set; }

        /// <summary>
        /// ZF08 - Número da Guia
        /// </summary>
        public string nGuia { get; set; }
#endif
    }
}