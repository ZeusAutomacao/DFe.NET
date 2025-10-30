using System.IO;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using DFe.Utils;

namespace CTe.Utils.Evento
{
    public static class ExtretEnvEvento
    {
        /// <summary>
        ///     Carrega um objeto do tipo retEnvEvento a partir de uma string no formato XML
        /// </summary>
        /// <param name="retEnvEvento"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto retEnvEvento com as informações da string XML</returns>
        public static retEnvEvento CarregarDeXmlString(this retEnvEvento retEnvEvento, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retEnvEvento>(xmlString);
        }

        /// <summary>
        ///     Converte um objeto do tipo retEnvEvento para uma string no formato XML com os dados do objeto
        /// </summary>
        /// <param name="retEnvEvento"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retEnvEvento</returns>
        public static string ObterXmlString(this retEnvEvento retEnvEvento)
        {
            return FuncoesXml.ClasseParaXmlString(retEnvEvento);
        }

        public static void SalvarXmlEmDisco(this retEventoCTe retEnviCte, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, retEnviCte.infEvento.chCTe + "-eve.xml");

            FuncoesXml.ClasseParaArquivoXml(retEnviCte, arquivoSalvar);
        }
    }
}