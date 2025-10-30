using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    public enum tpServ
    {
        [XmlEnum("0")]
        normal = 0,
        [XmlEnum("1")]
        subcontratacao,
        [XmlEnum("2")]
        redespacho,
        [XmlEnum("3")]
        redespachoIntermediario,
        [XmlEnum("4")]
        servicoVinculadoMultimodal,
        [XmlEnum("6")]
        transportePessoas = 6,
        [XmlEnum("7")]
        transporteValores = 7,
        [XmlEnum("8")]
        excessoBagagem = 8
    }
}