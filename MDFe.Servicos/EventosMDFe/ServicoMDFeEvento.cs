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
/* Rua Comendador Francisco José da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Retorno.MDFeEvento;
using System.Collections.Generic;
using DFe.Classes.Entidades;
using MDFe.Classes.Informacoes;
using MDFe.Utils.Configuracoes;
using MDFeEletronica = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class ServicoMDFeEvento
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(
            MDFeEletronica mdfe, byte sequenciaEvento, string nome,
            string cpf, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoIncluirCondutor = new EventoInclusaoCondutor();

            return eventoIncluirCondutor.MDFeEventoIncluirCondutor(mdfe, sequenciaEvento, nome, cpf, config);
        }

        public MDFeRetEventoMDFe MDFeEventoIncluirDFe(
            MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string codigoMunicipioCarregamento, string nomeMunicipioCarregamento, List<MDFeInfDocInc> informacoesDocumentos,
            MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoIncluirDFe = new EventoInclusaoDFe();

            return eventoIncluirDFe.MDFeEventoIncluirDFe(mdfe, sequenciaEvento, protocolo, codigoMunicipioCarregamento, nomeMunicipioCarregamento, informacoesDocumentos, config);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, sequenciaEvento, protocolo, config);
        }

        public MDFeRetEventoMDFe MDFeEventoEncerramentoMDFeEventoEncerramento(MDFeEletronica mdfe, Estado estadoEncerramento, 
            long codigoMunicipioEncerramento, byte sequenciaEvento, string protocolo, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoEncerramento = new EventoEncerramento();

            return eventoEncerramento.MDFeEventoEncerramento(mdfe, estadoEncerramento, codigoMunicipioEncerramento, sequenciaEvento, protocolo, config);
        }

        public MDFeRetEventoMDFe MDFeEventoCancelar(MDFeEletronica mdfe, byte sequenciaEvento, string protocolo,
            string justificativa, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoCancelamento = new EventoCancelar();

            return eventoCancelamento.MDFeEventoCancelar(mdfe, sequenciaEvento, protocolo, justificativa, config);
        }

        public MDFeRetEventoMDFe MDFeEventoPagamentoOperacaoTransporte(MDFeEletronica mdfe, byte sequenciaEvneto,
            string protocolo, MDFeInfViagens infViagens, List<MDFeInfPag> infPagamentos, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;

            var eventoPagamentoOperacao = new EventoPagamentoOperacao();

            return eventoPagamentoOperacao.MDFeEventoPagamentoOperacao(mdfe, sequenciaEvneto, protocolo,
                infViagens, infPagamentos, config);
        }
    }
}