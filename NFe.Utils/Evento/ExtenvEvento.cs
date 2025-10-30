using DFe.Utils;
using NFe.Classes.Servicos.Evento;

namespace NFe.Utils.Evento
{
    public static class ExtenvEvento
    {
        /// <summary>
        ///     Recebe um objeto envEvento e devolve a string no formato XML
        /// </summary>
        /// <param name="pedEvento">Objeto do tipo envEvento</param>
        /// <returns>string com XML no do objeto envEvento</returns>
        public static string ObterXmlString(this envEvento pedEvento)
        {
            return FuncoesXml.ClasseParaXmlString(pedEvento);
        }
    }
}