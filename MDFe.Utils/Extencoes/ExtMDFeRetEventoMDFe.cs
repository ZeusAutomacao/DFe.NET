using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento;
using MDFe.Utils.Configuracoes;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeRetEventoMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetEventoMDFe retEvento, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + chave + "-env.xml";

            FuncoesXml.ClasseParaArquivoXml(retEvento, arquivoSalvar);
        }
    }
}