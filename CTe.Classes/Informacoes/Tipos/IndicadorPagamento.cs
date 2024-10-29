using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Indicador da forma de pagamento
    ///     <para>0 - pagamento à vista;</para>
    ///     <para>1 - pagamento à prazo;</para>
    ///     <para>2 - outros.</para>
    /// </summary>
    public enum IndicadorPagamento
    {
        [XmlEnum("0")]
        ipVista,
        [XmlEnum("1")]
        ipPrazo,
        [XmlEnum("2")]
        ipOutras
    }
}