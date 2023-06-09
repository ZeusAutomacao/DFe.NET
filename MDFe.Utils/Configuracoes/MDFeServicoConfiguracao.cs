using DFe.Utils;
using DFe.Utils.Assinatura;
using System;
using System.Security.Cryptography.X509Certificates;

namespace MDFe.Utils.Configuracoes
{
    public class MDFeServicoConfiguracao : IDisposable
    {
        private MDFeVersaoWebService _versaoWebService;

        private X509Certificate2 _certificado;

        public bool IsSalvarXml { get; set; }
        public string CaminhoSchemas { get; set; }
        public string CaminhoSalvarXml { get; set; }
        public bool IsAdicionaQrCode { get; set; }
        public ConfiguracaoCertificado ConfiguracaoCertificado { get; set; }

        public MDFeVersaoWebService VersaoWebService
        {
            get { return GetMdfeVersaoWebService(); }
            set { _versaoWebService = value; }
        }

        public X509Certificate2 X509Certificate2
        {
            get
            {
                if (_certificado != null)
                    if (!ConfiguracaoCertificado.ManterDadosEmCache)
                        _certificado.Reset();
                _certificado = ObterCertificado();
                return _certificado;
            }
        }

        private X509Certificate2 ObterCertificado()
        {
            return CertificadoDigital.ObterCertificado(ConfiguracaoCertificado);
        }

        public bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }

        private MDFeVersaoWebService GetMdfeVersaoWebService()
        {
            if (_versaoWebService == null)
                _versaoWebService = new MDFeVersaoWebService();

            return _versaoWebService;
        }

        public void Dispose()
        {
            if (!ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }

        ~MDFeServicoConfiguracao()
        {
            if (!ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }
    }
}
