using System;
using CTeDLL.Classes.Servicos.Inutilizacao;
using DFe.Utils;

namespace CTeDLL.Utils.Inutilizacao
{
    public static class ExtinutCTe
    {
        /// <summary>
        ///     Converte o objeto inutCTe para uma string no formato XML
        /// </summary>
        /// <param name="pedInutilizacao"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto inutCTe</returns>
        public static string ObterXmlString(this inutCTe pedInutilizacao)
        {
            return FuncoesXml.ClasseParaXmlString(pedInutilizacao);
        }

        /// <summary>
        ///     Assina um objeto inutCTe
        /// </summary>
        /// <param name="inutCTe"></param>
        /// <returns>Retorna um objeto do tipo inutCTe assinado</returns>
        public static inutCTe Assina(this inutCTe inutCTe)
        {
            var inutCTeLocal = inutCTe;
            if (inutCTeLocal.infInut.Id == null)
                throw new Exception("Não é possível assinar um onjeto inutNFe sem sua respectiva Id!");

            // todo
            /*var assinatura = Assinador.ObterAssinatura(inutCTeLocal, inutCTeLocal.infInut.Id);
            inutCTeLocal.Signature = assinatura;*/
            return inutCTeLocal;
        }
    }
}