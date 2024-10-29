using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using DFe.Utils.Assinatura;
using System;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Utils.Configuracoes
{
    public class MDFeConfiguracao : IDisposable 
    {
        private static MDFeVersaoWebService _versaoWebService;

        public MDFeConfiguracao()
        {
            VersaoWebService = new MDFeVersaoWebService();
        }

        public static ConfiguracaoCertificado ConfiguracaoCertificado { get; set; }

        public static bool IsSalvarXml { get; set; }
        public static string CaminhoSchemas { get; set; }
        public static string CaminhoSalvarXml { get; set; }
        public static bool IsAdicionaQrCode { get; set; }

        public static MDFeVersaoWebService VersaoWebService
        {
            get { return GetMdfeVersaoWebService(); }
            set { _versaoWebService = value; }
        }

        private static MDFeVersaoWebService GetMdfeVersaoWebService()
        {
            if(_versaoWebService == null)
                _versaoWebService = new MDFeVersaoWebService();

            return _versaoWebService;
        }

        private static X509Certificate2 _certificado = null;
        public static X509Certificate2 X509Certificate2
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

        public static bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }

        private static X509Certificate2 ObterCertificado()
        {
            return CertificadoDigital.ObterCertificado(ConfiguracaoCertificado);
        }

        public void Dispose()
        {
            if (!ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }

        ~MDFeConfiguracao()
        {
            if (!ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }
    }

    public class MDFeVersaoWebService
    {
        public int TimeOut { get; set; }
        public Estado UfEmitente { get; set; }
        public TipoAmbiente TipoAmbiente { get; set; }
        public VersaoServico VersaoLayout { get; set; }
    }
}