using System.Collections.Generic;
using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Consulta;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Inutilizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.RetRecepcao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.StatusServico;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Evento.CorpoEvento;
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
        private readonly CTeOSEnviarLote _enviarLoteCTeOS;
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
            _enviarLoteCTeOS = new CTeOSEnviarLote(DfeConfig, CertificadoDigital);
            _consultaLote = new CTeConsultaLote(DfeConfig, CertificadoDigital);
            _statusConsulta = new CTeStatusConsulta(DfeConfig, CertificadoDigital);
            _consulta = new CTeConsulta(DfeConfig, CertificadoDigital);
            _cancelar = new CTeCancelar(DfeConfig, CertificadoDigital);
            _cartaCorrecao = new CTeCartaCorrecao(DfeConfig, CertificadoDigital);
            _inutilizacao = new CTeInutilizacao(DfeConfig, CertificadoDigital);
        }

        public RetornoEnviarCte Enviar(int lote, CteEletronico cte)
        {
            return _enviar.Enviar(lote, cte);
        }

        public retEnviCte EnviarLote(int lote, List<CteEletronico> loteCte)
        {
            return _enviarLote.EnviarLote(lote, loteCte);
        }

        public retEnviCte EnviarLoteCTeOS(int lote, List<CTeOS.CTeOS> loteCteOs)
        {
            return _enviarLoteCTeOS.EnviarLote(lote, loteCteOs);
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

        public retEventoCTe Cancelar(string chave, string cnpjEmitente, int sequenciaEvento, string numeroProtocolo, string justificativa)
        {
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
    }
}