using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeRecepcao;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetEnviMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetEnviMDFe retEnviMDFe, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, retEnviMDFe.InfRec.NRec + "-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(retEnviMDFe, arquivoSalvar);
        }
    }
}