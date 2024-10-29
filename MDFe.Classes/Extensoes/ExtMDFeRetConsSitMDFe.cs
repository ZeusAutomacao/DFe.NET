using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsSitMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsSitMDFe retConsSitMdFe, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml,  chave + "-sit.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsSitMdFe, arquivoSalvar);
        }
    }
}