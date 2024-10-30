using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Retorno.MDFeEvento;
using System.Collections.Generic;
using DFe.Classes.Entidades;
using MDFe.Classes.Informacoes;
using MDFeEletronica = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoMDFeEvento
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(
            MDFeEletronica mdfe, byte sequenciaEvento, string nome,
            string cpf)
        {
            var eventoIncluirCondutor = new EventoInclusaoCondutor();

            return eventoIncluirCondutor.MDFeEventoIncluirCondutor(mdfe, sequenciaEvento, nome, cpf);
        }

        public MDFeRetEventoMDFe MDFeEventoIncluirDFe(
            MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos)
        {
            var eventoIncluirDFe = new EventoInclusaoDFe();

            return eventoIncluirDFe.MDFeEventoIncluirDFe(mdfe, sequenciaEvento, protocolo, codigoMunicipioCarregamento, nomeMunicipioCarregamento, informacoesDocumentos);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo)
        {
            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, sequenciaEvento, protocolo);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, Estado estadoEncerramento, long codigoMunicipioEncerramento, byte sequenciaEvento, string protocolo)
        {
            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, estadoEncerramento, codigoMunicipioEncerramento, sequenciaEvento, protocolo);
        }

        public MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string justificativa)
        {
            var eventoCancelamento = new EventoCancelar();

            return eventoCancelamento.MDFeEventoCancelar(mdfe, sequenciaEvento, protocolo, justificativa);
        }

        public MDFeRetEventoMDFe MDFeEventoPagamentoOperacaoTransporte(MDFeEletronica mdfe, byte sequenciaEvneto,
            string protocolo, infViagens infViagens, List<infPag> infPagamentos)
        {
            var eventoPagamentoOperacao = new EventoPagamentoOperacao();

            return eventoPagamentoOperacao.MDFeEventoPagamentoOperacao(mdfe, sequenciaEvneto, protocolo,
                infViagens, infPagamentos);
        }
    }
}