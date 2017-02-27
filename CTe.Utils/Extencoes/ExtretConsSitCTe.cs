using CTeDLL;
using CTeDLL.Classes.Servicos.Consulta;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtretConsSitCTe
    {
        public static void SalvarXmlEmDisco(this retConsSitCTe retConsSitCTe, string chave)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            if (configuracaoServico.NaoSalvarXml()) return;

            var caminhoXml = configuracaoServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + chave + "-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(retConsSitCTe, arquivoSalvar);
        }
    }
}