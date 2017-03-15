﻿/********************************************************************************/
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
using NFe.Classes.Servicos.Consulta;

namespace NFe.Utils.Consulta
{
    public static class ExtprocEventoNFe
    {
        /// <summary>
        ///     Converte o objeto procEventoNFe para uma string no formato XML
        /// </summary>
        /// <param name="procEventoNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto procEventoNFe</returns>
        public static string ObterXmlString(this procEventoNFe procEventoNFe)
        {
            return FuncoesXml.ClasseParaXmlString(procEventoNFe);
        }

        /// <summary>
        ///     Coverte uma string XML no formato procEventoNFe para um objeto procEventoNFe
        /// </summary>
        /// <param name="procEventoNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo procEventoNFe</returns>
        public static procEventoNFe CarregarDeXmlString(this procEventoNFe procEventoNFe, string xmlString)
        {
            var s = FuncoesXml.ObterNodeDeStringXml(typeof(procEventoNFe).Name, xmlString);
            return FuncoesXml.XmlStringParaClasse<procEventoNFe>(s);
        }
    }
}