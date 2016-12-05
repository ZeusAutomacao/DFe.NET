/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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
using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils.Assinatura;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace MDFe.Utils.Configuracoes
{
    public class MDFeConfiguracao
    {
        public MDFeConfiguracao()
        {
            VersaoWebService = new MDFeVersaoWebService();
        }

        public static string CaminhoCertificadoDigital { get; set; }
        public static string SenhaCertificadoDigital { get; set; }
        public static string NumeroSerieCertificadoDigital { get; set; }

        public static bool IsSalvarXml { get; set; }
        public static string CaminhoSchemas { get; set; }
        public static string CaminhoSalvarXml { get; set; }

        public static MDFeVersaoWebService VersaoWebService { get; set; }

        public static X509Certificate2 X509Certificate2 { get { return ObterCertificado(); } }


        public static bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }

        private static X509Certificate2 ObterCertificado()
        {
            if (!string.IsNullOrEmpty(CaminhoCertificadoDigital) && !string.IsNullOrEmpty(SenhaCertificadoDigital))
            {
                return CertificadoDigital.ObterDeArquivo(CaminhoCertificadoDigital, SenhaCertificadoDigital);
            }

            return CertificadoDigital.ObterDoRepositorio(NumeroSerieCertificadoDigital, SenhaCertificadoDigital);
        }
    }

    public class MDFeVersaoWebService
    {
        public int TimeOut { get; set; }
        public EstadoUF UfDestino { get; set; }
        public TipoAmbiente TipoAmbiente { get; set; }
        public VersaoServico VersaoMDFeRecepcao { get; set; }
        public VersaoServico VersaoMDFeRetRecepcao { get; set; }
        public VersaoServico VersaoMDFeRecepcaoEvento { get; set; }
        public VersaoServico VersaoMDFeConsulta { get; set; }
        public VersaoServico VersaoMDFeStatusServico { get; set; }
        public VersaoServico VersaoMDFeConsNaoEnc { get; set; }
    }
}