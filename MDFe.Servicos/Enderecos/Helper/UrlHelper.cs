using System;
using DFe.Classes.Flags;

namespace MDFe.Servicos.Enderecos.Helper
{
    public static class UrlHelper
    {
        public static UrlMDFe ObterUrlServico(TipoAmbiente ambiente)
        {
            switch (ambiente)
            {
                    case TipoAmbiente.Producao:
                        return new UrlMDFe
                        {
                            MDFeConsNaoEnc = "https://mdfe.svrs.rs.gov.br/ws/MDFerecepcao/MDFeRecepcao.asmx",
                            MDFeStatusServico = "https://mdfe.svrs.rs.gov.br/ws/MDFeStatusServico/MDFeStatusServico.asmx",
                            MDFeRetRecepcao = "https://mdfe.svrs.rs.gov.br/ws/MDFeRetRecepcao/MDFeRetRecepcao.asmx",
                            MDFeConsulta = "https://mdfe.svrs.rs.gov.br/ws/MDFeConsulta/MDFeConsulta.asmx",
                            MDFeRecepcaoEvento = "https://mdfe.svrs.rs.gov.br/ws/MDFeRecepcaoEvento/MDFeRecepcaoEvento.asmx",
                            MDFeRecepcao = "https://mdfe.svrs.rs.gov.br/ws/MDFerecepcao/MDFeRecepcao.asmx"
                        };
                    case TipoAmbiente.Homologacao:
                        return new UrlMDFe
                        {
                            MDFeConsNaoEnc = "https://mdfe-homologacao.svrs.rs.gov.br/ws/MDFeConsNaoEnc/MDFeConsNaoEnc.asmx",
                            MDFeConsulta = "https://mdfe-homologacao.svrs.rs.gov.br/ws/MDFeConsulta/MDFeConsulta.asmx",
                            MDFeRecepcao = "https://mdfe-homologacao.svrs.rs.gov.br/ws/MDFerecepcao/MDFeRecepcao.asmx",
                            MDFeRecepcaoEvento = "https://mdfe-homologacao.svrs.rs.gov.br/ws/MDFeRecepcaoEvento/MDFeRecepcaoEvento.asmx",
                            MDFeRetRecepcao = "https://mdfe-homologacao.svrs.rs.gov.br/ws/MDFeRetRecepcao/MDFeRetRecepcao.asmx",
                            MDFeStatusServico = "https://mdfe-homologacao.svrs.rs.gov.br/ws/MDFeStatusServico/MDFeStatusServico.asmx"
                        };
            }

            throw new InvalidOperationException("Tipo Ambiente inexistente");
        }
    }
}