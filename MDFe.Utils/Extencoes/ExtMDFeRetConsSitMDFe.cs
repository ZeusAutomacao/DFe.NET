using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeConsultaProtocolo;
using MDFe.Utils.Configuracoes;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeRetConsSitMDFe
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsSitMDFe retConsSitMdFe, string chave)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + chave + "-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(retConsSitMdFe, arquivoSalvar);
        }
    }
}