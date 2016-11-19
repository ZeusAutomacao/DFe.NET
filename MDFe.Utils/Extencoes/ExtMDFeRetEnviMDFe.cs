using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRecepcao;
using MDFe.Utils.Configuracoes;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeRetEnviMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetEnviMDFe retEnviMDFe)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + retEnviMDFe.InfRec.NRec + "-rec.xml";

            FuncoesXml.ClasseParaArquivoXml(retEnviMDFe, arquivoSalvar);
        }
    }
}