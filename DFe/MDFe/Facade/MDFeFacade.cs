using DFe.MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using DFe.MDFe.Classes.Retorno.MDFeEvento;
using DFe.MDFe.Classes.Retorno.MDFeRecepcao;
using DFe.MDFe.Classes.Retorno.MDFeRetRecepcao;
using DFe.MDFe.Classes.Retorno.MDFeStatusServico;
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

        public MDFeFacade()
        {
            _enviarLote = new MDFeEnviarLote();
            _consultaLote = new MDFeConsultaLote();
            _statusConsulta = new MDFeStatusConsulta();
            _consulta = new MDFeConsulta();
            _cancelar = new MDFeCancelar();
        }

        public MDFeRetEnviMDFe EnviarLote(long lote, MdfeEletronico mdfe)
        {
            return _enviarLote.EnviarLote(lote, mdfe);
        }

        public MDFeRetConsReciMDFe ConsultaLote(string numeroRecibo)
        {
            return _consultaLote.ConsultaLote(numeroRecibo);
        }

        public MDFeRetConsStatServ StatusConsulta()
        {
            return _statusConsulta.StatusConsulta();
        }

        public MDFeRetConsSitMDFe Consulta(string chave)
        {
            return _consulta.Consulta(chave);
        }

        public MDFeRetEventoMDFe Cancelar(MdfeEletronico mdfe, byte sequenciaEvento, string protocolo, string justificativa)
        {
            return _cancelar.Cancelar(mdfe, sequenciaEvento, protocolo, justificativa);
        }
    }
}