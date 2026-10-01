using MDFe.Classes.Informacoes;
using System.Collections.Generic;

namespace MDFe.Servicos.EventosMDFe
{
    /// <summary>
    ///     Dados para emitir o evento de pagamento de operação de transporte.
    /// </summary>
    public class MDFeComandoPagamentoOperacao : MDFeComandoEvento
    {
        public string Protocolo { get; set; }
        public MDFeInfViagens InfViagens { get; set; }
        public List<MDFeInfPag> Pagamentos { get; set; }
    }
}