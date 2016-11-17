using System.Xml;
using DFe.Classes.Extencoes;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.StatusServico;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeStatusServico;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Utils.Validacao;
using MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;
using MDFe.Wsdl.Gerado.MDFeStatusServico;

namespace MDFe.Servicos.StatusServicoMDFe
{
    public class ServicoMDFeStatusServico
    {
        public MDFeRetConsStatServ MDFeStatusServico()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeStatusServico;
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeStatusServico.GetVersaoString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            var ws = new MDFeStatusServico(url, codigoEstado, versao, certificadoDigital);

            var consStatServMDFe = new MDFeConsStatServMDFe
            {
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeStatusServico,
                XServ = "STATUS"
            };

            // converte o objeto para uma string de xml
            var xmlEnvio = FuncoesXml.ClasseParaXmlString(consStatServMDFe);

            Validador.Valida(xmlEnvio, "consStatServMDFe_v1.00.xsd");

            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlEnvio);

            SalvarArquivoXml(consStatServMDFe);

            var retornoXml = ws.mdfeStatusServicoMDF(dadosRecibo);

            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetConsStatServ>(retornoXml.OuterXml);

            SalvarArquivoXmlRetorno(retorno);

            return retorno;

        }

        private void SalvarArquivoXmlRetorno(MDFeRetConsStatServ retorno)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\-retorno-status-servico.xml";

            FuncoesXml.ClasseParaArquivoXml(retorno, arquivoSalvar);
        }

        private void SalvarArquivoXml(MDFeConsStatServMDFe consReciMdfe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\-resultado-status-servico.xml";

            FuncoesXml.ClasseParaArquivoXml(consReciMdfe, arquivoSalvar);
        }
    }
}