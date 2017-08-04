using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.ConsultaNaoEncerrados;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.ConsultaProtocolo;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.RetRecepcao;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.StatusServico;
using DFe.DocumentosEletronicos.MDFe.Servicos.ConsultaLoteMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.ConsultaNaoEncerradosMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.ConsultaProtocoloMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.EnviarLoteMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.EventosMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.StatusServicoMDFe;
using DFe.Facade;
using MdfeEletronico = DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.MDFe;

namespace DFe.DocumentosEletronicos.MDFe.Facade
{
    public class MDFeFacade : FacadeBase
    {
        private readonly MDFeEnviarLote _enviarLote;
        private readonly MDFeConsultaLote _consultaLote;
        private readonly MDFeStatusConsulta _statusConsulta;
        private readonly MDFeConsulta _consulta;
        private readonly MDFeCancelar _cancelar;
        private readonly MDFeConsultaNaoEncerradas _consultaNaoEncerradas;
        private readonly MDFeIncluirCondutor _incluirCondutor;
        private readonly MDFeEncerrar _encerrar;

        public MDFeFacade(DFeConfig dfeConfig, CertificadoDigital certificadoDigital) : base(dfeConfig, certificadoDigital)
        {
            DfeConfig = dfeConfig;

            _enviarLote = new MDFeEnviarLote(DfeConfig, CertificadoDigital);
            _consultaLote = new MDFeConsultaLote(DfeConfig, CertificadoDigital);
            _statusConsulta = new MDFeStatusConsulta(DfeConfig, CertificadoDigital);
            _consulta = new MDFeConsulta(DfeConfig, CertificadoDigital);
            _cancelar = new MDFeCancelar(DfeConfig, CertificadoDigital);
            _consultaNaoEncerradas = new MDFeConsultaNaoEncerradas(DfeConfig, CertificadoDigital);
            _incluirCondutor = new MDFeIncluirCondutor(DfeConfig, certificadoDigital);
            _encerrar = new MDFeEncerrar(DfeConfig, CertificadoDigital);
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