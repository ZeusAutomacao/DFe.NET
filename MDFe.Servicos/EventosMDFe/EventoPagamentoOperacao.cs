using System.Collections.Generic;
using MDFe.Classes.Informacoes;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoPagamentoOperacao
    {
        public MDFeRetEventoMDFe MDFeEventoPagamentoOperacao(Classes.Informacoes.MDFe mdfe, byte sequencia,
            string protocolo, infViagens infViagens, List<infPag> infPagamentos, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoPagamento = ClassesFactory.CriaEvPagtoOperMDFe(
                protocolo
                , infViagens
                , infPagamentos
            );

            return new ServicoController().Executar(mdfe, sequencia, eventoPagamento, MDFeTipoEvento.PagamentoOperacaoMDFe, config);
        }
    }
}