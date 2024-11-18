using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeRetRecepcao;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsReciMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsReciMDFe consReciMdFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, consReciMdFe.NRec + "-pro-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(consReciMdFe, arquivoSalvar);
        }
    }
}