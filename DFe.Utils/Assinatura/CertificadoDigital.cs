/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
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
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DFe.Utils.Assinatura
{
    public static class CertificadoDigital
    {
        private static X509Certificate2 _certificado;

        #region Métodos privados

        /// <summary>
        /// Cria e devolve um objeto <see cref="X509Store"/>
        /// </summary>
        /// <param name="openFlags"></param>
        /// <returns></returns>
        private static X509Store ObterX509Store(OpenFlags openFlags)
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
        private static X509Certificate2 ObterDeArquivo(string arquivo, string senha)
        {
            if (!File.Exists(arquivo))
            {
                throw new Exception(String.Format("Certificado digital {0} não encontrado!", arquivo));
            }

            var certificado = new X509Certificate2(arquivo, senha, X509KeyStorageFlags.MachineKeySet);
            return certificado;
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

        /// <summary>
        /// Se a propriedade <see cref="ConfiguracaoCertificado.Arquivo"/> for informada, Obtém o certificado do arquivo, senão obtém o certificado do repositório
        /// </summary>
        /// <returns></returns>
        private static X509Certificate2 ObterDadosCertificado(ConfiguracaoCertificado configuracaoCertificado)
        {
            switch (configuracaoCertificado.TipoCertificado)
            {
                case TipoCertificado.A1Repositorio:
                    return ObterDoRepositorio(configuracaoCertificado.Serial, OpenFlags.MaxAllowed);
                case TipoCertificado.A1Arquivo:
                    return ObterDeArquivo(configuracaoCertificado.Arquivo, configuracaoCertificado.Senha);
                case TipoCertificado.A3:
                    return ObterDoRepositorioPassandoPin(configuracaoCertificado.Serial, configuracaoCertificado.Senha);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        /// <summary>
        /// Exibe a lista de certificados instalados no PC e devolve o certificado selecionado
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2 ListareObterDoRepositorio()
        {
            var store = ObterX509Store(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);
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
            if (_certificado != null)
                return _certificado;
            _certificado = ObterDadosCertificado(configuracaoCertificado);
            return _certificado;
        }


        public static void clearCache()
        {
            if (_certificado != null)
                _certificado =null;

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
}