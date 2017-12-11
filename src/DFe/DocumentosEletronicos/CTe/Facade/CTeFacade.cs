using System;
using System.Collections.Generic;
using System.Linq;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Flags;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Consulta;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Inutilizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.RetRecepcao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.StatusServico;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Evento.CorpoEvento;
using DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Facade.Flags;
using DFe.DocumentosEletronicos.CTe.Servicos.ConsultaLoteCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.ConsultaProtocoloCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EventosCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EvniarLoteCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.InutilizacaoCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.StatusServicoCTe;
using DFe.Facade;
using CteEletronico = DFe.DocumentosEletronicos.CTe.Classes.Informacoes.CTe;

namespace DFe.DocumentosEletronicos.CTe.Facade
{
    public class CTeFacade : FacadeBase
    {
        private readonly CTeEnviar _enviar;
        private readonly CTeEnviarOS _enviarOS;
        private readonly CTeEnviarLote _enviarLote;
        private readonly CTeConsultaLote _consultaLote;
        private readonly CTeStatusConsulta _statusConsulta;
        private readonly CTeConsulta _consulta;
        private readonly CTeCancelar _cancelar;
        private readonly CTeCartaCorrecao _cartaCorrecao;
        private readonly CTeInutilizacao _inutilizacao;

        public CTeFacade(DFeConfig dfeConfig, CertificadoDigital certificadoDigital) : base(dfeConfig, certificadoDigital)
        {
            _enviar = new CTeEnviar(DfeConfig, CertificadoDigital);
            _enviarOS = new CTeEnviarOS(DfeConfig, CertificadoDigital);
            _enviarLote = new CTeEnviarLote(DfeConfig, CertificadoDigital);
            _consultaLote = new CTeConsultaLote(DfeConfig, CertificadoDigital);
            _statusConsulta = new CTeStatusConsulta(DfeConfig, CertificadoDigital);
            _consulta = new CTeConsulta(DfeConfig, CertificadoDigital);
            _cancelar = new CTeCancelar(DfeConfig, CertificadoDigital);
            _cartaCorrecao = new CTeCartaCorrecao(DfeConfig, CertificadoDigital);
            _inutilizacao = new CTeInutilizacao(DfeConfig, CertificadoDigital);

            Eventos();
        }

        public RetornoEnviarCte Enviar(int lote, CteEletronico cte)
        {
            return _enviar.Enviar(lote, cte);
        }

        public retCTeOS Enviar(CTeOS.CTeOS cteOs)
        {
            return _enviarOS.Enviar(cteOs);
        }

        public retEnviCte EnviarLote(int lote, List<CteEletronico> loteCte)
        {
            return _enviarLote.EnviarLote(lote, loteCte);
        }

        public retConsReciCTe ConsultaLote(string recibo)
        {
            return _consultaLote.ConsultaLote(recibo);
        }

        public retConsStatServCte StatusConsulta()
        {
            return _statusConsulta.ConsultaStatus();
        }

        public retConsSitCTe Consulta(string chave)
        {
            return _consulta.Consulta(chave);
        }

        public retEventoCTe Cancelar(string chave, string cnpjEmitente, int sequenciaEvento, string numeroProtocolo, string justificativa, ConsultaProtocolo consultaProtocolo)
        {
            if (consultaProtocolo == ConsultaProtocolo.Sim)
            {
                var retConSitCTe = Consulta(chave);

                foreach (var procEventoCTe in retConSitCTe.procEventoCTe)
                {
                    if (procEventoCTe.eventoCTe.infEvento.tpEvento == TipoEvento.Cancelamento)
                    {
                        
                    }
                }
            }

            return _cancelar.Cancelar(chave, cnpjEmitente, sequenciaEvento, numeroProtocolo, justificativa);
        }

        public retEventoCTe CartaCorrecao(string chave, string cnpjEmitente, int sequenciaEvento, List<infCorrecao> infCorrecaos)
        {
            return _cartaCorrecao.CartaCorrecao(chave, cnpjEmitente, sequenciaEvento, infCorrecaos);
        }

        public retInutCTe Inutilizacao(ConfigInutiliza configInutiliza)
        {
            return _inutilizacao.Inutilizacao(configInutiliza);
        }

        #region Eventos

        public event EventHandler<AntesDeEnviarCteOs> AntesDeEnviarCteOsHandler;
        public event EventHandler<AntesDeValidarSchema> AntesDeValidarSchemaCteOsHandler;
        public event EventHandler<AntesDeAssinar> AntesDeAssinarCteOsHandler;

        protected virtual void OnAntesDeEnviarCteOsHandler(AntesDeEnviarCteOs e)
        {
            AntesDeEnviarCteOsHandler?.Invoke(this, e);
        }

        private void AntesEnviarCteOs(object sender, AntesDeEnviarCteOs e)
        {
            OnAntesDeEnviarCteOsHandler(e);
        }

        private void AntesValidarSchemas(object sender, AntesDeValidarSchema e)
        {
           OnAntesDeValidarSchemaHandler(e);
        }




        private void Eventos()
        {
            _enviarOS.AntesDeEnviarCteOs += AntesEnviarCteOs;
            _enviarOS.AntesDeValidarSchema += AntesValidarSchemas;
            _enviarOS.AntesDeAssinar += AntesAssinar;
        }


        protected virtual void OnAntesDeValidarSchemaHandler(AntesDeValidarSchema e)
        {
            AntesDeValidarSchemaCteOsHandler?.Invoke(this, e);
        }

        protected virtual void OnAntesDeAssinarHandler(AntesDeAssinar e)
        {
            AntesDeAssinarCteOsHandler?.Invoke(this, e);
        }

        private void AntesAssinar(object sender, AntesDeAssinar e)
        {
            OnAntesDeAssinarHandler(e);    
        }

        #endregion
    }
}