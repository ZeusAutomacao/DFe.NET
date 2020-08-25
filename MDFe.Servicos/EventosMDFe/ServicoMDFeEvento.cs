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

using System.Collections.Generic;
using System.Threading.Tasks;
using DFe.Classes.Entidades;
using MDFe.Classes.Informacoes;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Utils.Configuracoes;
using MDFeEletronica = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoMDFeEvento
    {
        public async Task<MDFeRetEventoMDFe> MDFeEventoIncluirCondutor(
            MDFeEletronica mdfe, byte sequenciaEvento, string nome,
            string cpf, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoIncluirCondutor = new EventoInclusaoCondutor();

            return await eventoIncluirCondutor.MDFeEventoIncluirCondutor(mdfe, sequenciaEvento, nome, cpf, config);
        }

        public async Task<MDFeRetEventoMDFe> MDFeEventoIncluirDFe(
            MDFeEletronica mdfe, byte sequenciaEvento, string protocolo, string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, 
            List<MDFeInfDocInc> informacoesDocumentos, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoIncluirDFe = new EventoInclusaoDFe();

            return await eventoIncluirDFe.MDFeEventoIncluirDFe(mdfe, sequenciaEvento, protocolo, codigoMunicipioCarregamento,
                nomeMunicipioCarregamento, informacoesDocumentos, config);
        }

        public async Task<MDFeRetEventoMDFe> MDFeEventoPagamentoOperacaoTransporte(MDFeEletronica mdfe,
          byte sequenciaEvento, string protocolo, List<MDFeInfPag> informacoesPagamento, MDFeInfViagens informacoesViagens, 
          MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var eventoOperacaoPagamento = new EventoPagamentoOperacao();

            return await eventoOperacaoPagamento.MDFeEventoPagamentoOperacaoMDFe(mdfe, sequenciaEvento, protocolo, 
                informacoesPagamento, informacoesViagens, config);
        }

        public async Task<MDFeRetEventoMDFe> MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, Estado cUF, long cMun, 
            byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoEncerramento = new EventoEncerramento();

            return await eventoEncerramento.MDFeEventoEncerramento(mdfe, cUF, cMun, sequenciaEvento, protocolo, config);
        }

        public async Task<MDFeRetEventoMDFe> MDFeEventoCancelar(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string justificativa, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoCancelamento = new EventoCancelar();

            return await eventoCancelamento.MDFeEventoCancelar(mdfe, sequenciaEvento, protocolo, justificativa, config);
        }
    }
}