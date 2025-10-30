using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Retorno.MDFeEvento;
using System.Collections.Generic;
using DFe.Classes.Entidades;
using MDFe.Classes.Informacoes;
using MDFeEletronica = MDFe.Classes.Informacoes.MDFe;
using MDFe.Utils.Configuracoes;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoMDFeEvento
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(
            MDFeEletronica mdfe, byte sequenciaEvento, string nome,
            string cpf, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoIncluirCondutor = new EventoInclusaoCondutor();

            return eventoIncluirCondutor.MDFeEventoIncluirCondutor(mdfe, sequenciaEvento, nome, cpf, config);
        }

        public MDFeRetEventoMDFe MDFeEventoIncluirDFe(
            MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoIncluirDFe = new EventoInclusaoDFe();

            return eventoIncluirDFe.MDFeEventoIncluirDFe(mdfe, sequenciaEvento, protocolo, codigoMunicipioCarregamento, nomeMunicipioCarregamento, informacoesDocumentos, config);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, sequenciaEvento, protocolo, config);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, Estado estadoEncerramento, long codigoMunicipioEncerramento, byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, estadoEncerramento, codigoMunicipioEncerramento, sequenciaEvento, protocolo, config);
        }

        public MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string justificativa, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoCancelamento = new EventoCancelar();

            return eventoCancelamento.MDFeEventoCancelar(mdfe, sequenciaEvento, protocolo, justificativa, config);
        }

        public MDFeRetEventoMDFe MDFeEventoPagamentoOperacaoTransporte(MDFeEletronica mdfe, byte sequenciaEvneto,
            string protocolo, infViagens infViagens, List<infPag> infPagamentos, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoPagamentoOperacao = new EventoPagamentoOperacao();

            return eventoPagamentoOperacao.MDFeEventoPagamentoOperacao(mdfe, sequenciaEvneto, protocolo,
                infViagens, infPagamentos, config);
        }
    }
}