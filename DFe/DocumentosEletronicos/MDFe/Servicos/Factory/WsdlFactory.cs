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

using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Ext;
using DFe.DocumentosEletronicos.MDFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Enderecos.Helper;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeEventos;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeRecepcao;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeRetRecepcao;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeStatusServico;

namespace DFe.DocumentosEletronicos.MDFe.Servicos.Factory
{
    public static class WsdlFactory
    {
        public static MDFeConsNaoEnc CriaWsdlMDFeConsNaoEnc(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(dfeConfig.TipoAmbiente).MDFeConsNaoEnc;
            var versao = dfeConfig.VersaoServico.GetVersaoString();
            var configuracaoWsdl = CriaConfiguracao(url, versao, dfeConfig, certificadoDigital);

            var ws = new MDFeConsNaoEnc(configuracaoWsdl);
            return ws;
        }

        public static MDFeConsulta CriaWsdlMDFeConsulta(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(dfeConfig.TipoAmbiente).MDFeConsulta;

            var versao = dfeConfig.VersaoServico.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, dfeConfig, certificadoDigital);

            return new MDFeConsulta(configuracaoWsdl);
        }

        public static MDFeRecepcaoEvento CriaWsdlMDFeRecepcaoEvento(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(dfeConfig.TipoAmbiente).MDFeRecepcaoEvento;
            var versao = dfeConfig.VersaoServico.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, dfeConfig, certificadoDigital);

            return new MDFeRecepcaoEvento(configuracaoWsdl);
        }

        public static MDFeRecepcao CriaWsdlMDFeRecepcao(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(dfeConfig.TipoAmbiente).MDFeRecepcao;
            var versaoServico = dfeConfig.VersaoServico.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versaoServico, dfeConfig, certificadoDigital);

            return new MDFeRecepcao(configuracaoWsdl);
        }

        public static MDFeRetRecepcao CriaWsdlMDFeRetRecepcao(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(dfeConfig.TipoAmbiente).MDFeRetRecepcao;
            var versao = dfeConfig.VersaoServico.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, dfeConfig, certificadoDigital);

            return new MDFeRetRecepcao(configuracaoWsdl);
        }

        public static MDFeStatusServico CriaWsdlMDFeStatusServico(DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(dfeConfig.TipoAmbiente).MDFeStatusServico;
            var versao = dfeConfig.VersaoServico.GetVersaoString();

            var configuracaoWsdl = CriaConfiguracao(url, versao, dfeConfig, certificadoDigital);

            return new MDFeStatusServico(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao, DFeConfig dfeConfig, CertificadoDigital certificadoDigital)
        {
            var codigoEstado = dfeConfig.EstadoUf.GetCodigoIbgeEmString();

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital.ObterCertificadoDigital(),
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = dfeConfig.TimeOut
            };
        }
    }
}
