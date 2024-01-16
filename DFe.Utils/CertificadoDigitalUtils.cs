using System;
using System.Security.Cryptography.X509Certificates;
#if !(NETSTANDARD || NETCOREAPP)
using DFe.Utils.Assinatura;
#endif
using System.Security;
using System.IO;

namespace DFe.Utils
{
    public class CertificadoDigitalUtils
    {
#if !(NETSTANDARD || NETCOREAPP)

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
#endif

        /// <summary>
        /// Retorna o certificado que está no caminho especificado
        /// </summary>
        /// <param name="caminho">Caminho do certificado (.pfx)</param>
        /// <param name="password">string representando a senha do certificado</param>
        public static X509Certificate2 ObterDoCaminho(string caminho, string password)
        {
            caminho = ArrumaCaminho(caminho);
            SecureString stringSegura = null;
            try
            {
                stringSegura = new SecureString();
                if ((password.Length > 0))
                {
                    foreach (Char caractere in password.ToCharArray())
                    {
                        stringSegura.AppendChar(caractere);
                    }
                }
                return ObterDoCaminho(caminho, stringSegura);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o certificado que está no caminho especificado
        /// </summary>
        /// <param name="caminho">Caminho do certificado (.pfx)</param>
        /// <param name="password">SecureString representando a senha do certificado</param>
        /// <returns></returns>
        public static X509Certificate2 ObterDoCaminho(string caminho, SecureString password)
        {
            caminho = ArrumaCaminho(caminho);
            if (!caminho.ToLower().EndsWith(".pfx"))
            {
                throw new Exception("Caminho do certificado deve terminar com '.pfx'");
            }

            if (!File.Exists(caminho))
            {
                throw new Exception("Certificado não se encontra no caminho especificado");
            }

            var cert = new X509Certificate2(caminho, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            return cert;
        }

        /// <summary>
        /// Retorna o certificado que está em bytes
        /// </summary>
        /// <param name="bytes">array de byte do certificado</param>
        /// <param name="password">string representando a senha do certificado</param>
        /// <returns></returns>
        public static X509Certificate2 ObterDosBytes(byte[] bytes, string password, X509KeyStorageFlags? keyStorageFlags)
        {
            SecureString stringSegura = null;
            try
            {
                stringSegura = new SecureString();
                if ((password.Length > 0))
                {
                    foreach (Char caractere in password.ToCharArray())
                    {
                        stringSegura.AppendChar(caractere);
                    }
                }

                return ObterDosBytes(bytes, stringSegura, keyStorageFlags);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna o certificado que está em bytes
        /// </summary>
        /// <param name="bytes">array de byte do certificado</param>
        /// <param name="password">SecureString senha do certificado</param>
        /// <returns></returns>
        public static X509Certificate2 ObterDosBytes(byte[] bytes, SecureString password, X509KeyStorageFlags? keyStorageFlags)
        {
            var cert = new X509Certificate2(bytes, password, keyStorageFlags ?? (X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable));
            return cert;
        }

        /// <summary>
        /// Arruma o path caso tenha sido escrito incorretamente
        /// </summary>
        /// <param name="caminho">Caminho do arquivo + pastas</param>
        /// <returns></returns>
        private static string ArrumaCaminho(string caminho)
        {
            caminho = caminho.Replace(@"\\", @"\");
            caminho = caminho.Replace(@"//", @"/");
            caminho = caminho.Replace("\"", ""); //remove "
            caminho = caminho.Replace("\'", ""); //remove '
            caminho = caminho.Replace(@"/", Path.DirectorySeparatorChar.ToString());
            caminho = caminho.Replace(@"\", Path.DirectorySeparatorChar.ToString());

            return caminho;
        }
    }
}