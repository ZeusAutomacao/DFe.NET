using System;
using System.Security.Cryptography.X509Certificates;

namespace DFe.CertificadosDigitais.Servicos
{
    public class ProxyBase
    {
        protected X509Store ObterX509Store(OpenFlags openFlags)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(openFlags);
            return store;
        }

        protected X509Certificate2 ObterDoRepositorio(string serial, OpenFlags opcoesDeAbertura)
        {
            if (string.IsNullOrEmpty(serial))
                throw new ArgumentException("O número de série do certificado digital não foi informado!");

            X509Certificate2 certificado = null;

            var store = ObterX509Store(opcoesDeAbertura);

            try
            {
                foreach (var item in store.Certificates)
                {
                    if (item.SerialNumber != null && item.SerialNumber.ToUpper().Equals(serial.ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                        certificado = item;
                }

                if (certificado == null)
                    throw new Exception(string.Format("Certificado digital nº {0} não encontrado!", serial.ToUpper()));
            }
            finally
            {
                store.Close();
            }

            return certificado;
        }
    }
}