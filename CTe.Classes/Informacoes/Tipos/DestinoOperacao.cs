using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Identificador de Local de destino da operação (1-Interna;2-Interestadual;3-Exterior)
    /// </summary>
    public enum DestinoOperacao
    {
        [XmlEnum("1")]
        doInterna,
        [XmlEnum("2")]
        doInterestadual,
        [XmlEnum("3")]
        doExterior
    }
}