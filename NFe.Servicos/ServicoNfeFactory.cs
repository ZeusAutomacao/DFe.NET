using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils;
using NFe.Wsdl;
using NFe.Wsdl.AdmCsc;
using NFe.Wsdl.Autorizacao;
using NFe.Wsdl.Autorizacao.SVAN;
using NFe.Wsdl.ConsultaProtocolo;
using NFe.Wsdl.ConsultaProtocolo.SVAN;
using NFe.Wsdl.DistribuicaoDFe;
using NFe.Wsdl.Download;
using NFe.Wsdl.Evento;
using NFe.Wsdl.Evento.AN;
using NFe.Wsdl.Evento.SVAN;
using NFe.Wsdl.Inutilizacao;
using NFe.Wsdl.Inutilizacao.SVAN;
using NFe.Wsdl.Recepcao;
using NFe.Wsdl.Status;
using NFe.Wsdl.Status.SVAN;

namespace NFe.Servicos
{
    public static class ServicoNfeFactory
    {
        public static INfeServicoAutorizacao CriaWsdlAutorizacao(ConfiguracaoServico cfg, X509Certificate2 certificado) 
        {
            var url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, cfg);

            if (cfg.IsSvanNFe4())
            {
                return new NFeAutorizacao4SVAN(url, certificado, cfg.TimeOut);
            }

            if (cfg.VersaoNFeAutorizacao == VersaoServico.ve400)
                return new NFeAutorizacao4(url, certificado, cfg.TimeOut);



            if (cfg.cUF == Estado.PR & cfg.VersaoNFeAutorizacao == VersaoServico.ve310)
                return new NfeAutorizacao3(url, certificado, cfg.TimeOut);

            return new NfeAutorizacao(url, certificado, cfg.TimeOut);
        }

        public static INfeServico CriaWsdlOutros(
            ServicoNFe servico, 
            ConfiguracaoServico cfg,
            X509Certificate2 certificado)
        {
            var url = Enderecador.ObterUrlServico(servico, cfg);
            switch (servico)
            {
                case ServicoNFe.NfeStatusServico:
                    if (cfg.cUF == Estado.PR & cfg.VersaoNfeStatusServico == VersaoServico.ve310)
                    {
                        return new NfeStatusServico3(url, certificado, cfg.TimeOut);
                    }
                    if (cfg.cUF == Estado.BA & cfg.VersaoNfeStatusServico == VersaoServico.ve310 &
                        cfg.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeStatusServico(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.IsSvanNFe4())
                    {
                        return new NfeStatusServico4NFeSVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeStatusServico == VersaoServico.ve400)
                    {
                        return new NfeStatusServico4(url, certificado, cfg.TimeOut);
                    }

                    return new NfeStatusServico2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeConsultaProtocolo:

                    if (cfg.IsSvanNFe4())
                    {
                        return new NfeConsulta4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeConsultaProtocolo == VersaoServico.ve400)
                    {
                        return new NfeConsulta4(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.PR & cfg.VersaoNfeConsultaProtocolo == VersaoServico.ve310)
                    {
                        return new NfeConsulta3(url, certificado, cfg.TimeOut);
                    }
                    if (cfg.cUF == Estado.BA & cfg.VersaoNfeConsultaProtocolo == VersaoServico.ve310 &
                        cfg.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeConsulta(url, certificado, cfg.TimeOut);
                    }
                    return new NfeConsulta2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeRecepcao:
                    return new NfeRecepcao2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeRetRecepcao:
                    return new NfeRetRecepcao2(url, certificado, cfg.TimeOut);

                case ServicoNFe.NFeAutorizacao:
                    throw new Exception(string.Format("O serviço {0} não pode ser criado no método {1}!", servico,
                        MethodBase.GetCurrentMethod().Name));

                case ServicoNFe.NFeRetAutorizacao:
                    if (cfg.IsSvanNFe4())
                    {
                        return new NfeRetAutorizacao4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNFeRetAutorizacao == VersaoServico.ve400)
                        return new NfeRetAutorizacao4(url, certificado, cfg.TimeOut);

                    if (cfg.cUF == Estado.PR & cfg.VersaoNFeAutorizacao == VersaoServico.ve310)
                        return new NfeRetAutorizacao3(url, certificado, cfg.TimeOut);
                    return new NfeRetAutorizacao(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeInutilizacao:

                    if (cfg.IsSvanNFe4())
                    {
                        return new NFeInutilizacao4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoNfeStatusServico == VersaoServico.ve400)
                    {
                        return new NFeInutilizacao4(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.cUF == Estado.PR & cfg.VersaoNfeStatusServico == VersaoServico.ve310)
                    {
                        return new NfeInutilizacao3(url, certificado, cfg.TimeOut);
                    }
                    if (cfg.cUF == Estado.BA & cfg.VersaoNfeStatusServico == VersaoServico.ve310 &
                        cfg.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeInutilizacao(url, certificado, cfg.TimeOut);
                    }

                    return new NfeInutilizacao2(url, certificado, cfg.TimeOut);

                case ServicoNFe.RecepcaoEventoCancelmento:
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                    if (cfg.IsSvanNFe4())
                    {
                        return new RecepcaoEvento4SVAN(url, certificado, cfg.TimeOut);
                    }

                    if (cfg.VersaoRecepcaoEventoCceCancelamento == VersaoServico.ve400)
                    {
                        return new RecepcaoEvento4(url, certificado, cfg.TimeOut);
                    }

                    return new RecepcaoEvento(url, certificado, cfg.TimeOut);

                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    {
                        if (cfg.VersaoRecepcaoEventoManifestacaoDestinatario == VersaoServico.ve400)
                        {
                            return new RecepcaoEvento4AN(url, certificado, cfg.TimeOut);
                        }

                        return new RecepcaoEvento(url, certificado, cfg.TimeOut);
                    }

                case ServicoNFe.RecepcaoEventoEpec:
                    return new RecepcaoEPEC(url, certificado, cfg.TimeOut);

                case ServicoNFe.NfeConsultaCadastro:
                    switch (cfg.cUF)
                    {
                        case Estado.CE:
                            return new Wsdl.ConsultaCadastro.CE.CadConsultaCadastro2(url, certificado,
                                cfg.TimeOut);
                    }


                    if (cfg.VersaoNfeConsultaCadastro == VersaoServico.ve400)
                    {
                        return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro4(url, certificado, cfg.TimeOut);
                    }

                    return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro2(url, certificado,
                        cfg.TimeOut);

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