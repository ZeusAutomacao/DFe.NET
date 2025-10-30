using System.IO;
using CTe.Classes;
using DFe.Utils;
using cteOSProc = CTe.Classes.cteOSProc;

namespace CTe.Utils.CTe
{
    public static class ExtCteOSProc
    {
        /// <summary>
        ///     Carrega um arquivo XML para um objeto da classe cteOSProc
        /// </summary>
        /// <param name="cteOSProc"></param>
        /// <param name="arquivoXml">arquivo XML</param>
        /// <returns>Retorna um cteOSProc carregada com os dados do XML</returns>
        public static cteOSProc CarregarDeArquivoXml(this cteOSProc cteOSProc, string arquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<cteOSProc>(arquivoXml);
        }

        /// <summary>
        ///     Converte o objeto cteOSProc para uma string no formato XML
        /// </summary>
        /// <param name="cteOSProc"></param>
        /// <returns>Retorna uma string no formato XML com os dados do cteOSProc</returns>
        public static string ObterXmlString(this cteOSProc cteOSProc)
        {
            return FuncoesXml.ClasseParaXmlString(cteOSProc);
        }

        /// <summary>
        ///     Coverte uma string XML no formato cteOSProc para um objeto cteOSProc
        /// </summary>
        /// <param name="cteOSProc"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo cteOSProc</returns>
        public static cteOSProc CarregarDeXmlString(this cteOSProc cteOSProc, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(cteOSProc).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<cteOSProc>(s);
        }

        /// <summary>
        ///     Grava os dados do objeto cteOSProc em um arquivo XML
        /// </summary>
        /// <param name="cteOSProc">Objeto cteOSProc</param>
        /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
        public static void SalvarArquivoXml(this cteOSProc cteOSProc, string arquivoXml)
        {
            FuncoesXml.ClasseParaArquivoXml(cteOSProc, arquivoXml);
        }

        public static void SalvarXmlEmDisco(this cteOSProc cteOSProc, ConfiguracaoServico configuracaoServico = null)
        {
            if (cteOSProc == null) return;

            var instanciaServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            if (instanciaServico.NaoSalvarXml()) return;

            var caminhoXml = instanciaServico.DiretorioSalvarXml;

            var arquivoSalvar = Path.Combine(caminhoXml, cteOSProc.CTeOS.Chave() + "-cteOSProc.xml");

            FuncoesXml.ClasseParaArquivoXml(cteOSProc, arquivoSalvar);
        }
    }
}