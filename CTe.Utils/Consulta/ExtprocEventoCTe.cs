using CTeDLL.Classes.Servicos.Consulta;
using DFe.Utils;

namespace CTeDLL.Utils.Consulta
{
    public static class ExtprocEventoCTe
    {
        /// <summary>
        ///     Converte o objeto procEventoCTe para uma string no formato XML
        /// </summary>
        /// <param name="procEventoCTe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto procEventoCTe</returns>
        public static string ObterXmlString(this procEventoCTe procEventoCTe)
        {
            return FuncoesXml.ClasseParaXmlString(procEventoCTe);
        }

        /// <summary>
        ///     Coverte uma string XML no formato procEventoCTe para um objeto procEventoCTe
        /// </summary>
        /// <param name="procEventoCTe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo procEventoNFe</returns>
        public static procEventoCTe CarregarDeXmlString(this procEventoCTe procEventoCTe, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(procEventoCTe).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<procEventoCTe>(s);
        }
    }
}