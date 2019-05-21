using System;
using System.Security.Cryptography.X509Certificates;
using DFe.Utils.Assinatura;

namespace DFe.Utils
{
    public class CertificadoDigitalUtils
    {
        /// <summary>
        /// Exibe a lista de certificados instalados no PC e devolve o certificado selecionado
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2 ListareObterDoRepositorio()
        {
            var store = CertificadoDigital.ObterX509Store(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
            var collection = store.Certificates;
            var fcollection = collection.Find(X509FindType.FindByTimeValid, DateTime.Now, true);
            var scollection = X509Certificate2UI.SelectFromCollection(fcollection, "Certificados válidos:", "Selecione o certificado que deseja usar",
                X509SelectionFlag.SingleSelection);

            if (scollection.Count == 0)
            {
                throw new Exception("Nenhum certificado foi selecionado!");
            }

            store.Close();
            return scollection[0];
        }
    }
}