using DFe.Classes.Entidades;

namespace MDFe.Servicos.EventosMDFe
{
    /// <summary>
    ///     Dados para emitir o evento de encerramento de MDFe.
    /// </summary>
    public class MDFeComandoEncerramento : MDFeComandoEvento
    {
        public string Protocolo { get; set; }
        public Estado EstadoEncerramento { get; set; }
        public long CodigoMunicipioEncerramento { get; set; }
    }
}