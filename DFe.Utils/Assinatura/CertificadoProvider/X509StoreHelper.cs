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