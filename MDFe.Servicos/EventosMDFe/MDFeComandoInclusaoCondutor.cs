namespace MDFe.Servicos.EventosMDFe
{
    /// <summary>
    ///     Dados para emitir o evento de inclusão de condutor no MDFe.
    /// </summary>
    public class MDFeComandoInclusaoCondutor : MDFeComandoEvento
    {
        public string Nome { get; set; }
        public string CpfCondutor { get; set; }
    }
}