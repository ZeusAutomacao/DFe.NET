using NFe.Classes;

namespace NFe.Utils.NFe
{
    public static class ExtNfeProc
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe nfeProc
        /// </summary>
        /// <param name="nfeProc"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna uma nfeProc carregada com os dados do XML</returns>
        public static nfeProc CarregarDeArquivoXml(this nfeProc nfeProc, string arquivoXml)
        {
            var s = FuncoesXml.ObterNodeDeArquivoXml(typeof(nfeProc).Name, arquivoXml);
            return FuncoesXml.XmlStringParaClasse<nfeProc>(s);
        }

        /// <summary>
        ///     Converte o objeto nfeProc para uma string no formato XML
        /// </summary>
        /// <param name="nfeProc"></param>
        /// <returns>Retorna uma string no formato XML com os dados da nfeProc</returns>
        public static string ObterXmlString(this nfeProc nfeProc)
        {
            return FuncoesXml.ClasseParaXmlString(nfeProc);
        }

        /// <summary>
        ///     Coverte uma string XML no formato nfeProc para um objeto nfeProc
        /// </summary>
        /// <param name="nfeProc"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo nfeProc</returns>
        public static nfeProc CarregarDeXmlString(this nfeProc nfeProc, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(nfeProc).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<nfeProc>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto nfeProc em um arquivo XML
        /// </summary>
        /// <param name="nfeProc">Objeto nfeProc</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this nfeProc nfeProc, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(nfeProc, arquivoXml);
        }
    }
}
