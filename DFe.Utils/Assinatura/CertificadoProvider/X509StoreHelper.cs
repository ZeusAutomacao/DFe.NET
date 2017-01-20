using System;
using System.Security.Cryptography.X509Certificates;

namespace DFe.Utils.Assinatura.CertificadoProvider
{
    public static class X509StoreHelper
    {
        public static X509Certificate2 ObterPeloSerial(string serial, OpenFlags openFlags)
        {
            X509Certificate2 certificado = null;

            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(openFlags);

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