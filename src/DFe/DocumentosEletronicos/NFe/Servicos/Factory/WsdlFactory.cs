using System;
using System.Reflection;
using DFe.CertificadosDigitais;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.NFe.Comunicacao.NFeStatusServico;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Flags;
using DFe.DocumentosEletronicos.NFe.Wsdl;
using DFe.DocumentosEletronicos.NFe.Wsdl.AdmCsc;
using DFe.DocumentosEletronicos.NFe.Wsdl.Autorizacao;
using DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.NFe.Wsdl.ConsultaCadastro.CE;
using DFe.DocumentosEletronicos.NFe.Wsdl.ConsultaProtocolo;
using DFe.DocumentosEletronicos.NFe.Wsdl.DistribuicaoDFe;
using DFe.DocumentosEletronicos.NFe.Wsdl.Download;
using DFe.DocumentosEletronicos.NFe.Wsdl.Enderecos;
using DFe.DocumentosEletronicos.NFe.Wsdl.Evento;
using DFe.DocumentosEletronicos.NFe.Wsdl.Inutilizacao;
using DFe.DocumentosEletronicos.NFe.Wsdl.Recepcao;
using DFe.DocumentosEletronicos.NFe.Wsdl.Status;

namespace DFe.DocumentosEletronicos.NFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static INfeServico CriarServico(ServicoNFe servico, NFeBaseConfig nFeBaseConfig, CertificadoDigital certificadoDigital)
        {
            var url = Enderecador.ObterUrlServico(nFeBaseConfig);
            var certX509Implementacao = certificadoDigital.ObterCertificadoDigital();

            switch (servico)
            {
                case ServicoNFe.NfeStatusServico:
                    if (nFeBaseConfig.VersaoNfeStatusServico == VersaoServico.Versao400)
                    {
                        return new NfeStatusServico4(new WsdlConfiguracao
                        {
                            CertificadoDigital = certX509Implementacao,
                            EstadoUF = nFeBaseConfig.EstadoUf,
                            TimeOut = nFeBaseConfig.TimeOut,
                            Url = url,
                            VersaoLayout = nFeBaseConfig.VersaoNfeStatusServico,
                    });
                    }
                    if (nFeBaseConfig.EstadoUf == Estado.PR & nFeBaseConfig.VersaoNfeStatusServico == VersaoServico.Versao310)
                    {
                        return new NfeStatusServico3(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    }
                    if (nFeBaseConfig.EstadoUf == Estado.BA & nFeBaseConfig.VersaoNfeStatusServico == VersaoServico.Versao310 &
                        nFeBaseConfig.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeStatusServico(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    }
                    return new NfeStatusServico2(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NfeConsultaProtocolo:
                    if (nFeBaseConfig.EstadoUf == Estado.PR & nFeBaseConfig.VersaoNfeConsultaProtocolo == VersaoServico.Versao310)
                    {
                        return new NfeConsulta3(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    }
                    if (nFeBaseConfig.EstadoUf == Estado.BA & nFeBaseConfig.VersaoNfeConsultaProtocolo == VersaoServico.Versao310 &
                        nFeBaseConfig.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeConsulta(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    }
                    return new NfeConsulta2(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NfeRecepcao:
                    return new NfeRecepcao2(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NfeRetRecepcao:
                    return new NfeRetRecepcao2(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NFeAutorizacao:
                    throw new Exception(string.Format("O serviço {0} não pode ser criado no método {1}!", servico,
                        MethodBase.GetCurrentMethod().Name));

                case ServicoNFe.NFeRetAutorizacao:
                    if (nFeBaseConfig.EstadoUf == Estado.PR & nFeBaseConfig.VersaoNFeAutorizacao == VersaoServico.Versao310)
                        return new NfeRetAutorizacao3(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    return new NfeRetAutorizacao(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NfeInutilizacao:
                    if (nFeBaseConfig.EstadoUf == Estado.PR & nFeBaseConfig.VersaoNfeStatusServico == VersaoServico.Versao310)
                    {
                        return new NfeInutilizacao3(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    }
                    if (nFeBaseConfig.EstadoUf == Estado.BA & nFeBaseConfig.VersaoNfeStatusServico == VersaoServico.Versao310 &
                        nFeBaseConfig.ModeloDocumento == ModeloDocumento.NFe)
                    {
                        return new NfeInutilizacao(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                    }
                    return new NfeInutilizacao2(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.RecepcaoEventoCancelmento:
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    return new RecepcaoEvento(url, certX509Implementacao, nFeBaseConfig.TimeOut);
                case ServicoNFe.RecepcaoEventoEpec:
                    return new RecepcaoEPEC(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NfeConsultaCadastro:
                    switch (nFeBaseConfig.EstadoUf)
                    {
                        case Estado.CE:
                            return new CadConsultaCadastro2(url, certX509Implementacao,
                                nFeBaseConfig.TimeOut);
                    }
                    return new Wsdl.ConsultaCadastro.DEMAIS_UFs.CadConsultaCadastro2(url, certX509Implementacao,
                        nFeBaseConfig.TimeOut);

                case ServicoNFe.NfeDownloadNF:
                    return new NfeDownloadNF(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NfceAdministracaoCSC:
                    return new NfceCsc(url, certX509Implementacao, nFeBaseConfig.TimeOut);

                case ServicoNFe.NFeDistribuicaoDFe:
                    return new NfeDistDFeInteresse(url, certX509Implementacao, nFeBaseConfig.TimeOut);

            }

            return null;
        }

        public static INfeServicoAutorizacao CriarServicoAutorizacao(ServicoNFe servico, NFeBaseConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = Enderecador.ObterUrlServico(dfeConfig);

            var certX509Implementacao = certificadoDigital.ObterCertificadoDigital();

            if (servico != ServicoNFe.NFeAutorizacao)
                throw new Exception(
                    string.Format("O serviço {0} não pode ser criado no método {1}!", servico,
                        MethodBase.GetCurrentMethod().Name));

            if (dfeConfig.EstadoUf == Estado.PR & dfeConfig.VersaoNFeAutorizacao == VersaoServico.Versao310)
                return new NfeAutorizacao3(url, certX509Implementacao, dfeConfig.TimeOut);

            return new NfeAutorizacao(url, certX509Implementacao, dfeConfig.TimeOut);
        }
    }
}