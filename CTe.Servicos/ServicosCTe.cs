using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Inutilizacao;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Recepcao.Retorno;
using CTe.Classes.Servicos.Status;
using CTe.CTeOSClasses;
using CTe.CTeOSDocumento.CTe.CTeOS.Servicos.Autorizacao;
using CTe.Servicos.ConsultaProtocolo;
using CTe.Servicos.ConsultaRecibo;
using CTe.Servicos.ConsultaStatus;
using CTe.Servicos.DistribuicaoDFe;
using CTe.Servicos.EnviarCte;
using CTe.Servicos.Inutilizacao;
using CTe.Servicos.Recepcao;
using DFe.Utils;
using DFe.Utils.Assinatura;
using CTeEletronico = CTe.Classes.CTe;

namespace CTe.Servicos
{
    public sealed class ServicosCTe : IServicosCTe
    {
        private readonly ConfiguracaoServico _configuracaoServico;
        private readonly X509Certificate2 _certificado;
        private readonly bool _controlarCertificado;

        public ServicosCTe(ConfiguracaoServico configuracaoServico, X509Certificate2 certificado = null)
        {
            _configuracaoServico = configuracaoServico ?? throw new ArgumentNullException(nameof(configuracaoServico));
            _controlarCertificado = certificado == null;
            _certificado = certificado ?? configuracaoServico.X509Certificate2;
        }

        // === Assinatura ===

        public CTeEletronico Assina(CTeEletronico cte)
        {
            var modeloDocumentoFiscal = cte.infCte.ide.mod;
            var tipoEmissao = (int)cte.infCte.ide.tpEmis;
            var codigoNumerico = cte.infCte.ide.cCT;
            var estado = cte.infCte.ide.cUF;
            var dataEHoraEmissao = cte.infCte.ide.dhEmi;
            var cnpj = cte.infCte.emit.CNPJ;
            var numeroDocumento = cte.infCte.ide.nCT;
            int serie = cte.infCte.ide.serie;

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal,
                serie, numeroDocumento, tipoEmissao, codigoNumerico);

            cte.infCte.Id = "CTe" + dadosChave.Chave;
            cte.infCte.versao = _configuracaoServico.ObterVersaoLayoutValida();
            cte.infCte.ide.cDV = dadosChave.DigitoVerificador;

            var assinatura = AssinaturaDigital.Assina(cte, cte.infCte.Id, _certificado);
            cte.Signature = assinatura;

            return cte;
        }

        public CTeOS Assina(CTeOS cteOs)
        {
            var modeloDocumentoFiscal = cteOs.InfCte.ide.mod;
            var tipoEmissao = (int)cteOs.InfCte.ide.tpEmis;
            var codigoNumerico = cteOs.InfCte.ide.cCT;
            var estado = cteOs.InfCte.ide.cUF;
            var dataEHoraEmissao = cteOs.InfCte.ide.dhEmi;
            var cnpj = cteOs.InfCte.emit.CNPJ;
            var numeroDocumento = cteOs.InfCte.ide.nCT;
            int serie = cteOs.InfCte.ide.serie;

            var dadosChave = ChaveFiscal.ObterChave(estado, dataEHoraEmissao, cnpj, modeloDocumentoFiscal,
                serie, numeroDocumento, tipoEmissao, codigoNumerico);

            cteOs.InfCte.Id = "CTe" + dadosChave.Chave;
            cteOs.InfCte.versao = DFe.Classes.Flags.VersaoServico.Versao400;
            cteOs.InfCte.ide.cDV = dadosChave.DigitoVerificador;

            var assinatura = AssinaturaDigital.Assina(cteOs, cteOs.InfCte.Id, _certificado);
            cteOs.Signature = assinatura;

            return cteOs;
        }

