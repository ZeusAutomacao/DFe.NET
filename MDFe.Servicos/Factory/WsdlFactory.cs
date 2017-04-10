﻿/********************************************************************************/
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

using DFe.Classes.Extensoes;
using MDFe.Classes.Extencoes;
using MDFe.Servicos.Enderecos.Helper;
using MDFe.Utils.Configuracoes;
using MDFe.Wsdl.Configuracao;
using MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;
using MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;
using MDFe.Wsdl.Gerado.MDFeRecepcao;
using MDFe.Wsdl.Gerado.MDFeRetRecepcao;
using MDFe.Wsdl.Gerado.MDFeStatusServico;

namespace MDFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsNaoEnc;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();
            var configuracaoWsdl = CriaConfiguracao(url, versao);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeConsulta;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeConsulta(configuracaoWsdl);
        }

        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcaoEvento;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeRecepcaoEvento(configuracaoWsdl);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRecepcao;
            var versaoServico = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico);

            return new MDFeRecepcao(configuracaoWsdl); ;
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeRetRecepcao;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeRetRecepcao(configuracaoWsdl);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico()
        {
            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente).MDFeStatusServico;
            var versao = MDFeConfiguracao.VersaoWebService.VersaoLayout.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao);

            return new MDFeStatusServico(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao)
        {
            var codigoEstado = MDFeConfiguracao.VersaoWebService.UfEmitente.GetCodigoIbgeEmString();
            var certificadoDigital = MDFeConfiguracao.X509Certificate2;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = MDFeConfiguracao.VersaoWebService.TimeOut
            };
        }
    }
}
