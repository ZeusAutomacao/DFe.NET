using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Forma de emissão da CT-e
    ///     <para>1 - Emissão normal (não em contingência)</para>
    ///     <para>4 - Contingência EPEC pela SVC</para>
    ///     <para>5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)</para>
    ///     <para>8 - Contingência SVC-SP (SEFAZ Virtual de Contingência de SP)</para>
    /// </summary>
    public enum tpEmis
    {
        [XmlEnum("1")]
        teNormal = 1,
        [XmlEnum("4")]
        teEPEC = 4,
        [XmlEnum("5")]
        teFSDA = 5,
        [XmlEnum("7")]
        teSVCRS = 7,
        [XmlEnum("8")]
        teSVCSP = 8
    }
}