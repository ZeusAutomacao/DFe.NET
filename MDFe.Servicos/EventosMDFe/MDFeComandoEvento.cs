using DFe.Classes.Entidades;

namespace MDFe.Servicos.EventosMDFe
{
    /// <summary>
    /// Base abstrata com os dados mínimos necessários
    /// para emitir um evento de MDFe sem depender
    /// do objeto "<see cref="MDFe.Classes.Informacoes.MDFe" />" completo.
    /// </summary>
    public abstract class MDFeComandoEvento
    {
        public string Chave { get; set; }
        public Estado UfEmitente { get; set; }
        public string CnpjEmitente { get; set; }
        public string CpfEmitente { get; set; }
        public byte SequenciaEvento { get; set; }
    }
}