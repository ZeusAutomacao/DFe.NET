namespace NFe.Utils.Enderecos
{
    public class CearaUrl : IZeusEnderecosUrls
    {
        public CearaUrl()
        {
            CartaCorrecaoCancelamentoHomologacao_VersaoUm = "https://nfeh.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl";
            NfeRecepcaoHomologacao_VersaoDois = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl";
            NfeRetRecepcaoHomologacao_VersaoDois = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl";
            NfeInutilizacaoHomologacao_VersaoDoisETres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl";
            NfeConsultaProtocoloHomologacao_VersaoDoisETres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl";
            NfeStatusServicoHomologacao_VersaoDoisETres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl";
            NfeConsultaCadastroHomologacao_VersaoDoisETres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl";
            NfeDownloadNFHomologacao_VersaoDoisETres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl";
            NFeAutorizacaoHomologacao_VersaoTres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl";
            NFeRetAutorizacaoHomologacao_VersaoTres = "https://nfeh.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl";
            CartaCorrecaoCancelamentoHomologacao_VersaoQuatro =
                "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?wsdl";
            NfeInutilizacaoHomologacao_VersaoQuatro =
                "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeInutilizacao4?wsdl";
            NfeConsultaProtocoloHomologacao_VersaoQuatro =
                "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?wsdl";
            NfeStatusServicoHomologacao_VersaoQuatro =
                "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeStatusServico4?wsdl";
            NFeAutorizacaoHomologacao_VersaoQuatro = "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?wsdl";
            NFeRetAutorizacaoHomologacao_VersaoQuatro =
                "https://nfeh.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?wsdl";
            CartaCorrecaoCancelamentoProducao_VersaoUm =
                "https://nfe.sefaz.ce.gov.br/nfe2/services/RecepcaoEvento?wsdl";
            NfeRecepcaoProducao_VersaoDois = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRecepcao2?wsdl";
            NfeRetRecepcaoProducao_VersaoDois = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetRecepcao2?wsdl";
            NfeInutilizacaoProducao_VersaoDoisETres = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeInutilizacao2?wsdl";
            NfeConsultaProtocoloProducao_VersaoDoisETres =
                "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeConsulta2?wsdl";
            NfeStatusServicoProducao_VersaoDoisETres =
                "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeStatusServico2?wsdl";
            NfeConsultaCadastroProducao_VersaoDoisETres =
                "https://nfe.sefaz.ce.gov.br/nfe2/services/CadConsultaCadastro2?wsdl";
            NfeDownloadNFProducao_VersaoDoisETres = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeDownloadNF?wsdl";
            NFeAutorizacaoProducao_VersaoTres = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeAutorizacao?wsdl";
            NFeRetAutorizacaoProducao_VersaoTres = "https://nfe.sefaz.ce.gov.br/nfe2/services/NfeRetAutorizacao?wsdl";
            NfeInutilizacaoProducao_VersaoQuatro = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeInutilizacao4?wsdl";
            NfeConsultaProtocoloProducao_VersaoQuatro =
                "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeConsultaProtocolo4?wsdl";
            NfeStatusServicoProducao_VersaoQuatro = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeStatusServico4?wsdl";
            NfeConsultaCadastroProducao_VersaoQuatro =
                "https://nfe.sefaz.ce.gov.br/nfe4/services/CadConsultaCadastro4?wsdl";
            CartaCorrecaoCancelamentoProducao_VersaoQuatro =
                "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRecepcaoEvento4?wsdl";
            NFeAutorizacaoProducao_VersaoQuatro = "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeAutorizacao4?wsdl";
            NFeRetAutorizacaoProducao_VersaoQuatro =
                "https://nfe.sefaz.ce.gov.br/nfe4/services/NFeRetAutorizacao4?wsdl";
        }

        public string CartaCorrecaoCancelamentoHomologacao_VersaoUm { get; }
        public string NfeRecepcaoHomologacao_VersaoDois { get; }
        public string NfeRetRecepcaoHomologacao_VersaoDois { get; }
        public string NfeInutilizacaoHomologacao_VersaoDoisETres { get; }
        public string NfeConsultaProtocoloHomologacao_VersaoDoisETres { get; }
        public string NfeStatusServicoHomologacao_VersaoDoisETres { get; }
        public string NfeConsultaCadastroHomologacao_VersaoDoisETres { get; }
        public string NfeDownloadNFHomologacao_VersaoDoisETres { get; }
        public string NFeAutorizacaoHomologacao_VersaoTres { get; }
        public string NFeRetAutorizacaoHomologacao_VersaoTres { get; }
        public string CartaCorrecaoCancelamentoHomologacao_VersaoQuatro { get; }
        public string NfeInutilizacaoHomologacao_VersaoQuatro { get; }
        public string NfeConsultaProtocoloHomologacao_VersaoQuatro { get; }
        public string NfeStatusServicoHomologacao_VersaoQuatro { get; }
        public string NFeAutorizacaoHomologacao_VersaoQuatro { get; }
        public string NFeRetAutorizacaoHomologacao_VersaoQuatro { get; }

        public string CartaCorrecaoCancelamentoProducao_VersaoUm { get; }
        public string NfeRecepcaoProducao_VersaoDois { get; }
        public string NfeRetRecepcaoProducao_VersaoDois { get; }
        public string NfeInutilizacaoProducao_VersaoDoisETres { get; }
        public string NfeConsultaProtocoloProducao_VersaoDoisETres { get; }
        public string NfeStatusServicoProducao_VersaoDoisETres { get; }
        public string NfeConsultaCadastroProducao_VersaoDoisETres { get; }
        public string NfeDownloadNFProducao_VersaoDoisETres { get; }
        public string NFeAutorizacaoProducao_VersaoTres { get; }
        public string NFeRetAutorizacaoProducao_VersaoTres { get; }
        public string NfeInutilizacaoProducao_VersaoQuatro { get; }
        public string NfeConsultaProtocoloProducao_VersaoQuatro { get; }
        public string NfeStatusServicoProducao_VersaoQuatro { get; }
        public string NfeConsultaCadastroProducao_VersaoQuatro { get; }
        public string CartaCorrecaoCancelamentoProducao_VersaoQuatro { get; }
        public string NFeAutorizacaoProducao_VersaoQuatro { get; }
        public string NFeRetAutorizacaoProducao_VersaoQuatro { get; }
    }
}