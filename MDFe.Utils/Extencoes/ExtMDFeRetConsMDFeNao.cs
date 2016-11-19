using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Utils.Configuracoes;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeRetConsMDFeNao
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsMDFeNao retConsMdFeNao, string cnpj)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + cnpj + "-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(retConsMdFeNao, arquivoSalvar);
        }
    }
}