using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using CTe.CTeOSDocumento.Wsdl;
using CTe.CTeOSDocumento.Wsdl.Cabecalho;
using CTe.CTeOSDocumento.Wsdl.Corpo;
using DFe.Wsdl;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos.Extensoes;
using NFe.Utils;
using NFe.Utils.Enderecos;
using NFe.Wsdl;
using NFe.Wsdl.AdmCsc;
using NFe.Wsdl.Autorizacao;
using NFe.Wsdl.Autorizacao.SVAN;
using NFe.Wsdl.Autorizacao.SVCAN;
using NFe.Wsdl.ConsultaProtocolo;
using NFe.Wsdl.ConsultaProtocolo.SVAN;
using NFe.Wsdl.ConsultaProtocolo.SVCAN;
using NFe.Wsdl.DistribuicaoDFe;
using NFe.Wsdl.Download;
using NFe.Wsdl.Evento;
using NFe.Wsdl.Evento.AN;
using NFe.Wsdl.Evento.SVAN;
using NFe.Wsdl.Evento.SVCAN;
using NFe.Wsdl.Inutilizacao;
using NFe.Wsdl.Inutilizacao.SVAN;
using NFe.Wsdl.Recepcao;
using NFe.Wsdl.Status;
using NFe.Wsdl.Status.SVAN;
using NFe.Wsdl.Status.SVCAN;
using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Servicos
{
    public static class ServicoNfeFactory
    {
        /// <summary>
        /// Cria o cliente <see cref="SoapHttpClientProtocol"/> para o serviço de Autorização
        /// </summary>
        /// <param name="cfg">Configuração do serviço</param>
        /// <param name="certificado">Certificado</param>
        /// <returns></returns>
        public static INfeServicoAutorizacao CriaWsdlAutorizacao(ConfiguracaoServico cfg, X509Certificate2 certificado, bool compactarMensagem)
        {
            string url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, cfg);

            if (cfg.UsaSvanNFe4(cfg.VersaoNFeAutorizacao))
            {
                return new NFeAutorizacao4SVAN(url, certificado, cfg.TimeOut);
            }

            if (cfg.UsaSvcanNFe4(cfg.VersaoNFeAutorizacao))
            {
                return new NFeAutorizacao4SVCAN(url, certificado, cfg.TimeOut);
            }

            if (cfg.VersaoNFeAutorizacao == VersaoServico.Versao400)
            {
                return new NFeAutorizacao4(url, certificado, cfg.TimeOut, compactarMensagem, ConverteVersaoLayout(cfg.VersaoNFeAutorizacao), cfg.cUF);
            }

            if (cfg.cUF == Estado.PR & cfg.VersaoNFeAutorizacao == VersaoServico.Versao310)
            {
                return new NfeAutorizacao3(url, certificado, cfg.TimeOut);
            }

            return new NfeAutorizacao(url, certificado, cfg.TimeOut);
        }

        private static DFe.Classes.Flags.VersaoServico ConverteVersaoLayout(VersaoServico cfgVersaoNFeAutorizacao)
        {
            switch (cfgVersaoNFeAutorizacao)
            {
                case VersaoServico.Versao100:
                    return DFe.Classes.Flags.VersaoServico.Versao100;
                case VersaoServico.Versao200:
                    return DFe.Classes.Flags.VersaoServico.Versao200;
                case VersaoServico.Versao310:
                    return DFe.Classes.Flags.VersaoServico.Versao310;
                case VersaoServico.Versao400:
                    return DFe.Classes.Flags.VersaoServico.Versao400;
                default:
                    throw new ArgumentOutOfRangeException("cfgVersaoNFeAutorizacao", cfgVersaoNFeAutorizacao, null);
            }
        }

        /// <summary>
        /// Cria o cliente <see cref="SoapHttpClientProtocol"/> para os serviços passados no parâmetro <paramref name="servico"/>
        /// </summary>
        /// <param name="servico">Tipo </param>
        /// <param name="cfg">Configuração do serviço</param>
        /// <param name="certificado">Certificado</param>
        /// <returns></returns>        
        public static INfeServico CriaWsdlOutros(ServicoNFe servico, ConfiguracaoServico cfg, X509Certificate2 certificado)
        {
            string url = Enderecador.ObterUrlServico(servico, cfg);

            switch (servico)
            {
                case ServicoNFe.NfeStatusServico:
                    if (cfg.UsaSvanNFe4(cfg.VersaoNfeStatusServico))
                    {
                        return new NfeStatusServico4NFeSVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.UsaSvcanNFe4(cfg.VersaoNfeStatusServico))
                    {
                        return new NfeStatusServico4NFeSVCAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.PR & cfg.VersaoNfeStatusServico == VersaoServico.Versao310)
                    {
                        return new NfeStatusServico3(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.BA & cfg.VersaoNfeStatusServico == VersaoServico.Versao310 & cfg.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeStatusServico(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeStatusServico == VersaoServico.Versao400)
                    {
                        return new NfeStatusServico4(url, certificado, cfg.TimeOut);
                    }

                    return new NfeStatusServico2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeConsultaProtocolo:
                    if (cfg.UsaSvanNFe4(cfg.VersaoNfeConsultaProtocolo))
                    {
                        return new NfeConsultaProtocolo4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.UsaSvcanNFe4(cfg.VersaoNfeConsultaProtocolo))
                    {
                        return new NfeConsultaProtocolo4SVCAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeConsultaProtocolo == VersaoServico.Versao400)
                    {
                        return new NfeConsultaProtocolo4(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.PR & cfg.VersaoNfeConsultaProtocolo == VersaoServico.Versao310)
                    {
                        return new NfeConsultaProtocolo3(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.BA & cfg.VersaoNfeConsultaProtocolo == VersaoServico.Versao310 &
                        cfg.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeConsultaProtocolo(url, certificado, cfg.TimeOut);
                    }

                    return new NfeConsultaProtocolo2(url, certificado, cfg.TimeOut);


                case ServicoNFe.NfeRecepcao:
                    return new NfeRecepcao2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeRetRecepcao:
                    return new NfeRetRecepcao2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NFeAutorizacao:
                    throw new Exception(string.Format("O serviço {0} não pode ser criado no método {1}!", servico,
                        MethodBase.GetCurrentMethod().Name));

                case ServicoNFe.NFeRetAutorizacao:
                    if (cfg.UsaSvanNFe4(cfg.VersaoNFeRetAutorizacao))
                    {
                        return new NfeRetAutorizacao4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.UsaSvcanNFe4(cfg.VersaoNFeRetAutorizacao))
                    {
                        return new NfeRetAutorizacao4SVCAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNFeRetAutorizacao == VersaoServico.Versao400)
                    {
                        return new NfeRetAutorizacao4(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.PR & cfg.VersaoNFeAutorizacao == VersaoServico.Versao310)
                    {
                        return new NfeRetAutorizacao3(url, certificado, cfg.TimeOut);
                    }

                    return new NfeRetAutorizacao(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeInutilizacao:
                    if (cfg.UsaSvanNFe4(cfg.VersaoNfeInutilizacao))
                    {
                        return new NFeInutilizacao4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeInutilizacao == VersaoServico.Versao400)
                    {
                        return new NFeInutilizacao4(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.PR & cfg.VersaoNfeInutilizacao == VersaoServico.Versao310)
                    {
                        return new NfeInutilizacao3(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.BA & cfg.VersaoNfeInutilizacao == VersaoServico.Versao310 & cfg.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeInutilizacao(url, certificado, cfg.TimeOut);
                    }

                    return new NfeInutilizacao2(url, certificado, cfg.TimeOut);

                case ServicoNFe.RecepcaoEventoCancelmento:
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                    if (cfg.UsaSvanNFe4(cfg.VersaoRecepcaoEventoCceCancelamento))
                    {
                        return new RecepcaoEvento4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.UsaSvcanNFe4(cfg.VersaoRecepcaoEventoCceCancelamento))
                    {
                        return new RecepcaoEvento4SVCAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoRecepcaoEventoCceCancelamento == VersaoServico.Versao400)
                    {
                        return new RecepcaoEvento4(url, certificado, cfg.TimeOut);
                    }

                    return new RecepcaoEvento(url, certificado, cfg.TimeOut);

                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    if (cfg.VersaoRecepcaoEventoManifestacaoDestinatario == VersaoServico.Versao400)
                    {
                        return new RecepcaoEventoManifestacaoDestinatario4AN(url, certificado, cfg.TimeOut);
                    }

                    return new RecepcaoEvento(url, certificado, cfg.TimeOut);

                case ServicoNFe.RecepcaoEventoEpec:
                    if (cfg.VersaoRecepcaoEventoEpec == VersaoServico.Versao400)
                    {
                        return new RecepcaoEvento4AN(url, certificado, cfg.TimeOut);
                    }

                    return new RecepcaoEPEC(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeConsultaCadastro:
                    if (cfg.cUF == Estado.CE)
                    {
                        return new Wsdl.ConsultaCadastro.CE.CadConsultaCadastro2(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeConsultaCadastro == VersaoServico.Versao400)
                    {
                        return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro4(url, certificado, cfg.TimeOut);
                    }

                    return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeDownloadNF:
                    return new NfeDownloadNF(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfceAdministracaoCSC:
                    return new NfceCsc(url, certificado, cfg.TimeOut);

                case ServicoNFe.NFeDistribuicaoDFe:
                    return new NfeDistDFeInteresse(url, certificado, cfg.TimeOut);
            }

            return null;
        }
    }
}