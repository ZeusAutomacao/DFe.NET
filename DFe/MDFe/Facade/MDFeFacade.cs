using DFe.MDFe.Classes.Retorno.Autorizacao;
using DFe.MDFe.Classes.Retorno.ConsultaNaoEncerrados;
using DFe.MDFe.Classes.Retorno.ConsultaProtocolo;
using DFe.MDFe.Classes.Retorno.Evento;
using DFe.MDFe.Classes.Retorno.RetRecepcao;
using DFe.MDFe.Classes.Retorno.StatusServico;
using DFe.MDFe.Servicos.ConsultaNaoEncerradosMDFe;
using DFe.MDFe.Servicos.ConsultaProtocoloMDFe;
using DFe.MDFe.Servicos.EventosMDFe;
using DFe.MDFe.Servicos.RecepcaoMDFe;
using DFe.MDFe.Servicos.RetRecepcaoMDFe;
using DFe.MDFe.Servicos.StatusServicoMDFe;
using MdfeEletronico = DFe.MDFe.Classes.Informacoes.MDFe;

namespace DFe.MDFe.Facade
{
    public class MDFeFacade
    {
        private readonly MDFeEnviarLote _enviarLote;
        private readonly MDFeConsultaLote _consultaLote;
        private readonly MDFeStatusConsulta _statusConsulta;
        private readonly MDFeConsulta _consulta;
        private readonly MDFeCancelar _cancelar;
        private readonly MDFeConsultaNaoEncerradas _consultaNaoEncerradas;
        private readonly MDFeIncluirCondutor _incluirCondutor;
        private readonly MDFeEncerrar _encerrar;

        public MDFeFacade()
        {
            _enviarLote = new MDFeEnviarLote();
            _consultaLote = new MDFeConsultaLote();
            _statusConsulta = new MDFeStatusConsulta();
            _consulta = new MDFeConsulta();
            _cancelar = new MDFeCancelar();
            _consultaNaoEncerradas = new MDFeConsultaNaoEncerradas();
            _incluirCondutor = new MDFeIncluirCondutor();
            _encerrar = new MDFeEncerrar();
        }

        public retEnviMDFe EnviarLote(long lote, MdfeEletronico mdfe)
        {
            return _enviarLote.EnviarLote(lote, mdfe);
        }

        public retConsReciMDFe ConsultaLote(string numeroRecibo)
        {
            return _consultaLote.ConsultaLote(numeroRecibo);
        }

        public retConsStatServMDFe StatusConsulta()
        {
            return _statusConsulta.StatusConsulta();
        }

        public retConsSitMDFe Consulta(string chave)
        {
            return _consulta.Consulta(chave);
        }

        public retEventoMDFe Cancelar(string chave, string cnpjEmitente, byte sequenciaEvento, string protocolo, string justificativa)
        {
            return _cancelar.Cancelar(chave, cnpjEmitente, sequenciaEvento, protocolo, justificativa);
        }

        public retConsMDFeNaoEnc ConsultaNaoEncerradas(string cnpj)
        {
            return _consultaNaoEncerradas.MDFeConsultaNaoEncerrados(cnpj);
        }

        public retEventoMDFe IncluirCondutor(string chave, string cnpj, byte sequenciaEvento, string nome, string cpf)
        {
            return _incluirCondutor.MDFeEventoIncluirCondutor(chave, cnpj, sequenciaEvento, nome, cpf);
        }

        public retEventoMDFe Encerrar(string chave, string cnpj, long codigoIbgeCidade, byte sequenciaEvento, string protocolo)
        {
            return _encerrar.MDFeEventoEncerramento(chave, cnpj, codigoIbgeCidade, sequenciaEvento, protocolo);
        }
    }
}