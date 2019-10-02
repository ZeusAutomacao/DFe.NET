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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using CTe.Servicos.Enderecos.Helpers;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CTe.Utils.Recepcao;
using DFe.Classes.Flags;
using CTeEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Recepcao
{
    public class ServicoCTeRecepcao
    {
        public event EventHandler<AntesEnviarRecepcao> AntesDeEnviar;

        public retEnviCte CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList, ConfiguracaoServico configuracaoServico = null)
        {
            var enviCte = PreparaEnvioCTe(lote, cteEletronicosList, configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteRecepcao(configuracaoServico);

            OnAntesDeEnviar(enviCte);

            var retornoXml = webService.cteRecepcaoLote(enviCte.CriaRequestWs(configuracaoServico));

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }

        public async Task<retEnviCte> CTeRecepcaoAsync(int lote, List<CTeEletronico> cteEletronicosList, ConfiguracaoServico configuracaoServico = null)
        {
            var enviCte = PreparaEnvioCTe(lote, cteEletronicosList, configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteRecepcao(configuracaoServico);

            OnAntesDeEnviar(enviCte);

            var retornoXml = await webService.cteRecepcaoLoteAsync(enviCte.CriaRequestWs(configuracaoServico));

            var retorno = retEnviCte.LoadXml(retornoXml.OuterXml, enviCte);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }

        private static enviCTe PreparaEnvioCTe(int lote, List<CTeEletronico> cteEletronicosList, ConfiguracaoServico configuracaoServico = null)
        {
            var instanciaConfiguracao = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var enviCte = ClassesFactory.CriaEnviCTe(lote, cteEletronicosList, instanciaConfiguracao);

            if (instanciaConfiguracao.tpAmb == TipoAmbiente.Homologacao)
            {
                foreach (var cte in enviCte.CTe)
                {
                    const string razaoSocial = "CT-E EMITIDO EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                    cte.infCte.rem.xNome = razaoSocial;
                    cte.infCte.dest.xNome = razaoSocial;
                }
            }


            foreach (var cte in enviCte.CTe)
            {
                cte.infCte.ide.tpEmis = instanciaConfiguracao.TipoEmissao;
                cte.Assina(instanciaConfiguracao);
                cte.infCTeSupl = cte.QrCode(instanciaConfiguracao.X509Certificate2, Encoding.UTF8, instanciaConfiguracao.IsAdicionaQrCode, UrlHelper.ObterUrlServico(instanciaConfiguracao).QrCode);
                cte.ValidaSchema(instanciaConfiguracao);
                cte.SalvarXmlEmDisco(instanciaConfiguracao);
            }

            enviCte.ValidaSchema(instanciaConfiguracao);
            enviCte.SalvarXmlEmDisco(instanciaConfiguracao);
            return enviCte;
        }

        protected virtual void OnAntesDeEnviar(enviCTe enviCTe)
        {
            var handler = AntesDeEnviar;
            if (handler != null) handler(this, new AntesEnviarRecepcao(enviCTe));
        }
    }
}