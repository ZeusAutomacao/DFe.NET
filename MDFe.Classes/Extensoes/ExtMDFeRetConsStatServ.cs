using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeStatusServico;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsStatServ
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsStatServ retConsStatServ, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            if (config.NaoSalvarXml()) return;

            var caminhoXml = config.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, "-retorno-status-servico.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsStatServ, arquivoSalvar);
        }
    }
}