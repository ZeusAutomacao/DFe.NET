using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeRecepcao;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetEnviMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetEnviMDFe retEnviMDFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, retEnviMDFe.InfRec.NRec + "-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(retEnviMDFe, arquivoSalvar);
        }
    }
}