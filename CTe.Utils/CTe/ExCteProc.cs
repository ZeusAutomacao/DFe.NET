using System.IO;
using CTe.Classes;
using DFe.Utils;
using cteProc = CTe.Classes.cteProc;

namespace CTe.Utils.CTe
{
    public static class ExtCteProc
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe cteProc
        /// </summary>
        /// <param name="cteProc"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna um cteProc carregada com os dados do XML</returns>
        public static cteProc CarregarDeArquivoXml(this cteProc cteProc, string arquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<cteProc>(arquivoXml);
        }

        /// <summary>
        ///     Converte o objeto cteProc para uma string no formato XML
        /// </summary>
        /// <param name="cteProc"></param>
        /// <returns>Retorna uma string no formato XML com os dados do cteProc</returns>
        public static string ObterXmlString(this cteProc cteProc)
        {
            return FuncoesXml.ClasseParaXmlString(cteProc);
        }

        /// <summary>
        ///     Coverte uma string XML no formato cteProc para um objeto cteProc
        /// </summary>
        /// <param name="cteProc"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo cteProc</returns>
        public static cteProc CarregarDeXmlString(this cteProc cteProc, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(cteProc).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<cteProc>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto cteProc em um arquivo XML
        /// </summary>
        /// <param name="cteProc">Objeto cteProc</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this cteProc cteProc, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(cteProc, arquivoXml);
        }

        public static void SalvarXmlEmDisco(this cteProc cteProc, ConfiguracaoServico configuracaoServico = null)
        {
            if (cteProc == null) return;

            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cteProc.CTe.Chave() + "-cteproc.xml");

            FuncoesXml.ClasseParaArquivoXml(cteProc, arquivoSalvar);
        }
    }
}