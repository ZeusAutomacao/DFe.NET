using System.IO;
using DFe.Utils;
using MDFe.Classes.Retorno.MDFeStatusServico;
using MDFe.Utils.Configuracoes;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeRetConsStatServ
    {
        public static void SalvarXmlEmDisco(this MDFeRetConsStatServ retConsStatServ)
        {
            if (MDFeConfiguracao.NaoSalvarXml()) return;

            var caminhoXml = MDFeConfiguracao.CaminhoSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, "-retorno-status-servico.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsStatServ, arquivoSalvar);
        }
    }
}