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
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CteEletronico = CTe.Classes.CTe;

namespace CTe.Servicos.Eventos
{
    public class EventoCancelamento
    {
        private readonly CteEletronico _cte;
        private readonly int _sequenciaEvento;
        private readonly string _numeroProtocolo;
        private readonly string _justificativa;
       
        public eventoCTe EventoEnviado { get; private set; }
        public retEventoCTe RetornoSefaz { get; private set; }
        
        public EventoCancelamento(CteEletronico cte, int sequenciaEvento, string numeroProtocolo, string justificativa)
        {
            _cte = cte;
            _sequenciaEvento = sequenciaEvento;
            _numeroProtocolo = numeroProtocolo;
            _justificativa = justificativa;
        }

        public retEventoCTe Cancelar(ConfiguracaoServico configuracaoServico = null)
        {
            var evento = ClassesFactory.CriaEvCancCTe(_justificativa, _numeroProtocolo);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Cancelamento, _sequenciaEvento, _cte.Chave(), _cte.infCte.emit.CNPJ, evento, configuracaoServico);
            RetornoSefaz = new ServicoController().Executar(_cte, _sequenciaEvento, evento, CTeTipoEvento.Cancelamento, configuracaoServico);

            return RetornoSefaz;
        }

        public async Task<retEventoCTe> CancelarAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var evento = ClassesFactory.CriaEvCancCTe(_justificativa, _numeroProtocolo);

            EventoEnviado = FactoryEvento.CriaEvento(CTeTipoEvento.Cancelamento, _sequenciaEvento, _cte.Chave(), _cte.infCte.emit.CNPJ, evento, configuracaoServico);
            RetornoSefaz = await new ServicoController().ExecutarAsync(_cte, _sequenciaEvento, evento, CTeTipoEvento.Cancelamento, configuracaoServico);

            return RetornoSefaz;
        }
    }
}