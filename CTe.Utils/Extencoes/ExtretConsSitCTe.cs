using CTeDLL;
using CTeDLL.Classes.Servicos.Consulta;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtretConsSitCTe
    {
        /// <summary>
        ///     Coverte uma string XML no formato CTe para um objeto retConsSitCTe
        /// </summary>
        /// <param name="retConsSitCTe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retConsSitNFe</returns>
        public static retConsSitCTe CarregarDeXmlString(this retConsSitCTe retConsSitCTe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retConsSitCTe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retConsSitCTe para uma string no formato XML
        /// </summary>
        /// <param name="retConsSitCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retConsSitCTe</returns>
        public static string ObterXmlString(this retConsSitCTe retConsSitCTe)
        {
            return FuncoesXml.ClasseParaXmlString(retConsSitCTe);
        }

        public static void SalvarXmlEmDisco(this retConsSitCTe retConsSitCTe, string chave)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            if (configuracaoServico.NaoSalvarXml()) return;

            var caminhoXml = configuracaoServico.DiretorioSalvarXml;

            var arquivoSalvar = caminhoXml + @"\" + chave + "-sit.xml";

            FuncoesXml.ClasseParaArquivoXml(retConsSitCTe, arquivoSalvar);
        }
    }
}