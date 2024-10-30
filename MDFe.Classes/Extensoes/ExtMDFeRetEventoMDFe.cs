using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetEventoMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetEventoMDFe retEvento, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, chave + "-env.xml");

            FuncoesXml.ClasseParaArquivoXml(retEvento, arquivoSalvar);
        }
    }
}