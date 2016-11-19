using DFe.Utils;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeStatusServico;
using MDFe.Utils.Configuracoes;

namespace MDFe.Utils.Extencoes
{
    public static class ExtMDFeRetConsStatServ
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsStatServ retConsStatServ)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = caminhoXml + @"\-retorno-status-servico.xml";

            FuncoesXml.ClasseParaArquivoXml(retConsStatServ, arquivoSalvar);
        }
    }
}