        public eventoCTe Assina(eventoCTe evento)
        {
            if (evento.infEvento.Id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            evento.Signature = AssinaturaDigital.Assina(evento, evento.infEvento.Id, _certificado);

            return evento;
        }

        // === Consulta de Protocolo ===

        public retConsSitCTe ConsultaProtocolo(string chave)
        {
            return new ConsultaProtcoloServico().ConsultaProtocolo(chave, _configuracaoServico);
        }

        public retConsSitCTe ConsultaProtocoloV4(string chave)
        {
            return new ConsultaProtcoloServico().ConsultaProtocoloV4(chave, _configuracaoServico);
        }

        public Task<retConsSitCTe> ConsultaProtocoloAsync(string chave)
        {
            return new ConsultaProtcoloServico().ConsultaProtocoloAsync(chave, _configuracaoServico);
        }

        // === Consulta de Recibo ===

        public retConsReciCTe ConsultarRecibo(string recibo)
        {
            return new ConsultaReciboServico(recibo).Consultar(_configuracaoServico);
        }

        public Task<retConsReciCTe> ConsultarReciboAsync(string recibo)
        {
            return new ConsultaReciboServico(recibo).ConsultarAsync(_configuracaoServico);
        }

        // === Status do Serviço ===

        public retConsStatServCte ConsultaStatus()
        {
            return new StatusServico().ConsultaStatus(_configuracaoServico);
        }

        public retConsStatServCTe ConsultaStatusV4()
        {
            return new StatusServico().ConsultaStatusV4(_configuracaoServico);
        }

        public Task<retConsStatServCte> ConsultaStatusAsync()
        {
            return new StatusServico().ConsultaStatusAsync(_configuracaoServico);
        }

        // === Distribuição DFe ===

        public RetornoCteDistDFeInt CTeDistDFeInteresse(string ufAutor, string documento, string ultNSU = "0", string nSU = "0")
        {
            return new ServicoCTeDistribuicaoDFe(_configuracaoServico, _certificado)
                .CTeDistDFeInteresse(ufAutor, documento, ultNSU, nSU, _configuracaoServico);
        }

        public Task<RetornoCteDistDFeInt> CTeDistDFeInteresseAsync(string ufAutor, string documento, string ultNSU = "0", string nSU = "0")
        {
            return new ServicoCTeDistribuicaoDFe(_configuracaoServico, _certificado)
                .CTeDistDFeInteresseAsync(ufAutor, documento, ultNSU, nSU, _configuracaoServico);
        }

        // === Enviar CTe ===

        public RetornoEnviarCte Enviar(int lote, CTeEletronico cte)
        {
            return new EnviarCte.ServicoEnviarCte().Enviar(lote, cte, _configuracaoServico);
        }

        public Task<RetornoEnviarCte> EnviarAsync(int lote, CTeEletronico cte)
        {
            return new EnviarCte.ServicoEnviarCte().EnviarAsync(lote, cte, _configuracaoServico);
        }

        // === Inutilização ===

        public retInutCTe Inutilizar(ConfigInutiliza configInutiliza)
        {
            return new InutilizacaoServico(configInutiliza).Inutilizar(_configuracaoServico);
        }

        public Task<retInutCTe> InutilizarAsync(ConfigInutiliza configInutiliza)
        {
            return new InutilizacaoServico(configInutiliza).InutilizarAsync(_configuracaoServico);
        }

        // === Recepção CTe ===

        public retEnviCte CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList)
        {
            return new ServicoCTeRecepcao().CTeRecepcao(lote, cteEletronicosList, _configuracaoServico);
        }

        public Task<retEnviCte> CTeRecepcaoAsync(int lote, List<CTeEletronico> cteEletronicosList)
        {
            return new ServicoCTeRecepcao().CTeRecepcaoAsync(lote, cteEletronicosList, _configuracaoServico);
        }

        public retCTe CTeRecepcaoSincronoV4(CTeEletronico cte)
        {
            return new ServicoCTeRecepcao().CTeRecepcaoSincronoV4(cte, _configuracaoServico);
        }

        // === Recepção CTeOS ===

        public retCTeOS CTeOSRecepcaoSincronoV4(CTeOS cte)
        {
            return new ServicoCTeOSRecepcao().CTeRecepcaoSincronoV4(cte, _configuracaoServico);
        }

        // === Dispose ===

        public void Dispose()
        {
            if (_controlarCertificado)
                _certificado?.Dispose();
        }
    }
}
