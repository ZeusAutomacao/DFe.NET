using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRetRecepcao;
using MDFe.Utils.Configuracoes;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeRetConsReciMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsReciMDFe consReciMdFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + consReciMdFe.NRec + "-pro-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(consReciMdFe, arquivoSalvar);
        }
    }
}