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
using DFe.Classes.Entidades;
using static SOAP.Handler.Enums.Enums;

namespace SOAP.Handler.Body
{
    public static class ExTagCorpoParametroEntrada
    {
        public static string GetParametroDeEntradaWsdl(this Estado estado, bool compactar, TipoRequisicao tipo)
        {
            var mensagem = GetTipoCorpo(tipo);
            switch (estado)
            {
                case Estado.AC:
                case Estado.AL:
                case Estado.AP:
                case Estado.AM:
                case Estado.BA:
                case Estado.CE:
                case Estado.DF:
                case Estado.ES:
                case Estado.MA:
                case Estado.MT:
                case Estado.MS:
                case Estado.MG:
                case Estado.PA:
                case Estado.PB:
                case Estado.PR:
                case Estado.PE:
                case Estado.PI:
                case Estado.RJ:
                case Estado.RN:
                case Estado.RS:
                case Estado.RO:
                case Estado.RR:
                case Estado.SC:
                case Estado.SP:
                case Estado.SE:
                case Estado.TO:
                case Estado.AN:
                case Estado.EX:
                case Estado.GO:
                    return compactar ? ""+mensagem+"Zip" : mensagem;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Estado), estado, null);
            }
        }

        private static string GetTipoCorpo(TipoRequisicao tipo)
        {
            switch (tipo)
            {
                case TipoRequisicao.MDFe:
                    return "mdfeDadosMsg";
                case TipoRequisicao.NFe:
                    return "nfeDadosMsg";
                case TipoRequisicao.CTe:
                    return "cteDadosMsg";
                default:
                    throw new ArgumentOutOfRangeException(nameof(TipoRequisicao), tipo, null);

            }
        }
    }
}
