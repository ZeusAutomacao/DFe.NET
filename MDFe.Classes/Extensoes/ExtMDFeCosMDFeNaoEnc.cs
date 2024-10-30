using System.IO;
using System.Xml;
using DFe.Utils;
using MDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
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

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlValidacao, "consMDFeNaoEnc_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlValidacao, "consMDFeNaoEnc_v3.00.xsd");
                    break;
            }
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

            var arquivoSalvar = Path.Combine(caminhoXml, cosMdFeNaoEnc.CNPJ + "-ped-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(cosMdFeNaoEnc, arquivoSalvar);
        }
    }
}