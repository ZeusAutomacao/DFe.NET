/********************************************************************************/
/* Projeto: Biblioteca ZeusDFe                                                  */
/* Biblioteca C# para auxiliar no desenvolvimento das demais bibliotecas DFe    */
/*                                                                              */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DFe.Utils.Assinatura.CertificadoProvider.Extencoes
{
    public static class X509Certificate2Extension
    {
        public static void SetPinForPrivateKey(this X509Certificate2 certificate, string pin)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");
            var key = (RSACryptoServiceProvider)certificate.PrivateKey;

            var providerHandle = IntPtr.Zero;
            var pinBuffer = Encoding.ASCII.GetBytes(pin);

            SafeNativeMethods.Execute(() => SafeNativeMethods.CryptAcquireContext(ref providerHandle,
                                            key.CspKeyContainerInfo.KeyContainerName,
                                            key.CspKeyContainerInfo.ProviderName,
                                            key.CspKeyContainerInfo.ProviderType,
                                            SafeNativeMethods.CryptContextFlags.Silent));
            SafeNativeMethods.Execute(() => SafeNativeMethods.CryptSetProvParam(providerHandle,
                                            SafeNativeMethods.CryptParameter.KeyExchangePin,
                                            pinBuffer, 0));
            SafeNativeMethods.Execute(() => SafeNativeMethods.CertSetCertificateContextProperty(
                                            certificate.Handle,
                                            SafeNativeMethods.CertificateProperty.CryptoProviderHandle,
                                            0, providerHandle));
        }
    }

    internal static class SafeNativeMethods
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

        public static void Execute(Func<bool> action)
        {
            if (!action())
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}