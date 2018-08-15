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
using NFe.Classes.Servicos.Inutilizacao;

namespace NFe.Utils.Inutilizacao
{
    public static class ExtretInutNFe
    {
        /// <summary>
        ///     Coverte uma string XML no formato NFe para um objeto retInutNFe
        /// </summary>
        /// <param name="retInutNFe"></param>
        /// <param name="xmlString"></param>
        /// <returns>Retorna um objeto do tipo retInutNFe</returns>
        public static retInutNFe CarregarDeXmlString(this retInutNFe retInutNFe, string xmlString)
        {
            return FuncoesXml.XmlStringParaClasse<retInutNFe>(xmlString);
        }

        /// <summary>
        ///     Converte o objeto retInutNFe para uma string no formato XML
        /// </summary>
        /// <param name="retInutNFe"></param>
        /// <returns>Retorna uma string no formato XML com os dados do objeto retInutNFe</returns>
        public static string ObterXmlString(this retInutNFe retInutNFe)
        {
            return FuncoesXml.ClasseParaXmlString(retInutNFe);
        }
    }
}