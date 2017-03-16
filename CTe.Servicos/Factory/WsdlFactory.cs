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
using CTe.Classes.Ext;
using CTeDLL.Servicos.Enderecos.Helpers;
using CTeDLL.Wsdl.Configuracao;
using CTeDLL.Wsdl.ConsultaProtocolo;
using CTeDLL.Wsdl.Evento;
using CTeDLL.Wsdl.Inutilizacao;
using CTeDLL.Wsdl.Recepcao;
using CTeDLL.Wsdl.RetRecepcao;
using CTeDLL.Wsdl.Status;
using DFe.Classes.Extencoes;

namespace CTeDLL.Servicos.Factory
{
    public class WsdlFactory
    {
        public static CteStatusServico CriaWsdlCteStatusServico()
        {
            var url = UrlHelper.ObterUrlServico().CteStatusServico;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteStatusServico(configuracaoWsdl);
        }

        public static CteConsulta CriaWsdlConsultaProtocolo()
        {
            var url = UrlHelper.ObterUrlServico().CteConsulta;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteConsulta(configuracaoWsdl);
        }

        public static CteInutilizacao CriaWsdlCteInutilizacao()
        {
            var url = UrlHelper.ObterUrlServico().CteInutilizacao;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteInutilizacao(configuracaoWsdl);
        }

        public static CteRetRecepcao CriaWsdlCteRetRecepcao()
        {
            var url = UrlHelper.ObterUrlServico().CteRetRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteRetRecepcao(configuracaoWsdl);
        }

        public static CteRecepcao CriaWsdlCteRecepcao()
        {
            var url = UrlHelper.ObterUrlServico().CteRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteRecepcao(configuracaoWsdl);
        }

        public static CteRecepcaoEvento CriaWsdlCteEvento()
        {
            var url = UrlHelper.ObterUrlServico().CteRecepcaoEvento;

            var configuracaoWsdl = CriaConfiguracao(url);

            return new CteRecepcaoEvento(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            var codigoEstado = configuracaoServico.cUF.GetCodigoIbgeEmString();
            var certificadoDigital = configuracaoServico.X509Certificate2;
            var versaoEmString = configuracaoServico.VersaoLayout.GetString();
            var timeOut = configuracaoServico.TimeOut;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versaoEmString,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = timeOut
            };
        }
    }
}