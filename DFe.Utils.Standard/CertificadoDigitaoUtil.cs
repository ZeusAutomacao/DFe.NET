using System;
using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace DFe.Utils.Standard
{
    public class CertificadoDigitaoUtil
    {
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
        public static X509Certificate2 ObterDosBytes(byte[] bytes, string password)
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

                return ObterDosBytes(bytes, stringSegura);
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
        public static X509Certificate2 ObterDosBytes(byte[] bytes, SecureString password)
        {
            var cert = new X509Certificate2(bytes, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
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
