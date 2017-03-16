using System;
using CTeDLL;
using CTeDLL.Classes.Servicos.Status;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtretConsStatServCte
    {
        public static void SalvarXmlEmDisco(this retConsStatServCte retConsStatServCte)
        {
            var instanciaServico = ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + DateTime.Now.ParaDataHoraString() + "-sta.xml";

            FuncoesXml.ClasseParaArquivoXml(retConsStatServCte, arquivoSalvar);
        }
    }
}