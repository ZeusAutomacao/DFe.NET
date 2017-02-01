using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.Emitente
{
    /// <summary>
    ///     <para>1 – Simples Nacional;</para>
    ///     <para>2 – Simples Nacional – excesso de sublimite de receita bruta;</para>
    ///     <para>3 – Regime Normal. (v2.0).</para>
    /// </summary>
    public enum CRT
    {
        [XmlEnum("1")] SimplesNacional = 1,
        [XmlEnum("2")] SimplesNacionalExcessoSublimite = 2,
        [XmlEnum("3")] RegimeNormal = 3
    }
}