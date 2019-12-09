using System;
using System.Net;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using DFeFacadeBase;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils;

namespace DFeFacadeZeus
{
    public class ZeusConfig : DFeBase<ConfiguracaoServico>
    {
        public DFeEstado ObterEstado()
        {
            return DFeEstado.GO;
        }

        public DFeAmbiente ObterAmbiente()
        {
            return DFeAmbiente.Homologacao;
        }

        public DFeModeloDocumento ObterModeloDocumento()
        {
            return DFeModeloDocumento.NFe;
        }

        public DFeTipoEmissao ObterTipoEmissao()
        {
            return DFeTipoEmissao.Normal;
        }

        public int ObterTimeOut()
        {
            return 30000;
        }

        public ICertificadoDigital ConfiguracaoCertificadoDigital()
        {
            return new CertificadoDigitalA1(
                @"C:\Users\rober\OneDrive\Roberto\Documentos\Certificados\AGIL4 TECNOLOGIA LTDA VENC 16-10-2019.pfx"
                ,"agil4@321")
            {
                ManterEmCache = true,
                CacheId = "1"
            };
        }

        public ConfiguracaoServico ObterConfiguracao()
        {
            return new ConfiguracaoServico
            {
                Certificado = ObterConfiguracaoCertificado(),
                DefineVersaoServicosAutomaticamente = true,
                ModeloDocumento = ResolveModeloDocumento(),
                ProtocoloDeSeguranca = SecurityProtocolType.Tls12,
                TimeOut = ObterTimeOut(),
                ValidarCertificadoDoServidor = false,
                RemoverAcentos = true,
                VersaoLayout = VersaoServico.Versao400,
                cUF = ResolveEstadoUf(),
                tpAmb = ResolveTipoAmbiente(),
                tpEmis = ResolveTipoEmissao()
            };
        }

        private TipoEmissao ResolveTipoEmissao()
        {
            return (TipoEmissao) ObterTipoEmissao();
        }

        private TipoAmbiente ResolveTipoAmbiente()
        {
            return (TipoAmbiente) ObterAmbiente();
        }

        private Estado ResolveEstadoUf()
        {
            return (Estado) ObterEstado();
        }

        private ModeloDocumento ResolveModeloDocumento()
        {
            switch (ObterModeloDocumento())
            {
                case DFeModeloDocumento.NFe:
                    return ModeloDocumento.NFe;
                case DFeModeloDocumento.NFCe:
                    return ModeloDocumento.NFCe;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private ConfiguracaoCertificado ObterConfiguracaoCertificado()
        {
            TipoCertificado tipoCertificado = ResolveTipoCertificado();

            return ResolveConfiguracaoCertificado(tipoCertificado);
        }

        private ConfiguracaoCertificado ResolveConfiguracaoCertificado(TipoCertificado tipoCertificado)
        {
            switch (tipoCertificado)
            {
                case TipoCertificado.A1Repositorio:
                    var configA1Repositorio = (CertificadoDigitalA1Repositorio)ConfiguracaoCertificadoDigital();

                    return new ConfiguracaoCertificado
                    {
                        TipoCertificado = tipoCertificado,
                        Serial = configA1Repositorio.Serial,
                        ManterDadosEmCache = configA1Repositorio.ManterEmCache,
                        CacheId = configA1Repositorio.CacheId
                    };
                case TipoCertificado.A1Arquivo:
                    var configA1Arquivo = (CertificadoDigitalA1)ConfiguracaoCertificadoDigital();

                    return new ConfiguracaoCertificado
                    {
                        TipoCertificado = tipoCertificado,
                        Arquivo = configA1Arquivo.LocalArquivoPrfx,
                        ManterDadosEmCache = configA1Arquivo.ManterEmCache,
                        CacheId = configA1Arquivo.CacheId,
                        Senha = configA1Arquivo.Senha
                    };
                case TipoCertificado.A3:
                    var configA3 = (CertificadoDigitalA3)ConfiguracaoCertificadoDigital();

                    return new ConfiguracaoCertificado
                    {
                        TipoCertificado = tipoCertificado,
                        Serial = configA3.Serial,
                        ManterDadosEmCache = configA3.ManterEmCache,
                        CacheId = configA3.CacheId,
                        Senha = configA3.Senha
                    };
                case TipoCertificado.A1ByteArray:
                    var configA1ByteArray = (CertificadoDigitalA1Repositorio)ConfiguracaoCertificadoDigital();

                    return new ConfiguracaoCertificado
                    {
                        TipoCertificado = tipoCertificado,
                        Serial = configA1ByteArray.Serial,
                        ManterDadosEmCache = configA1ByteArray.ManterEmCache,
                        CacheId = configA1ByteArray.CacheId
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoCertificado), tipoCertificado, null);
            }
        }

        private TipoCertificado ResolveTipoCertificado()
        {
            var certificadoFacade = ObterConfiguracaoCertificado();

            switch (certificadoFacade.TipoCertificado)
            {
                case TipoCertificado.A1Repositorio:
                    return TipoCertificado.A1Repositorio;
                case TipoCertificado.A1Arquivo:
                    return TipoCertificado.A1Arquivo;
                case TipoCertificado.A3:
                    return TipoCertificado.A3;
                case TipoCertificado.A1ByteArray:
                    return TipoCertificado.A1ByteArray;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}