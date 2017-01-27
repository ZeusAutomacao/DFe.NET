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
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace DFe.Utils.Assinatura
{
    public static class CertificadoDigital
    {

        /// <summary>
        /// Exibe a lista de certificados instalados no PC e devolve o certificado selecionado
        /// </summary>
        /// <returns></returns>
        public static X509Certificate2 ObterDoRepositorio()
        {
            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

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
        /// Obtém um certificado instalado no PC a partir do número de série passado no parâmetro
        /// </summary>
        /// <param name="numeroSerial">Serial do certificado</param>
        /// <param name="senha">Informe a senha se desejar que o usuário não precise digitá-la toda vez que for iniciada uma nova instância da aplicação. Não informe a senha para certificado A1!</param>
        /// <returns></returns>
        public static X509Certificate2 ObterDoRepositorio(ConfiguracaoCertificado configuracaoCertificado)
        {
            if (string.IsNullOrEmpty(configuracaoCertificado.Serial))
                throw new Exception("O nº de série do certificado não foi informado para a função ObterDoRepositorio!");

            return configuracaoCertificado.CriaCertificado();
        }

        /// <summary>
        /// Obtém um certificado a partir do arquivo e da senha passados nos parâmetros
        /// </summary>
        /// <param name="arquivo">Arquivo do certificado digital</param>
        /// <param name="senha">Senha do certificado digital</param>
        /// <returns></returns>
        public static X509Certificate2 ObterDeArquivo(string arquivo, string senha)
        {
            if (!File.Exists(arquivo))
            {
                throw new Exception(string.Format("Certificado digital {0} não encontrado!", arquivo));
            }

            var certificado = new X509Certificate2(arquivo, senha, X509KeyStorageFlags.MachineKeySet);
            return certificado;
        }

        private static X509Certificate2 _certificado;

        /// <summary>
        /// Obtém um objeto contendo o certificado digital
        /// <para>Se for informado <see cref="ConfiguracaoCertificado.Arquivo"/>, 
        /// o certificado digital será obtido pelo método <see cref="ObterDeArquivo(string,string)"/>,
        /// senão será obtido pelo método <see cref="ObterDoRepositorio()"/> </para>
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

        private static X509Certificate2 ObterDadosCertificado(ConfiguracaoCertificado configuracaoCertificado)
        {
            return string.IsNullOrEmpty(configuracaoCertificado.Arquivo)
                ? ObterDoRepositorio(configuracaoCertificado)
                : ObterDeArquivo(configuracaoCertificado.Arquivo,
                    configuracaoCertificado.Senha);
        }
    }
}