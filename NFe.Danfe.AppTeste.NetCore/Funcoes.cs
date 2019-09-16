using System;
using System.IO;

namespace NFe.Danfe.AppTeste.NetCore
{
    public static class Funcoes
    {
        /// <summary>
        ///     Abre busca de arquivo xml com os dados passados no parâmetro
        /// </summary>
        /// <param name="caminho">Nome e caminho do arquivo mais caminho digitado pelo usuário</param>
        public static string BuscarArquivoXml(string caminho)
        {
            var caminhoArquivo = ArrumaCaminho(caminho);

            if (!caminho.EndsWith(".xml"))
            {
                throw new Exception("Caminho do arquivo precisa terminar com .xml");
            }

            if (!File.Exists(caminho))
            {
                throw new Exception("Caminho do arquivo incorreto! Arquivo não foi encontrado.");
            }

            return File.ReadAllText(caminho);
        }

        /// <summary>
        ///     Gera um arquivo através do caminho do arquivo xml passado anteriormente (.xml)
        /// </summary>
        /// <param name="caminhoXmlAnterior">Nome e caminho do arquivo do xml encontrado anteriormente</param>
        public static void SalvaArquivoGerado(string caminhoXmlAnterior, string extensao, byte[] bytes)
        {
            //caso você chegou até aqui se perguntando de o que fazer com o array de bytes, para exemplos com aspnet core e outras plataformas, de como retornar os bytes para o client:
            //https://github.com/FastReports/FastReport/blob/master/Demos/OpenSource/FastReport.OpenSource.Web.Vue/Controllers/ReportsController.cs

            var caminhoArquivo = Path.GetDirectoryName(ArrumaCaminho(caminhoXmlAnterior)) + "/generated" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + extensao;
            File.WriteAllBytes(caminhoArquivo, bytes);

            Console.Clear();
            Console.WriteLine("Arquivo gerado em: " + caminhoArquivo + " \n Digite algo para continuar...");
            Console.ReadKey();            
        }

        /// <summary>
        /// Arruma o path caso tenha sido escrito incorretamente
        /// </summary>
        /// <param name="caminho">Caminho do arquivo + pastas</param>
        /// <returns></returns>
        private static string ArrumaCaminho(string caminho)
        {
            caminho = caminho.Replace(@"/", @"\");
            caminho = caminho.Replace(@"\\", @"\");
            caminho = caminho.Replace("\"", ""); //remove "
            caminho = caminho.Replace("\'", ""); //remove '
            return caminho;
        }
    }
}
