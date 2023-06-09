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

using CTe.CTeOSDocumento.Common;
using DFe.Classes.Extensoes;
using MDFe.Classes.Extencoes;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Wsdl.Gerado.MDFeStatusServico;
using MDFe.Wsdl.MDFeConsultaNaoEncerrados;
using MDFe.Wsdl.MDFeConsultaProtoloco;
using MDFe.Wsdl.MDFeEventos;
using MDFe.Wsdl.MDFeRecepcao;
using MDFe.Wsdl.MDFeRetRecepcao;
using System.Security.Cryptography.X509Certificates;

namespace MDFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc()
        {
            return CriaWsdlMDFeConsNaoEncInternal(MDFeConfiguracao.VersaoWebService, MDFeConfiguracao.X509Certificate2);
        }

        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaWsdlMDFeConsNaoEncInternal(servicoConfiguracao.VersaoWebService, servicoConfiguracao.X509Certificate2);
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta()
        {
            return CriaWsdlMDFeConsultaInternal(MDFeConfiguracao.VersaoWebService, MDFeConfiguracao.X509Certificate2);
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaWsdlMDFeConsultaInternal(servicoConfiguracao.VersaoWebService, servicoConfiguracao.X509Certificate2);
        }

        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento()
        {
            return CriaWsdlMDFeRecepcaoEventoInternal(MDFeConfiguracao.VersaoWebService, MDFeConfiguracao.X509Certificate2);
        }
        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaWsdlMDFeRecepcaoEventoInternal(servicoConfiguracao.VersaoWebService, servicoConfiguracao.X509Certificate2);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao()
        {
            return CriaWsdlMDFeRecepcaoInternal(MDFeConfiguracao.VersaoWebService, MDFeConfiguracao.X509Certificate2);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaWsdlMDFeRecepcaoInternal(servicoConfiguracao.VersaoWebService, servicoConfiguracao.X509Certificate2);
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao()
        {
            return CriaWsdlMDFeRetRecepcaoInternal(MDFeConfiguracao.VersaoWebService, MDFeConfiguracao.X509Certificate2);
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaWsdlMDFeRetRecepcaoInternal(servicoConfiguracao.VersaoWebService, servicoConfiguracao.X509Certificate2);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico()
        {
            return CriaWsdlMDFeStatusServicoInternal(MDFeConfiguracao.VersaoWebService, MDFeConfiguracao.X509Certificate2);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico(MDFeServicoConfiguracao servicoConfiguracao)
        {
            return CriaWsdlMDFeStatusServicoInternal(servicoConfiguracao.VersaoWebService, servicoConfiguracao.X509Certificate2);
        }

        #region Métodos privados

        private static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEncInternal(MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var url = UrlHelper.ObterUrlServico(versaoWebService.TipoAmbiente).MDFeConsNaoEnc;
            var versao = versaoWebService.VersaoLayout.GetVersaoString();
            var configuracaoWsdl = CriaConfiguracao(url, versao, versaoWebService, certificado);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        private static MDFeConsulta CriaWsdlMDFeConsultaInternal(MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var url = UrlHelper.ObterUrlServico(versaoWebService.TipoAmbiente).MDFeConsulta;
            var versao = versaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, versaoWebService, certificado);

            return new MDFeConsulta(configuracaoWsdl);
        }

        private static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEventoInternal(MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var url = UrlHelper.ObterUrlServico(versaoWebService.TipoAmbiente).MDFeRecepcaoEvento;
            var versao = versaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, versaoWebService, certificado);

            return new MDFeRecepcaoEvento(configuracaoWsdl);
        }

        private static MDFeRecepcao CriaWsdlMDFeRecepcaoInternal(MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var url = UrlHelper.ObterUrlServico(versaoWebService.TipoAmbiente).MDFeRecepcao;
            var versaoDoServico = versaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoDoServico, versaoWebService, certificado);

            return new MDFeRecepcao(configuracaoWsdl);
        }

        private static MDFeRetRecepcao CriaWsdlMDFeRetRecepcaoInternal(MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var url = UrlHelper.ObterUrlServico(versaoWebService.TipoAmbiente).MDFeRetRecepcao;
            var versao = versaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, versaoWebService, certificado);

            return new MDFeRetRecepcao(configuracaoWsdl);
        }

        private static MDFeStatusServico CriaWsdlMDFeStatusServicoInternal(MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var url = UrlHelper.ObterUrlServico(versaoWebService.TipoAmbiente).MDFeStatusServico;
            var versao = versaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, versaoWebService, certificado);

            return new MDFeStatusServico(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao, MDFeVersaoWebService versaoWebService, X509Certificate2 certificado)
        {
            var codigoEstado = versaoWebService.UfEmitente.GetCodigoIbgeEmString();
            var certificadoDigital = certificado;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = versaoWebService.TimeOut
            };
        }

        #endregion
    }
}
