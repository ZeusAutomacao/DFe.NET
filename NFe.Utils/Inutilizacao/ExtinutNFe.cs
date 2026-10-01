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
using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes.Servicos.Inutilizacao;
using NFe.Utils.Assinatura;

namespace NFe.Utils.Inutilizacao
{
    public static class ExtinutNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto inutNFe
        /// </summary>
        /// <param name="inutNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo inutNFe</returns>
        public static inutNFe CarregarDeXmlString(this inutNFe inutNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<inutNFe>(xmlString);
        }
        
        /// <summary>
        ///     Converte o objeto inutNFe para uma string no formato XML
        /// </summary>
        /// <param name="pedInutilizacao"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto inutNFe</returns>
        public static string ObterXmlString(this inutNFe pedInutilizacao)
        {
            return FuncoesXml.ClasseParaXmlString(pedInutilizacao);
        }

        /// <summary>
        ///     Obtém o Id de um pedido de inutilização (infInut/@Id): literal "ID" + cUF + ano com 2 dígitos + CNPJ +
        ///     modelo + série com 3 dígitos + número inicial e número final com 9 dígitos
        ///     <para>
        ///         Uso opcional, para quando o pedido for montado fora da biblioteca — por exemplo, para assinar com o
        ///         certificado numa máquina cliente e depois transmitir com <see cref="Assina"/> já feito, via a
        ///         sobrecarga que recebe o inutNFe pronto. O método que assina internamente continua calculando o Id
        ///         sozinho.
        ///     </para>
        /// </summary>
        /// <param name="cUF">Código da UF do solicitante</param>
        /// <param name="ano">Ano de inutilização da numeração</param>
        /// <param name="cnpj">CNPJ do emitente</param>
        /// <param name="modelo">Modelo do documento</param>
        /// <param name="serie">Série</param>
        /// <param name="numeroInicial">Número inicial a ser inutilizado</param>
        /// <param name="numeroFinal">Número final a ser inutilizado</param>
        /// <returns>Retorna o conteúdo do atributo infInut/@Id</returns>
        public static string ObterId(Estado cUF, int ano, string cnpj, ModeloDocumento modelo, int serie,
            int numeroInicial, int numeroFinal)
        {
            var numId = string.Concat((int)cUF, ano.ToString("D2"),
                cnpj, (int)modelo,
                serie.ToString().PadLeft(3, '0'),
                numeroInicial.ToString().PadLeft(9, '0'),
                numeroFinal.ToString().PadLeft(9, '0'));

            return "ID" + numId;
        }

        /// <summary>
        ///     Assina um objeto inutNFe
        /// </summary>
        /// <param name="inutNFe"></param>
        /// <param name="certificadoDigital">Informe o certificado digital, se já possuir esse em cache, evitando novo acesso ao certificado</param>
        /// <returns>Retorna um objeto do tipo inutNFe assinado</returns>
        public static inutNFe Assina(this inutNFe inutNFe, X509Certificate2 certificadoDigital, string signatureMethodSignedXml = "http://www.w3.org/2000/09/xmldsig#rsa-sha1", string digestMethodReference = "http://www.w3.org/2000/09/xmldsig#sha1", bool removerAcentos = false)
        {
            var inutNFeLocal = inutNFe;
            if (inutNFeLocal.infInut.Id == null)
                throw new Exception("Não é possível assinar um onjeto inutNFe sem sua respectiva Id!");

            var assinatura = Assinador.ObterAssinatura(inutNFeLocal, inutNFeLocal.infInut.Id, certificadoDigital, false, signatureMethodSignedXml, digestMethodReference, removerAcentos);
            inutNFeLocal.Signature = assinatura;
            return inutNFeLocal;
        }
    }
}