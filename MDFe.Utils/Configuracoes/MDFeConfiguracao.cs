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
using DFe.Utils;
using DFe.Utils.Assinatura;
using System;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.Utils.Configuracoes
{
    public class MDFeConfiguracao : IDisposable 
    {
        private static MDFeVersaoWebService _versaoWebService;

        public MDFeConfiguracao()
        {
            VersaoWebService = new MDFeVersaoWebService();
        }

        public static ConfiguracaoCertificado ConfiguracaoCertificado { get; set; }

        public static bool IsSalvarXml { get; set; }
        public static string CaminhoSchemas { get; set; }
        public static string CaminhoSalvarXml { get; set; }
        public static bool IsAdicionaQrCode { get; set; }

        public static MDFeVersaoWebService VersaoWebService
        {
            get { return GetMdfeVersaoWebService(); }
            set { _versaoWebService = value; }
        }

        private static MDFeVersaoWebService GetMdfeVersaoWebService()
        {
            if(_versaoWebService == null)
                _versaoWebService = new MDFeVersaoWebService();

            return _versaoWebService;
        }

        private static X509Certificate2 _certificado = null;
        public static X509Certificate2 X509Certificate2
        {
            get
            {
                if (_certificado != null)
                    if (!ConfiguracaoCertificado.ManterDadosEmCache)
                        _certificado.Reset();
                _certificado = ObterCertificado();
                return _certificado;
            }
        }

        public static bool NaoSalvarXml()
        {
            return !IsSalvarXml;
        }

        private static X509Certificate2 ObterCertificado()
        {
            return CertificadoDigital.ObterCertificado(ConfiguracaoCertificado);
        }

        public void Dispose()
        {
            if (!ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }

        ~MDFeConfiguracao()
        {
            if (!ConfiguracaoCertificado.ManterDadosEmCache && _certificado != null)
            {
                _certificado.Reset();
                _certificado = null;
            }
        }
    }

    public class MDFeVersaoWebService
    {
        public int TimeOut { get; set; }
        public Estado UfEmitente { get; set; }
        public TipoAmbiente TipoAmbiente { get; set; }
        public VersaoServico VersaoLayout { get; set; }
    }
}
