using CTe.Classes.Servicos.Evento;
using DFe.Utils;

namespace CTe.Utils.Extencoes
{
    public static class ExtevPrestDesacordo
    {
        /// <summary>
        ///     Converte o objeto evento para uma string no formato XML
        /// </summary>
        /// <param name="eventoDesacordoOperacao"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto evento</returns>
        public static string ObterXmlString(this evPrestDesacordo evPrestDesacordo)
        {
            return FuncoesXml.ClasseParaXmlString(evPrestDesacordo);
        }
    }
}