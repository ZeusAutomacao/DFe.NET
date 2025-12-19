using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe
{
    /// <summary>
    ///     <para>01 - Crédito presumido da aquisição de bens e serviços de produtor rural e produtor rural integrado não contribuinte, observado o art. 168 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>02 - Crédito presumido da aquisição de serviço de transportador autônomo de carga pessoa física não contribuinte, observado o art. 169 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>03 - Crédito presumido da aquisição de resíduos e demais materiais destinados à reciclagem, reutilização ou logística reversa adquiridos de pessoa física, cooperativa ou outra forma de organização popular, observado o art. 170 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>04 - Crédito presumido da aquisição de bens móveis usados de pessoa física não contribuinte para revenda, observado o art. 171 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>05 - Crédito presumido no regime automotivo, observado o art. 311 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>06 - Crédito presumido no regime automotivo, observado o art. 312 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>07 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 444 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>08 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 447 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>09 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 449 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>10 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 450 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>11 - Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 462 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>12 - Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 465 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>13 - Crédito presumido na aquisição pela indústria na Área de Livre Comércio, observado o art. 467 da Lei Complementar nº 214, de 2025.</para>
    /// </summary>
    public enum CodigoClassificacaoCCredPres
    {
        [Description("Crédito presumido da aquisição de bens e serviços de produtor rural e produtor rural integrado não contribuinte, observado o art. 168 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("00")]
        C01,
        [Description("Crédito presumido da aquisição de serviço de transportador autônomo de carga pessoa física não contribuinte, observado o art. 169 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("02")]
        C02,
        [Description("Crédito presumido da aquisição de resíduos e demais materiais destinados à reciclagem, reutilização ou logística reversa adquiridos de pessoa física, cooperativa ou outra forma de organização popular, observado o art. 170 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("03")]
        C03,
        [Description("Crédito presumido da aquisição de bens móveis usados de pessoa física não contribuinte para revenda, observado o art. 171 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("04")]
        C04,
        [Description("Crédito presumido no regime automotivo, observado o art. 311 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("05")]
        C05,
        [Description("Crédito presumido no regime automotivo, observado o art. 312 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("06")]
        C06,
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 444 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("07")]
        C07,
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 447 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("08")]
        C08,
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 449 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("09")]
        C09,
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 450 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("10")]
        C10,
        [Description("Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 462 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("11")]
        C11,
        [Description("Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 465 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("12")]
        C12,
        [Description("Crédito presumido na aquisição pela indústria na Área de Livre Comércio, observado o art. 467 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("13")]
        C13,
    }
}