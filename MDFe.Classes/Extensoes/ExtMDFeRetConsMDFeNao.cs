using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsMDFeNao
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsMDFeNao retConsMdFeNao, string cnpj)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cnpj + "-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsMdFeNao, arquivoSalvar);
        }
    }
}