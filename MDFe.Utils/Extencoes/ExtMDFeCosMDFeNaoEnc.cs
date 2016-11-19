using System.Xml;
using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaNaoEncerrados;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Validacao;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeCosMDFeNaoEnc
    {
        public static string XmlString(this MDFeCosMDFeNaoEnc consMDFeNaoEnc)
        {
            return FuncoesXml.ClasseParaXmlString(consMDFeNaoEnc);
        }

        public static void ValidarSchema(this MDFeCosMDFeNaoEnc consMdFeNaoEnc)
        {
            var xmlValidacao = consMdFeNaoEnc.XmlString();
            Validador.Valida(xmlValidacao, "consMDFeNaoEnc_v1.00.xsd");
        }

        public static XmlDocument CriaRequestWs(this MDFeCosMDFeNaoEnc cosMdFeNaoEnc)
        {
            var request = new XmlDocument();
            request.LoadXml(cosMdFeNaoEnc.XmlString());

            return request;
        }

        public static void SalvarXmlEmDisco(this MDFeCosMDFeNaoEnc cosMdFeNaoEnc)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + cosMdFeNaoEnc.CNPJ + "-ped-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(cosMdFeNaoEnc, arquivoSalvar);
        }
    }
}