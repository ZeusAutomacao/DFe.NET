using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DFe.Utils.Assinatura
{
    public static class CertificadoDigital
    {
        private static readonly Dictionary<string, X509Certificate2> CacheCertificado = new Dictionary<string, X509Certificate2>();

        #region Métodos privados

        /// <summary>
        /// Cria e devolve um objeto <see cref="X509Store"/>
        /// </summary>
        /// <param name="openFlags"></param>
        /// <returns></returns>
        public static X509Store ObterX509Store(OpenFlags openFlags)
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(openFlags);
            return store;
        }

        #region Métodos para obter um certificado X509Certificate2

        /// <summary>
        /// Obtém um certificado a partir do arquivo e da senha passados nos parâmetros
        /// </summary>
        /// <param name="arquivo">Arquivo do certificado digital</param>
        /// <param name="senha">Senha do certificado digital</param>
        /// <returns></returns>
        private static X509Certificate2 ObterDeArquivo(string arquivo, string senha, X509KeyStorageFlags keyStorageFlag)
        {
            if (!File.Exists(arquivo))
            {
                throw new Exception(string.Format("Certificado digital {0} não encontrado!", arquivo));
            }

            var certificado = new X509Certificate2(arquivo, senha, keyStorageFlag);
            return certificado;
        }


        /// <summary>
        /// Obtém um certificado a partir do arquivo e da senha passados nos parâmetros
        /// </summary>
        /// <param name="arrayBytes">Array de bytes do certificado digital</param>
        /// <param name="senha">Senha do certificado digital</param>
        /// <returns></returns>
        private static X509Certificate2 ObterDoArrayBytes(byte[] arrayBytes, string senha, X509KeyStorageFlags keyStorageFlag)
        {
            try
            {
                var certificado = new X509Certificate2(arrayBytes, senha, keyStorageFlag);
                return certificado;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel converter o stream para o certificado.", ex);
            }
        }

        /// <summary>
        /// Obtém um objeto <see cref="X509Certificate2"/> pelo serial passado no parÂmetro
        /// </summary>
        /// <returns></returns>
        private static X509Certificate2 ObterDoRepositorio(string serial, OpenFlags opcoesDeAbertura)
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

        /// <summary>
        /// Obtém um objeto <see cref="X509Certificate2"/> pelo serial passado no parâmetro e com opção de definir o PIN
        /// </summary>
        /// <param name="serial"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        private static X509Certificate2 ObterDoRepositorioPassandoPin(string serial, string senha = null)
        {
            var certificado = ObterDoRepositorio(serial, OpenFlags.ReadOnly);
            if (string.IsNullOrEmpty(senha)) return certificado;
            certificado.DefinirPinParaChavePrivada(senha);
            return certificado;
        }

        #endregion

        /// <summary>
        /// Define o PIN para chave privada de um objeto <see cref="X509Certificate2"/> passado no parâmetro
        /// </summary>
        private static void DefinirPinParaChavePrivada(this X509Certificate2 certificado, string pin)
        {
            /// Suprimindo o aviso CA1416 para esta região de código específica
#pragma warning disable CA1416
            if (Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32S)
            {
                if (certificado == null) throw new ArgumentNullException("certificado");
                var key = (RSACryptoServiceProvider)certificado.PrivateKey;

                var providerHandle = IntPtr.Zero;
                var pinBuffer = Encoding.ASCII.GetBytes(pin);

                MetodosNativos.Executar(() => MetodosNativos.CryptAcquireContext(ref providerHandle,
                    key.CspKeyContainerInfo.KeyContainerName,
                    key.CspKeyContainerInfo.ProviderName,
                    key.CspKeyContainerInfo.ProviderType,
                    MetodosNativos.CryptContextFlags.Silent));
                MetodosNativos.Executar(() => MetodosNativos.CryptSetProvParam(providerHandle,
                    MetodosNativos.CryptParameter.KeyExchangePin,
                    pinBuffer, 0));
                MetodosNativos.Executar(() => MetodosNativos.CertSetCertificateContextProperty(
                    certificado.Handle,
                    MetodosNativos.CertificateProperty.CryptoProviderHandle,
                    0, providerHandle));
            }
            else
            {
                throw new NotSupportedException("Metodo DefinirPinParaChavePrivada com suporte apenas no Windows atualmente!");
            }
#pragma warning restore CA1416
        }

        /// <summary>
        /// Busca o certificado de acordo com o <see cref="ConfiguracaoCertificado.TipoCertificado"/>
        /// </summary>
        /// <returns></returns>
        private static X509Certificate2 ObterDadosCertificado(ConfiguracaoCertificado configuracaoCertificado)
        {
            switch (configuracaoCertificado.TipoCertificado)
            {
                case TipoCertificado.A1Repositorio:
                    return ObterDoRepositorio(configuracaoCertificado.Serial, OpenFlags.MaxAllowed);
                case TipoCertificado.A1ByteArray:
                    return ObterDoArrayBytes(configuracaoCertificado.ArrayBytesArquivo, configuracaoCertificado.Senha, configuracaoCertificado.KeyStorageFlags);
                case TipoCertificado.A1Arquivo:
                    return ObterDeArquivo(configuracaoCertificado.Arquivo, configuracaoCertificado.Senha, configuracaoCertificado.KeyStorageFlags);
                case TipoCertificado.A3:
                    return ObterDoRepositorioPassandoPin(configuracaoCertificado.Serial, configuracaoCertificado.Senha);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        /// <summary>
        /// Obtém um objeto contendo o certificado digital
        /// <para>Se for informado <see cref="ConfiguracaoCertificado.Arquivo"/>, 
        /// o certificado digital será obtido pelo método <see cref="ObterDeArquivo(string,string)"/>,
        /// senão será obtido pelo método <see cref="ListareObterDoRepositorio"/> </para>
        /// <para>Para liberar os recursos do certificado, após seu uso, invoque o método <see cref="X509Certificate2.Reset()"/></para>
        /// </summary>
        public static X509Certificate2 ObterCertificado(ConfiguracaoCertificado configuracaoCertificado)
        {
            if (!configuracaoCertificado.ManterDadosEmCache)
                return ObterDadosCertificado(configuracaoCertificado);

            if (!string.IsNullOrEmpty(configuracaoCertificado.CacheId) && CacheCertificado.ContainsKey(configuracaoCertificado.CacheId))
                return CacheCertificado[configuracaoCertificado.CacheId];

            var certificado = ObterDadosCertificado(configuracaoCertificado);

            var keyCertificado = string.IsNullOrEmpty(configuracaoCertificado.CacheId)
                ? certificado.SerialNumber
                : configuracaoCertificado.CacheId;

            configuracaoCertificado.CacheId = keyCertificado;

            CacheCertificado.Add(keyCertificado, certificado);

            return CacheCertificado[keyCertificado];
        }

        /// <summary>
        /// Obtém a assinatura do certificado digital no formato PKCS#1, baseado em um array de bytes passado como Argumento [value].
        /// </summary>
        /// <param name="configuracaoCertificado"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ObterAssinaturaPkcs1(ConfiguracaoCertificado configuracaoCertificado, byte[] value)
        {
            X509Certificate2 certificado = ObterCertificado(configuracaoCertificado);
            using (RSA rsa = certificado.GetRSAPrivateKey())
                return rsa.SignData(value, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }

        public static void ClearCache()
        {
            CacheCertificado.Clear();
        }
    }

    internal static class MetodosNativos
    {
        internal enum CryptContextFlags
        {
            None = 0,
            Silent = 0x40
        }

        internal enum CertificateProperty
        {
            None = 0,
            CryptoProviderHandle = 0x1
        }

        internal enum CryptParameter
        {
            None = 0,
            KeyExchangePin = 0x20
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CryptAcquireContext(
            ref IntPtr hProv,
            string containerName,
            string providerName,
            int providerType,
            CryptContextFlags flags
            );

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CryptSetProvParam(
            IntPtr hProv,
            CryptParameter dwParam,
            [In] byte[] pbData,
            uint dwFlags);

        [DllImport("CRYPT32.DLL", SetLastError = true)]
        internal static extern bool CertSetCertificateContextProperty(
            IntPtr pCertContext,
            CertificateProperty propertyId,
            uint dwFlags,
            IntPtr pvData
            );

        public static void Executar(Func<bool> action)
        {
            if (!action())
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }

    public static class ExtensaoCertificadoDigital
    {
        /// <summary>
        /// Extenção para certificado digital
        /// <para>Verificar validade do certificado digital, se vencido dispara ArgumentException</para>
        /// </summary>
        /// <param name="x509Certificate2"></param>
        public static void VerificaValidade(this X509Certificate2 x509Certificate2)
        {
            DateTime dataExpiracao = Convert.ToDateTime(x509Certificate2.GetExpirationDateString());

            if (dataExpiracao <= DateTime.Now)
            {
                throw new ArgumentException("Certificado digital vencido na data => " + dataExpiracao);
            }
        }

        /// <summary>
        /// Extensão para retornar o número de dias válidos do certificado
        /// </summary>
        /// <param name="x509Certificate2"></param>
        /// <returns>Número de dias válidos</returns> 
        public static int VerificaDiasValidade(this X509Certificate2 x509Certificate2)
        {
            DateTime dtExp = Convert.ToDateTime(x509Certificate2.GetExpirationDateString().Substring(0, 10));
            TimeSpan dt = dtExp.Subtract(DateTime.Today);

            return dt.Days;
        }

        /// <summary>
        /// Extenção para certificado digital
        /// <para>Se usado ele retorna true se for um hardware, se for PenDriver ou SmartCard</para>
        /// </summary>
        /// <param name="x509Certificate2"></param>
        /// <returns>bool</returns>
        public static bool IsA3(this X509Certificate2 x509Certificate2)
        {
            if (x509Certificate2 == null)
                return false;

            bool result = false;

            /// Suprimindo o aviso CA1416 para esta região de código específica
#pragma warning disable CA1416
            if (Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32S)
            {

                try
                {
                    RSACryptoServiceProvider service = x509Certificate2.PrivateKey as RSACryptoServiceProvider;

                    if (service != null)
                    {
                        if (service.CspKeyContainerInfo.Removable &&
                            service.CspKeyContainerInfo.HardwareDevice)
                            result = true;
                    }
                }
                catch
                {
                    //assume que é false
                    result = false;
                }
            }
            else
            {
                throw new NotSupportedException("Metodo IsA3 com suporte apenas no Windows atualmente!");
            }
#pragma warning restore CA1416

            return result;
        }
    }
}