namespace MDFe.Servicos.EventosMDFe
{
    /// <summary>
    ///     Dados para emitir o evento de cancelamento de MDFe.
    /// </summary>
    public class MDFeComandoCancelamento : MDFeComandoEvento
    {
        public string Protocolo { get; set; }
        public string Justificativa { get; set; }
    }
}