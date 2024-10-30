using System;
using System.Security.Cryptography.X509Certificates;
using DFe.Utils;
using NFe.Classes.Servicos.Inutilizacao;
using NFe.Utils.Assinatura;

namespace NFe.Utils.Inutilizacao
{
    public static class ExtinutNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto inutNFe
        /// </summary>
        /// <param name="inutNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo inutNFe</returns>
        public static inutNFe CarregarDeXmlString(this inutNFe inutNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<inutNFe>(xmlString);
        }
        
        /// <summary>
        ///     Converte o objeto inutNFe para uma string no formato XML
        /// </summary>
        /// <param name="pedInutilizacao"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto inutNFe</returns>
        public static string ObterXmlString(this inutNFe pedInutilizacao)
        {
            return FuncoesXml.ClasseParaXmlString(pedInutilizacao);
        }

        /// <summary>
        ///     Assina um objeto inutNFe
        /// </summary>
        /// <param name="inutNFe"></param>
        /// <param name="certificadoDigital">Informe o certificado digital, se já possuir esse em cache, evitando novo acesso ao certificado</param>
        /// <returns>Retorna um objeto do tipo inutNFe assinado</returns>
        public static inutNFe Assina(this inutNFe inutNFe, X509Certificate2 certificadoDigital, string signatureMethodSignedXml = "http://www.w3.org/2000/09/xmldsig#rsa-sha1", string digestMethodReference = "http://www.w3.org/2000/09/xmldsig#sha1", bool removerAcentos = false)
        {
            var inutNFeLocal = inutNFe;
            if (inutNFeLocal.infInut.Id == null)
                throw new Exception("Não é possível assinar um onjeto inutNFe sem sua respectiva Id!");

            var assinatura = Assinador.ObterAssinatura(inutNFeLocal, inutNFeLocal.infInut.Id, certificadoDigital, false, signatureMethodSignedXml, digestMethodReference, removerAcentos);
            inutNFeLocal.Signature = assinatura;
            return inutNFeLocal;
        }
    }
}