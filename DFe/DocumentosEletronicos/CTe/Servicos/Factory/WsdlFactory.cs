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

using DFe.CertificadosDigitais;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.CTe.Classes;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Enderecos.Helpers;
using DFe.DocumentosEletronicos.CTe.Wsdl.Evento;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeConsultaProtocolo;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeInutilizacao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcaoOS;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRetRecepcao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeStatusServico;
using DFe.DocumentosEletronicos.Entidades;

namespace DFe.DocumentosEletronicos.CTe.Servicos.Factory
{
    public class WsdlFactory
    {
        public static CteStatusServico CriaWsdlCteStatusServico(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteStatusServico;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteStatusServico(configuracaoWsdl);
        }

        public static CteConsulta CriaWsdlConsultaProtocolo(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteConsulta;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteConsulta(configuracaoWsdl);
        }

        public static CteInutilizacao CriaWsdlCteInutilizacao(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteInutilizacao;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteInutilizacao(configuracaoWsdl);
        }

        public static CteRetRecepcao CriaWsdlCteRetRecepcao(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteRetRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteRetRecepcao(configuracaoWsdl);
        }

        public static CteRecepcao CriaWsdlCteRecepcao(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteRecepcao;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteRecepcao(configuracaoWsdl);
        }

        public static CteRecepcaoOS CriaWsdlCteRecepcaoOs(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteRecepcaoOS;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteRecepcaoOS(configuracaoWsdl);
        }

        public static CteRecepcaoEvento CriaWsdlCteEvento(DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var url = UrlHelper.ObterUrlServico(config).CteRecepcaoEvento;

            var configuracaoWsdl = CriaConfiguracao(url, config, certificadoDigital);

            return new CteRecepcaoEvento(configuracaoWsdl);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, DFeConfig config, CertificadoDigital certificadoDigital)
        {
            var codigoEstado = config.EstadoUf.GetCodigoIbgeEmString();
            var certificadoDigitalX509 = certificadoDigital.ObterCertificadoDigital();
            var versaoEmString = config.VersaoServico.GetString();
            var timeOut = config.TimeOut;

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigitalX509,
                Versao = versaoEmString,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = timeOut
            };
        }
    }
}