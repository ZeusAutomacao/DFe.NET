using System.Xml;
using DFe.Classes.Extencoes;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaNaoEncerrados;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Utils.Validacao;
using MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;

namespace MDFe.Servicos.ConsultaNaoEncerradosMDFe
{
    public class ServicoMDFeConsultaNaoEncerrados
    {
        public MDFeRetConsMDFeNao MDFeConsultaNaoEncerrados(string cnpj)
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsNaoEnc;
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsNaoEnc.GetVersaoString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            var ws = new MDFeConsNaoEnc(url, codigoEstado, versao, certificadoDigital);

            var consMDFeNaoEnc = new MDFeCosMDFeNaoEnc
            {
                CNPJ = cnpj,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeConsNaoEnc,
                XServ = "CONSULTAR NÃO ENCERRADOS"
            };

            // converte o objeto para uma string de xml
            var xmlEnvio = FuncoesXml.ClasseParaXmlString(consMDFeNaoEnc);

            Validador.Valida(xmlEnvio, "consMDFeNaoEnc_v1.00.xsd");

            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlEnvio);

            SalvarArquivoXml(consMDFeNaoEnc);

            var retornoXml = ws.mdfeConsNaoEnc(dadosRecibo);

            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetConsMDFeNao>(retornoXml.OuterXml);

            SalvarArquivoXmlRetorno(retorno, cnpj);

            return retorno;
        }

        private void SalvarArquivoXmlRetorno(MDFeRetConsMDFeNao retorno, string cnpj)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + cnpj + "-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(retorno, arquivoSalvar);
        }

        private void SalvarArquivoXml(MDFeCosMDFeNaoEnc consMdFeNaoEnc)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + consMdFeNaoEnc.CNPJ + "-ped-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(consMdFeNaoEnc, arquivoSalvar);
        }
    }
}