using System.Xml;
using DFe.Classes.Extencoes;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaProtocolo;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Utils.Validacao;
using MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;

namespace MDFe.Servicos.ConsultaProtocoloMDFe
{
    public class ServicoMDFeConsultaProtocolo
    {
        public MDFeRetConsSitMDFe MDFeConsultaProtocolo(string chave)
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsulta;
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsulta.GetVersaoString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            var ws = new MDFeConsulta(url, codigoEstado, versao, certificadoDigital, MDFeConfiguracao.VersaoWebService.TimeOut);

            var consSitMdfe = new MDFeConsSitMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsulta,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                XServ = "CONSULTAR",
                ChMDFe = chave
            };

            // converte o objeto para uma string de xml
            var xmlEnvio = FuncoesXml.ClasseParaXmlString(consSitMdfe);

            Validador.Valida(xmlEnvio, "consSitMdfe_v1.00.xsd");

            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlEnvio);

            SalvarArquivoXml(consSitMdfe);

            var retornoXml = ws.mdfeConsultaMDF(dadosRecibo);

            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetConsSitMDFe>(retornoXml.OuterXml);

            SalvarArquivoXmlRetorno(retorno, chave);

            return retorno;
        }

        private void SalvarArquivoXmlRetorno(MDFeRetConsSitMDFe retorno, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + chave + "-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(retorno, arquivoSalvar);
        }

        private void SalvarArquivoXml(MDFeConsSitMDFe consSitMdfe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + consSitMdfe.ChMDFe + "-ped-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(consSitMdfe, arquivoSalvar);
        }
    }
}