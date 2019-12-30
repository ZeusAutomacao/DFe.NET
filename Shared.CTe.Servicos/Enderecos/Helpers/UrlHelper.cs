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

using System;
using CTe.Classes;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTe.Servicos.Enderecos.Helpers
{
    public class UrlHelper
    {
        public static UrlCTe ObterUrlServico(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            switch (configServico.tpAmb)
            {
                case TipoAmbiente.Homologacao:
                    if (configServico.TipoEmissao == tpEmis.teSVCRS)
                    {
                        return UrlHomologacaoSvrs();
                    }

                    if (configServico.TipoEmissao == tpEmis.teSVCSP)
                    {
                        return UrlHomologacaoSvcsp();
                    }

                    return UrlHomologacao(configServico);
                case TipoAmbiente.Producao:
                    if (configServico.TipoEmissao == tpEmis.teSVCRS)
                    {
                        return UrlProducaoSvrs();
                    }

                    if (configServico.TipoEmissao == tpEmis.teSVCSP)
                    {
                        return UrlProducaoSvcsp();
                    }

                    return UrlProducao(configServico);
            }

            throw new InvalidOperationException("Tipo Ambiente inválido");
        }

        private static UrlCTe UrlProducaoSvcsp()
        {
            return new UrlCTe
            {
                CteRecepcao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx",
                CteRetRecepcao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteRetRecepcao.asmx",
                CteConsulta = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx",
                CteStatusServico = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteStatusServico.asmx",
                QrCode = @"http://dfe-portal.svrs.rs.gov.br/cte/QRCode",
                CteRecepcaoEvento = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteRecepcaoEvento.asmx"
            };
        }

        private static UrlCTe UrlHomologacaoSvcsp()
        {
            return new UrlCTe
            {
                CteRecepcaoEvento = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteRecepcaoEvento.asmx",
                CteRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteRecepcao.asmx",
                CteRetRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteRetRecepcao.asmx",
                CteConsulta = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx",
                QrCode = @"https://dfe-portal.svrs.rs.gov.br/cte/qrCode",
                CteStatusServico = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteStatusServico.asmx"
            };
        }

        private static UrlCTe UrlHomologacaoSvrs()
        {
            return new UrlCTe
            {
                CteRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcao/CTeRecepcao.asmx",
                CteRetRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteretrecepcao/CTeRetRecepcao.asmx",
                CteInutilizacao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx",
                CteConsulta = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteconsulta/CTeConsulta.asmx",
                CteStatusServico = @"https://cte-homologacao.svrs.rs.gov.br/ws/ctestatusservico/CTeStatusServico.asmx",
                QrCode = @"https://dfe-portal.svrs.rs.gov.br/cte/qrCode",
                CteRecepcaoEvento = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoevento/CTeRecepcaoEvento.asmx"
            };
        }

        private static UrlCTe UrlProducaoSvrs()
        {
            return new UrlCTe
            {
                CteRecepcao = @"https://cte.svrs.rs.gov.br/ws/cterecepcao/CTeRecepcao.asmx",
                CteRetRecepcao = @"https://cte.svrs.rs.gov.br/ws/cteretrecepcao/CTeRetRecepcao.asmx",
                CteInutilizacao = @"https://cte.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx",
                CteConsulta = @"https://cte.svrs.rs.gov.br/ws/cteconsulta/CTeConsulta.asmx",
                CteStatusServico = @"https://cte.svrs.rs.gov.br/ws/ctestatusservico/CTeStatusServico.asmx",
                QrCode = @"http://dfe-portal.svrs.rs.gov.br/cte/QRCode",
                CteRecepcaoEvento = @"https://cte.svrs.rs.gov.br/ws/cterecepcaoevento/CTeRecepcaoEvento.asmx"
            };
        }

        private static UrlCTe UrlProducao(ConfiguracaoServico configuracaoServico)
        {
            switch (configuracaoServico.cUF)
            {
                case Estado.MT:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://cte.sefaz.mt.gov.br/ctews/services/CteStatusServico",
                        CteRetRecepcao = "https://cte.sefaz.mt.gov.br/ctews/services/CteRetRecepcao",
                        CteRecepcao = "https://cte.sefaz.mt.gov.br/ctews/services/CteRecepcao",
                        CteInutilizacao = "https://cte.sefaz.mt.gov.br/ctews/services/CteInutilizacao",
                        CteRecepcaoEvento = "https://cte.sefaz.mt.gov.br/ctews2/services/CteRecepcaoEvento?wsdl",
                        CteConsulta = @"https://cte.sefaz.mt.gov.br/ctews/services/CteConsulta",
                        QrCode = @"https://www.sefaz.mt.gov.br/cte/qrcode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.MS:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://producao.cte.ms.gov.br/ws/CteStatusServico",
                        CteRetRecepcao = @"https://producao.cte.ms.gov.br/ws/CteRetRecepcao",
                        CteRecepcao = @"https://producao.cte.ms.gov.br/ws/CteRecepcao",
                        CteInutilizacao = @"https://producao.cte.ms.gov.br/ws/CteInutilizacao",
                        CteRecepcaoEvento = @"https://producao.cte.ms.gov.br/ws/CteRecepcaoEvento",
                        CteConsulta = @"https://producao.cte.ms.gov.br/ws/CteConsulta",
                        QrCode = @"http://www.dfe.ms.gov.br/cte/qrcode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.MG:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://cte.fazenda.mg.gov.br/cte/services/CteStatusServico",
                        CteRetRecepcao = @"https://cte.fazenda.mg.gov.br/cte/services/CteRetRecepcao",
                        CteRecepcao = @"https://cte.fazenda.mg.gov.br/cte/services/CteRecepcao",
                        CteInutilizacao = @"https://cte.fazenda.mg.gov.br/cte/services/CteInutilizacao",
                        CteRecepcaoEvento = @"https://cte.fazenda.mg.gov.br/cte/services/RecepcaoEvento",
                        CteConsulta = @"https://cte.fazenda.mg.gov.br/cte/services/CteConsulta",
                        QrCode = @"https://cte.fazenda.mg.gov.br/portalcte/sistema/qrcode.xhtml",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.PR:
                    return new UrlCTe
                    {
                        CteStatusServico = @"	https://cte.fazenda.pr.gov.br/cte/CteStatusServico?wsdl",
                        CteRetRecepcao = @"https://cte.fazenda.pr.gov.br/cte/CteRetRecepcao?wsdl",
                        CteRecepcao = @"https://cte.fazenda.pr.gov.br/cte/CteRecepcao?wsdl",
                        CteInutilizacao = @"https://cte.fazenda.pr.gov.br/cte/CteInutilizacao?wsdl",
                        CteRecepcaoEvento = @"https://cte.fazenda.pr.gov.br/cte/CteRecepcaoEvento?wsdl",
                        CteConsulta = @"https://cte.fazenda.pr.gov.br/cte/CteConsulta?wsdl",
                        QrCode = @"http://www.fazenda.pr.gov.br/cte/qrcode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.RS:
                    return new UrlCTe
                    {
                        CteStatusServico =
                            @"https://cte.svrs.rs.gov.br/ws/ctestatusservico/CteStatusServico.asmx",
                        CteRetRecepcao = @"https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx",
                        CteRecepcao = @"https://cte.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx",
                        CteInutilizacao =
                            @"https://cte.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx",
                        CteRecepcaoEvento =
                            @"https://cte.svrs.rs.gov.br/ws/cterecepcaoevento/cterecepcaoevento.asmx",
                        CteConsulta = @"https://cte.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx",
                        QrCode = @"https://dfe-portal.svrs.rs.gov.br/cte/qrCode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.SP:
                    return new UrlCTe
                    {
                        CteStatusServico =
                            @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteStatusServico.asmx",
                        CteRetRecepcao =
                            @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRetRecepcao.asmx",
                        CteRecepcao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx",
                        CteInutilizacao =
                            @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx",
                        CteRecepcaoEvento =
                            @"https://nfe.fazenda.sp.gov.br/cteweb/services/cteRecepcaoEvento.asmx",
                        CteConsulta = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteConsulta.asmx",
                        QrCode = @"https://nfe.fazenda.sp.gov.br/CTeConsulta/qrCode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.AC:
                case Estado.AL:
                case Estado.AM:
                case Estado.BA:
                case Estado.CE:
                case Estado.DF:
                case Estado.ES:
                case Estado.GO:
                case Estado.MA:
                case Estado.PA:
                case Estado.PB:
                case Estado.PI:
                case Estado.RJ:
                case Estado.RN:
                case Estado.RO:
                case Estado.SC:
                case Estado.SE:
                case Estado.TO:
                    return new UrlCTe
                    {
                        CteStatusServico =
                            @"https://cte.svrs.rs.gov.br/ws/ctestatusservico/CteStatusServico.asmx",
                        CteConsulta = @"https://cte.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx",
                        CteInutilizacao =
                            @"https://cte.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx",
                        CteRecepcao = @"https://cte.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx",
                        CteRecepcaoEvento =
                            @"https://cte.svrs.rs.gov.br/ws/cterecepcaoevento/cterecepcaoevento.asmx",
                        CteRetRecepcao = @"https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx",
                        QrCode = @"https://dfe-portal.svrs.rs.gov.br/cte/qrCode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.AP:
                case Estado.PE:
                case Estado.RR:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteStatusServico.asmx",
                        CteRetRecepcao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteRetRecepcao.asmx",
                        CteRecepcao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx",
                        CteInutilizacao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx",
                        CteRecepcaoEvento = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteRecepcaoEvento.asmx",
                        CteConsulta = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx",
                        QrCode = @"https://nfe.fazenda.sp.gov.br/CTeConsulta/qrCode",
                        CTeDistribuicaoDFe = "https://www1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                default:
                    throw new InvalidOperationException(
                        "Não achei a url do seu estado(uf), tente com outra unidade federativa");
            }

        }

        private static UrlCTe UrlHomologacao(ConfiguracaoServico configuracaoServico)
        {
            switch (configuracaoServico.cUF)
            {
                case Estado.MT:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.sefaz.mt.gov.br/ctews/services/CteStatusServico",
                        CteRetRecepcao = @"https://homologacao.sefaz.mt.gov.br/ctews/services/CteRetRecepcao",
                        CteRecepcao = @"https://homologacao.sefaz.mt.gov.br/ctews/services/CteRecepcao",
                        CteInutilizacao = @"https://homologacao.sefaz.mt.gov.br/ctews/services/CteInutilizacao",
                        CteRecepcaoEvento =
                            @"https://homologacao.sefaz.mt.gov.br/ctews2/services/CteRecepcaoEvento?wsdl",
                        CteConsulta = @"https://homologacao.sefaz.mt.gov.br/ctews/services/CteConsulta",
                        QrCode = @"https://homologacao.sefaz.mt.gov.br/cte/qrcode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.MS:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.cte.ms.gov.br/ws/CteStatusServico",
                        CteRetRecepcao = @"https://homologacao.cte.ms.gov.br/ws/CteRetRecepcao",
                        CteRecepcao = @"https://homologacao.cte.ms.gov.br/ws/CteRecepcao",
                        CteInutilizacao = @"https://homologacao.cte.ms.gov.br/ws/CteInutilizacao",
                        CteRecepcaoEvento = @"https://homologacao.cte.ms.gov.br/ws/CteRecepcaoEvento",
                        CteConsulta = @"https://homologacao.cte.ms.gov.br/ws/CteConsulta",
                        QrCode = @"http://www.dfe.ms.gov.br/cte/qrcode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.MG:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://hcte.fazenda.mg.gov.br/cte/services/CteStatusServico?wsdl",
                        CteRetRecepcao = @"https://hcte.fazenda.mg.gov.br/cte/services/CteRetRecepcao?wsdl",
                        CteRecepcao = @"https://hcte.fazenda.mg.gov.br/cte/services/CteRecepcao?wsdl",
                        CteInutilizacao = @"https://hcte.fazenda.mg.gov.br/cte/services/CteInutilizacao?wsdl",
                        CteRecepcaoEvento = @"https://hcte.fazenda.mg.gov.br/cte/services/RecepcaoEvento?wsdl",
                        CteConsulta = @"https://hcte.fazenda.mg.gov.br/cte/services/CteConsulta?wsdl",
                        QrCode = @"https://hcte.fazenda.mg.gov.br/portalcte/sistema/qrcode.xhtml",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.PR:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteStatusServico?wsdl",
                        CteRetRecepcao = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteRetRecepcao?wsdl",
                        CteRecepcao = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteRecepcao?wsdl",
                        CteInutilizacao = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteInutilizacao?wsdl",
                        CteRecepcaoEvento = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteRecepcaoEvento?wsdl",
                        CteConsulta = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteConsulta?wsdl",
                        QrCode = @"http://www.fazenda.pr.gov.br/cte/qrcode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.RS:
                    return new UrlCTe
                    {
                        CteStatusServico =
                            @"https://cte-homologacao.svrs.rs.gov.br/ws/ctestatusservico/CteStatusServico.asmx",
                        CteRetRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx",
                        CteRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcao/CteRecepcao.asmx",
                        CteInutilizacao =
                            @"https://cte-homologacao.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx",
                        CteRecepcaoEvento =
                            @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoevento/cterecepcaoevento.asmx",
                        CteConsulta = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx",
                        QrCode = @"https://dfe-portal.svrs.rs.gov.br/cte/qrCode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.SP:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteStatusServico.asmx",
                        CteRetRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteRetRecepcao.asmx",
                        CteRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx",
                        CteInutilizacao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx",
                        CteRecepcaoEvento = @"https://homologacao.nfe.fazenda.sp.gov.br/cteweb/services/cteRecepcaoEvento.asmx",
                        CteConsulta = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteConsulta.asmx",
                        QrCode = @"https://homologacao.nfe.fazenda.sp.gov.br/CTeConsulta/qrCode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.AC:
                case Estado.AL:
                case Estado.AM:
                case Estado.BA:
                case Estado.CE:
                case Estado.DF:
                case Estado.ES:
                case Estado.GO:
                case Estado.MA:
                case Estado.PA:
                case Estado.PB:
                case Estado.PI:
                case Estado.RJ:
                case Estado.RN:
                case Estado.RO:
                case Estado.SC:
                case Estado.SE:
                case Estado.TO:
                    return new UrlCTe
                    {
                        CteStatusServico =
                            @"https://cte-homologacao.svrs.rs.gov.br/ws/ctestatusservico/CTeStatusServico.asmx",
                        CteConsulta = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteconsulta/CTeConsulta.asmx",
                        CteInutilizacao =
                            @"https://cte-homologacao.svrs.rs.gov.br/ws/cteinutilizacao/cteinutilizacao.asmx",
                        CteRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcao/CTeRecepcao.asmx",
                        CteRecepcaoEvento =
                            @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoevento/CTeRecepcaoEvento.asmx",
                        CteRetRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteretrecepcao/CTeRetRecepcao.asmx",
                        QrCode = @"https://dfe-portal.svrs.rs.gov.br/cte/qrCode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                case Estado.AP:
                case Estado.PE:
                case Estado.RR:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteStatusServico.asmx",
                        CteRetRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteRetRecepcao.asmx",
                        CteRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteRecepcao.asmx",
                        CteInutilizacao = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx",
                        CteRecepcaoEvento = @"https://cte.sefaz.rs.gov.br/ws/CteRecepcaoEvento/CteRecepcaoEvento.asmx",
                        CteConsulta = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx",
                        QrCode = @"https://homologacao.nfe.fazenda.sp.gov.br/CTeConsulta/qrCode",
                        CTeDistribuicaoDFe = @"https://hom1.cte.fazenda.gov.br/CTeDistribuicaoDFe/CTeDistribuicaoDFe.asmx"
                    };
                default:
                    throw new InvalidOperationException(
                        "Não achei a url do seu estado(uf), tente com outra unidade federativa");
            }
        }
    }
}