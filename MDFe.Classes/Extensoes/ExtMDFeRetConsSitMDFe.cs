using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsSitMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsSitMDFe retConsSitMdFe, string chave, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml,  chave + "-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsSitMdFe, arquivoSalvar);
        }
    }
}