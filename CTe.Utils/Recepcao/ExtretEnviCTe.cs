using System.IO;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using DFe.Utils;

namespace CTe.Utils.Recepcao
{
    public static class ExtretEnviCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retEnviCte
        /// </summary>
        /// <param name="retEnviCte"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retEnviCte</returns>
        public static retEnviCte CarregarDeXmlString(this retEnviCte retEnviCte, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retEnviCte>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retEnviCte para uma string no formato XML
        /// </summary>
        /// <param name="retEnviCte"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnviCte</returns>
        public static string ObterXmlString(this retEnviCte retEnviCte)
        {
            return FuncoesXml.ClasseParaXmlString(retEnviCte);
        }

        public static void SalvarXmlEmDisco(this retEnviCte retEnviCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, retEnviCte.infRec.nRec + "-rec.xml");

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }
    }
}