using DFe.Utils;
using NFe.Classes.Servicos.DistribuicaoDFe;

namespace NFe.Utils.DistribuicaoDFe
{
    public static class ExtdistDFeInt
    {

        /// <summary>
        /// Recebe um objeto ExtdistDFeInt e devolve a string no formato XML
        /// </summary>
        /// <param name="pedDistDFeInt">Objeto do Tipo distDFeInt</param>
        /// <returns>string com XML no do objeto distDFeInt</returns>
        public static string ObterXmlString(this distDFeInt pedDistDFeInt)
        {
            return FuncoesXml.ClasseParaXmlString(pedDistDFeInt);
        }
    }
}