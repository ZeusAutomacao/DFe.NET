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
using DFe.Utils;
using NFe.Classes.Servicos.Evento;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Assinatura;
using Shared.DFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace NFe.Utils.Evento
{
    public static class Extevento
    {
        /// <summary>
        ///     Converte o objeto evento para uma string no formato XML
        /// </summary>
        /// <param name="pedEvento"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto evento</returns>
        public static string ObterXmlString(this evento pedEvento)
        {
            return FuncoesXml.ClasseParaXmlString(pedEvento);
        }

        /// <summary>
        ///     Obtém o Id de um evento (infEvento/@Id): literal "ID" + tpEvento + chNFe + nSeqEvento com 2 dígitos
        ///     <para>
        ///         Uso opcional, para quando o evento for montado fora da biblioteca — por exemplo, para assinar com o
        ///         certificado numa máquina cliente e depois transmitir com a sobrecarga que recebe o evento já
        ///         assinado. Os métodos que assinam internamente continuam calculando o Id sozinhos.
        ///     </para>
        /// </summary>
        /// <param name="tpEvento">Código do evento</param>
        /// <param name="chNFe">Chave de acesso da NF-e vinculada ao evento</param>
        /// <param name="nSeqEvento">Sequencial do evento para o mesmo tipo de evento</param>
        /// <returns>Retorna o conteúdo do atributo infEvento/@Id</returns>
        public static string ObterId(NFeTipoEvento tpEvento, string chNFe, int nSeqEvento)
        {
            return "ID" + ((int)tpEvento) + chNFe + nSeqEvento.ToString().PadLeft(2, '0');
        }

        /// <summary>
        ///     Assina um objeto evento
        /// </summary>
        /// <param name="evento"></param>
        /// <param name="certificadoDigital">Informe o certificado digital, se já possuir esse em cache, evitando novo acesso ao certificado</param>
        /// <param name="signatureMethodSignedXml"></param>
        /// <param name="digestMethodReference"></param>
        /// <param name="removerAcentos"></param>
        /// <returns>Retorna um objeto do tipo evento assinado</returns>
        public static evento Assina(this evento evento, X509Certificate2 certificadoDigital,
            string signatureMethodSignedXml = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
            string digestMethodReference = "http://www.w3.org/2000/09/xmldsig#sha1", bool removerAcentos = false)
        {
            var eventoLocal = evento;
            if (eventoLocal.infEvento.Id == null)
                throw new Exception("Não é possível assinar um objeto evento sem sua respectiva Id!");

            var assinatura = Assinador.ObterAssinatura(eventoLocal, eventoLocal.infEvento.Id, certificadoDigital, false, signatureMethodSignedXml, digestMethodReference, removerAcentos);
            eventoLocal.Signature = assinatura;
            return eventoLocal;
        }

        /// <summary>
        ///     descEvento que o XSD do evento só aceita com acento, indexados pela forma sem acento.
        ///     Os demais eventos (ex.: Carta de Correção, cujo XSD aceita as duas formas) continuam sem acento.
        /// </summary>
        private static readonly Dictionary<string, string> DescEventoSomenteComAcento = new[]
        {
            NFeTipoEvento.TeNfeCancConciliacaoFinanceiraNFe,
            NFeTipoEvento.TeNfeInformacaoDeEfetivoPagamentoIntegralParaLiberarCreditoPresumidoDoAdquirente,
            NFeTipoEvento.TeNfeImportacaoEmAlcZfmNaoConvertidaEmIsencao,
            NFeTipoEvento.TeNfeFornecimentoNaoRealizadoComPagamentoAntecipado,
            NFeTipoEvento.TeNfeAtualizacaoDaDataDePrevisaoDeEntrega,
            NFeTipoEvento.TeNfeSolicitacaoDeApropriacaoDeCreditoPresumido,
            NFeTipoEvento.TeNfeDestinacaoDeItemParaConsumoPessoal,
            NFeTipoEvento.TeNfeAceiteDeDebitoNaApuracaoPorEmissaoDeNotaDeCredito,
            NFeTipoEvento.TeNfeImobilizacaoDeItem,
            NFeTipoEvento.TeNfeSolicitacaoApropriacaoCreditoCombustivel,
            NFeTipoEvento.TeNfeSolicitacaoDeApropriacaoDeCreditoParaBensEServicosQueDependemDeAtividadeDoAdquirente,
            NFeTipoEvento.TeNfeManifestacaoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao,
            NFeTipoEvento.TeNfeManifestacaoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao,
            NFeTipoEvento.TeNfeManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeIbsEmOperacoesDeSucessao,
            NFeTipoEvento.TeNfeManifestacaoDoFiscoSobrePedidoDeTransferenciaDeCreditoDeCbsEmOperacoesDeSucessao
        }.Select(t => t.Descricao()).ToDictionary(d => d.RemoverAcentos(), d => d);

        /// <summary>
        ///     xCondUso que o XSD do evento só aceita com acento, indexados pela forma sem acento:
        ///     110150 (Ator interessado na NF-e), quando o autor é o emitente ou o destinatário.
        ///     A Carta de Correção não entra, porque o XSD dela aceita as duas formas.
        /// </summary>
        private static readonly Dictionary<string, string> XCondUsoSomenteComAcento = new[]
        {
            "O emitente ou destinatário da NF-e, declara que permite o transportador declarado no campo CNPJ/CPF deste evento a autorizar os transportadores subcontratados ou redespachados a terem acesso ao download da NF-e"
        }.ToDictionary(x => x.RemoverAcentos(), x => x);

        /// <summary>
        ///     Remove os acentos do XML, mantendo o descEvento e o xCondUso dos eventos cujo XSD só aceita o texto acentuado
        ///     (ex.: descEvento "Imobilização de Item" e xCondUso do evento 110150)
        /// </summary>
        /// <param name="xml">XML do evento</param>
        /// <returns>XML sem acentos, exceto no descEvento e no xCondUso dos eventos que exigem acentuação</returns>
        public static string RemoverAcentosPreservandoDescEvento(this string xml)
        {
            var semAcentos = xml.RemoverAcentos();
            if (string.IsNullOrEmpty(semAcentos) || (semAcentos.IndexOf("<descEvento>", StringComparison.Ordinal) < 0 && semAcentos.IndexOf("<xCondUso>", StringComparison.Ordinal) < 0))
                return semAcentos;

            semAcentos = RestaurarTextoAcentuado(semAcentos, "descEvento", DescEventoSomenteComAcento);
            return RestaurarTextoAcentuado(semAcentos, "xCondUso", XCondUsoSomenteComAcento);
        }

        private static string RestaurarTextoAcentuado(string xml, string tag, IReadOnlyDictionary<string, string> textosComAcento)
        {
            return Regex.Replace(xml, "<" + tag + ">([^<]*)</" + tag + ">", m =>
            {
                string texto;

                return textosComAcento.TryGetValue(m.Groups[1].Value, out texto)
                    ? "<" + tag + ">" + texto + "</" + tag + ">"
                    : m.Value;
            });
        }
    }
}