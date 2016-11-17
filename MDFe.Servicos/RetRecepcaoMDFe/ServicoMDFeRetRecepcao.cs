using System.Xml;
using DFe.Classes.Extencoes;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.RetRecepcao;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRetRecepcao;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Extencoes;
using MDFe.Utils.Validacao;
using MDFe.Wsdl.Gerado.MDFeRetRecepcao;

namespace MDFe.Servicos.RetRecepcaoMDFe
{
    public class ServicoMDFeRetRecepcao
    {
        public MDFeRetConsReciMDFe MDFeRetRecepcao(string numeroRecibo)
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRetRecepcao;
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfDestino.GetCodigoIbgeEmString();
            var versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRetRecepcao.GetVersaoString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            var ws = new MDFeRetRecepcao(url, codigoEstado, versao, certificadoDigital);

            var consReciMdfe = new MDFeConsReciMDFe
            {
                Versao = MDFeConfiguracao.VersaoWebService.VersaoMDFeRetRecepcao,
                TpAmb = MDFeConfiguracao.VersaoWebService.TipoAmbiente,
                NRec = numeroRecibo
            };


            // converte o objeto para uma string de xml
            var xmlEnvio = FuncoesXml.ClasseParaXmlString(consReciMdfe);

            Validador.Valida(xmlEnvio, "consReciMdfe_v1.00.xsd");

            var dadosRecibo = new XmlDocument();
            dadosRecibo.LoadXml(xmlEnvio);

            SalvarArquivoXml(consReciMdfe);

            var retornoXml = ws.mdfeRetRecepcao(dadosRecibo);

            var retorno = FuncoesXml.XmlStringParaClasse<MDFeRetConsReciMDFe>(retornoXml.OuterXml);

            SalvarArquivoXmlRetorno(retorno);

            return retorno;
        }

        private void SalvarArquivoXmlRetorno(MDFeRetConsReciMDFe retorno)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + retorno.NRec + "-pro-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(retorno, arquivoSalvar);
        }

        private void SalvarArquivoXml(MDFeConsReciMDFe envio)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + envio.NRec + "-ped-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(envio, arquivoSalvar);
        }
    }
}