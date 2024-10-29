using System;
using System.IO;
using CTe.Classes;
using CTe.Classes.Servicos.Status;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtretConsStatServCte
    {
        public static void SalvarXmlEmDisco(this retConsStatServCte retConsStatServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, DateTime.Now.ParaDataHoraString() + "-sta.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsStatServCte, arquivoSalvar);
        }
    }

    public static class ExtretConsStatServCTe
    {
        public static void SalvarXmlEmDisco(this retConsStatServCTe retConsStatServCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, DateTime.Now.ParaDataHoraString() + "-sta.xml");

            FuncoesXml.ClasseParaArquivoXml(retConsStatServCte, arquivoSalvar);
        }
    }
}