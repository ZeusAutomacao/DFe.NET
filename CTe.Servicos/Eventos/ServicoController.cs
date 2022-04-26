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

using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Servicos.Eventos.Contratos;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CTe.Utils.Evento;
using CteEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Eventos
{
    public class ServicoController : IServicoController
    {
        public retEventoCTe Executar(CteEletronico cte, int sequenciaEvento, EventoContainer container, CTeTipoEvento cTeTipoEvento, ConfiguracaoServico configuracaoServico = null)
        {
            return Executar(cTeTipoEvento, sequenciaEvento, cte.Chave(), cte.infCte.emit.CNPJ, container, configuracaoServico);
        }

        public async Task<retEventoCTe> ExecutarAsync(CteEletronico cte, int sequenciaEvento, EventoContainer container, CTeTipoEvento cTeTipoEvento, ConfiguracaoServico configuracaoServico = null)
        {
            return await ExecutarAsync(cTeTipoEvento, sequenciaEvento, cte.Chave(), cte.infCte.emit.CNPJ, container, configuracaoServico);
        }

        public retEventoCTe Executar(CTeTipoEvento cTeTipoEvento, int sequenciaEvento, string chave, string cnpj, EventoContainer container, ConfiguracaoServico configuracaoServico = null)
        {
            var evento = FactoryEvento.CriaEvento(cTeTipoEvento, sequenciaEvento, chave, cnpj, container, configuracaoServico);
            evento.Assina(configuracaoServico);
            evento.ValidarSchema(configuracaoServico);
            evento.SalvarXmlEmDisco(configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteEvento(configuracaoServico);
            var retornoXml = webService.cteRecepcaoEvento(evento.CriaXmlRequestWs());

            var retorno = retEventoCTe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }

        public async Task<retEventoCTe> ExecutarAsync(CTeTipoEvento cTeTipoEvento, int sequenciaEvento, string chave, string cnpj, EventoContainer container, ConfiguracaoServico configuracaoServico = null)
        {
            var evento = FactoryEvento.CriaEvento(cTeTipoEvento, sequenciaEvento, chave, cnpj, container, configuracaoServico);
            evento.Assina(configuracaoServico);
            evento.ValidarSchema(configuracaoServico);
            evento.SalvarXmlEmDisco(configuracaoServico);

            var webService = WsdlFactory.CriaWsdlCteEvento(configuracaoServico);
            var retornoXml = await webService.cteRecepcaoEventoAsync(evento.CriaXmlRequestWs());

            var retorno = retEventoCTe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(configuracaoServico);

            return retorno;
        }
    }
}