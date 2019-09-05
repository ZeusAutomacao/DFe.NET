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

using CTe.Classes;
using CTe.Classes.Ext;
using CTe.Servicos.Enderecos.Helpers;
using CTe.Wsdl.ConsultaProtocolo;
using CTe.Wsdl.DistribuicaoDFe;
using CTe.Wsdl.Evento;
using CTe.Wsdl.Inutilizacao;
using CTe.Wsdl.Recepcao;
using CTe.Wsdl.RetRecepcao;
using CTe.Wsdl.Status;
using DFe.Classes.Extensoes;
using CTe.CTeOSDocumento.Common;

namespace CTe.Servicos.Factory
{
    public class WsdlFactory
    {
        public static CteStatusServico CriaWsdlCteStatusServico(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteStatusServico;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CteStatusServico(configuracaoWsdl);
        }

        public static CteConsulta CriaWsdlConsultaProtocolo(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteConsulta;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CteConsulta(configuracaoWsdl);
        }

        public static CteInutilizacao CriaWsdlCteInutilizacao(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteInutilizacao;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CteInutilizacao(configuracaoWsdl);
        }

        public static CteRetRecepcao CriaWsdlCteRetRecepcao(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRetRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CteRetRecepcao(configuracaoWsdl);
        }

        public static CteRecepcao CriaWsdlCteRecepcao(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CteRecepcao(configuracaoWsdl);
        }

        public static CteRecepcaoEvento CriaWsdlCteEvento(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CteRecepcaoEvento;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CteRecepcaoEvento(configuracaoWsdl);
        }


        public static CTeDistDFeInteresse CriaWsdlCTeDistDFeInteresse(ConfiguracaoServico configuracaoServico = null)
        {
            var url = UrlHelper.ObterUrlServico(configuracaoServico).CTeDistribuicaoDFe;

            var configuracaoWsdl = CriaConfiguracao(url, configuracaoServico);

            return new CTeDistDFeInteresse(configuracaoWsdl);
        }


        private static WsdlConfiguracao CriaConfiguracao(string url, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var codigoEstado = configServico.cUF.GetCodigoIbgeEmString();
            var certificadoDigital = configServico.X509Certificate2;
            var versaoEmString = configServico.VersaoLayout.GetString();
            var timeOut = configServico.TimeOut;

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