using System.Xml.Serialization;

namespace DFe.DocumentosEletronicos.CTe.Classes.Flags
{
    /// <summary>
    ///     Indicador de Sincronização:
    ///     <para>0 = Assíncrono. A resposta deve ser obtida consultando o serviço NFeRetAutorizacao, com o nº do recibo</para>
    ///     <para>
    ///         1 = Síncrono. Empresa solicita processamento síncrono do Lote de NF-e (sem a geração de Recibo para consulta
    ///         futura);
    ///     </para>
    /// </summary>
    public enum IndicadorSincronizacao
    {
        [XmlEnum("0")] Assincrono = 0,
        [XmlEnum("1")] Sincrono = 1
    }
}