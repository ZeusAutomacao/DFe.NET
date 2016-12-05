﻿/********************************************************************************/
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
        public static X509Certificate2 ObterDoRepositorio(string numeroSerial, string senha = null)
        {
            if (string.IsNullOrEmpty(numeroSerial))
                throw new Exception("O nº de série do certificado não foi informado para a função ObterDoRepositorio!");

            X509Certificate2 certificado = null;

            var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.MaxAllowed);

                foreach (var item in store.Certificates)
                {
                    if (item.SerialNumber != null && item.SerialNumber.ToUpper().Equals(numeroSerial.ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                        certificado = item;
                }

                if (certificado == null)
                    throw new Exception(string.Format("Certificado digital nº {0} não encontrado!", numeroSerial.ToUpper()));
            }
            finally
            {
                store.Close();
            }
            
            if (string.IsNullOrEmpty(senha)) return certificado;

            //Se a senha for passada no parâmetro
            var senhaSegura = new SecureString();
            var passPhrase = senha.ToCharArray();
            foreach (var t in passPhrase)
            {
                senhaSegura.AppendChar(t);
            }

            var chavePrivada = certificado.PrivateKey as RSACryptoServiceProvider;
            if (chavePrivada == null) return certificado;

            var cspParameters = new CspParameters(chavePrivada.CspKeyContainerInfo.ProviderType,
                chavePrivada.CspKeyContainerInfo.ProviderName,
                chavePrivada.CspKeyContainerInfo.KeyContainerName,
                null,
                senhaSegura);
            var rsaCsp = new RSACryptoServiceProvider(cspParameters);
            certificado.PrivateKey = rsaCsp;
            return certificado;
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
    }
}