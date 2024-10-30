using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Tipos
{
    /// <summary>
    ///     Processo de emissão utilizado com a seguinte codificação:
    ///     <para>0 - emissão de NF-e com aplicativo do contribuinte;</para>
    ///     <para>Versão 2.0 / 1 - emissão de NF-e avulsa pelo Fisco;</para>
    ///     <para>Versão 2.0 / 2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;</para>
    ///     <para>3- emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.</para>
    /// </summary>
    public enum procEmi
    {
        [XmlEnum("0")]
        AplicativoContribuinte,
        [XmlEnum("1")]
        AvulsaFisco,
        [XmlEnum("2")]
        AvulsaContribuinte,
        [XmlEnum("3")]
        ContribuinteAplicativoFisco
    }
}