using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsMDFeNao
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsMDFeNao retConsMdFeNao, string cnpj, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cnpj + "-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsMdFeNao, arquivoSalvar);
        }
    }
}