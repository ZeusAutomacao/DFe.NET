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
using DFe.CTe.Classes;
using DFe.Entidades;
using DFe.Flags;

namespace DFe.CTe.Servicos.Enderecos.Helpers
{
    public class UrlHelper
    {
        public static UrlCTe ObterUrlServico()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            switch (configuracaoServico.tpAmb)
            {
                case TipoAmbiente.Homologacao:
                    return UrlHomologacao();
                case TipoAmbiente.Producao:
                    return UrlProducao();
            }

            throw new InvalidOperationException("Tipo Ambiente inválido");
        }

        private static UrlCTe UrlProducao()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

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
                        CteConsulta = @"https://cte.sefaz.mt.gov.br/ctews/services/CteConsulta"
                    };
                case Estado.MS:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://producao.cte.ms.gov.br/ws/CteStatusServico",
                        CteRetRecepcao = @"https://producao.cte.ms.gov.br/ws/CteRetRecepcao",
                        CteRecepcao = @"https://producao.cte.ms.gov.br/ws/CteRecepcao",
                        CteInutilizacao = @"https://producao.cte.ms.gov.br/ws/CteInutilizacao",
                        CteRecepcaoEvento = @"https://producao.cte.ms.gov.br/ws/CteRecepcaoEvento",
                        CteConsulta = @"https://producao.cte.ms.gov.br/ws/CteConsulta"
                    };
                case Estado.MG:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://cte.fazenda.mg.gov.br/cte/services/CteStatusServico",
                        CteRetRecepcao = @"https://cte.fazenda.mg.gov.br/cte/services/CteRetRecepcao",
                        CteRecepcao = @"https://cte.fazenda.mg.gov.br/cte/services/CteRecepcao",
                        CteInutilizacao = @"https://cte.fazenda.mg.gov.br/cte/services/CteInutilizacao",
                        CteRecepcaoEvento = @"https://cte.fazenda.mg.gov.br/cte/services/RecepcaoEvento",
                        CteConsulta = @"https://cte.fazenda.mg.gov.br/cte/services/CteConsulta"
                    };
                case Estado.PR:
                    return new UrlCTe
                    {
                        CteStatusServico = @"	https://cte.fazenda.pr.gov.br/cte/CteStatusServico?wsdl",
                        CteRetRecepcao = @"https://cte.fazenda.pr.gov.br/cte/CteRetRecepcao?wsdl",
                        CteRecepcao = @"https://cte.fazenda.pr.gov.br/cte/CteRecepcao?wsdl",
                        CteInutilizacao = @"https://cte.fazenda.pr.gov.br/cte/CteInutilizacao?wsdl",
                        CteRecepcaoEvento = @"https://cte.fazenda.pr.gov.br/cte/CteRecepcaoEvento?wsdl",
                        CteConsulta = @"https://cte.fazenda.pr.gov.br/cte/CteConsulta?wsdl"
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
                        CteConsulta = @"https://cte.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx"
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
                        CteConsulta = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/cteConsulta.asmx"
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
                        CteRetRecepcao = @"https://cte.svrs.rs.gov.br/ws/cteretrecepcao/cteRetRecepcao.asmx"
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
                        CteConsulta = @"https://nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx"
                    };
                default:
                    throw new InvalidOperationException(
                        "Não achei a url do seu estado(uf), tente com outra unidade federativa");
            }

        }

        private static UrlCTe UrlHomologacao()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

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
                        CteConsulta = @"https://homologacao.sefaz.mt.gov.br/ctews/services/CteConsulta"
                    };
                case Estado.MS:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.cte.ms.gov.br/ws/CteStatusServico",
                        CteRetRecepcao = @"https://homologacao.cte.ms.gov.br/ws/CteRetRecepcao",
                        CteRecepcao = @"https://homologacao.cte.ms.gov.br/ws/CteRecepcao",
                        CteInutilizacao = @"https://homologacao.cte.ms.gov.br/ws/CteInutilizacao",
                        CteRecepcaoEvento = @"https://homologacao.cte.ms.gov.br/ws/CteRecepcaoEvento",
                        CteConsulta = @"https://homologacao.cte.ms.gov.br/ws/CteConsulta"
                    };
                case Estado.MG:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://hcte.fazenda.mg.gov.br/cte/services/CteStatusServico?wsdl",
                        CteRetRecepcao = @"https://hcte.fazenda.mg.gov.br/cte/services/CteRetRecepcao?wsdl",
                        CteRecepcao = @"https://hcte.fazenda.mg.gov.br/cte/services/CteRecepcao?wsdl",
                        CteInutilizacao = @"https://hcte.fazenda.mg.gov.br/cte/services/CteInutilizacao?wsdl",
                        CteRecepcaoEvento = @"https://hcte.fazenda.mg.gov.br/cte/services/RecepcaoEvento?wsdl",
                        CteConsulta = @"https://hcte.fazenda.mg.gov.br/cte/services/CteConsulta?wsdl"
                    };
                case Estado.PR:
                    return new UrlCTe
                    {
                        CteStatusServico = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteStatusServico?wsdl",
                        CteRetRecepcao = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteRetRecepcao?wsdl",
                        CteRecepcao = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteRecepcao?wsdl",
                        CteInutilizacao = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteInutilizacao?wsdl",
                        CteRecepcaoEvento = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteRecepcaoEvento?wsdl",
                        CteConsulta = @"https://homologacao.cte.fazenda.pr.gov.br/cte/CteConsulta?wsdl"
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
                        CteConsulta = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteconsulta/CteConsulta.asmx"
                    };
                case Estado.SP:
                    return new UrlCTe
                    {
                        CteStatusServico =
                            @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteStatusServico.asmx",
                        CteRetRecepcao =
                            @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteRetRecepcao.asmx",
                        CteRecepcao = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteRecepcao.asmx",
                        CteInutilizacao =
                            @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteInutilizacao.asmx",
                        CteRecepcaoEvento =
                            @"https://homologacao.nfe.fazenda.sp.gov.br/cteweb/services/cteRecepcaoEvento.asmx",
                        CteConsulta = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/cteConsulta.asmx"
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
                        CteRetRecepcao = @"https://cte-homologacao.svrs.rs.gov.br/ws/cteretrecepcao/CTeRetRecepcao.asmx"
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
                        CteConsulta = @"https://homologacao.nfe.fazenda.sp.gov.br/cteWEB/services/CteConsulta.asmx"
                    };
                default:
                    throw new InvalidOperationException(
                        "Não achei a url do seu estado(uf), tente com outra unidade federativa");
            }
        }
    }
}