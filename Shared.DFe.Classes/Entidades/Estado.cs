/********************************************************************************/
/* Projeto: Biblioteca ZeusDFe                                                  */
/* Biblioteca C# para auxiliar no desenvolvimento das demais bibliotecas DFe    */
/*                                                                              */
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
using System.Xml.Serialization;

namespace DFe.Classes.Entidades
{
    public enum Estado
    {
        /// <summary>
        /// Acre
        /// </summary>
        [XmlEnum("12")]
        AC = 12,

        /// <summary>
        /// Alagoas
        /// </summary>
        [XmlEnum("27")]
        AL = 27,

        /// <summary>
        /// Amapá
        /// </summary>
        [XmlEnum("16")]
        AP = 16,

        /// <summary>
        /// Amazonas
        /// </summary>
        [XmlEnum("13")]
        AM = 13,

        /// <summary>
        /// Bahia
        /// </summary>
        [XmlEnum("29")]
        BA = 29,

        /// <summary>
        /// Ceará
        /// </summary>
        [XmlEnum("23")]
        CE = 23,

        /// <summary>
        /// Distrito Federal
        /// </summary>
        [XmlEnum("53")]
        DF = 53,

        /// <summary>
        /// Espírito Santo
        /// </summary>
        [XmlEnum("32")]
        ES = 32,

        /// <summary>
        /// Goiás
        /// </summary>
        [XmlEnum("52")]
        GO = 52,

        /// <summary>
        /// Maranhão
        /// </summary>
        [XmlEnum("21")]
        MA = 21,

        /// <summary>
        /// Mato Grosso
        /// </summary>
        [XmlEnum("51")]
        MT = 51,

        /// <summary>
        /// Mato Grosso do Sul
        /// </summary>
        [XmlEnum("50")]
        MS = 50,

        /// <summary>
        /// Minas Gerais
        /// </summary>
        [XmlEnum("31")]
        MG = 31,

        /// <summary>
        /// Pará
        /// </summary>
        [XmlEnum("15")]
        PA = 15,

        /// <summary>
        /// Paraíba
        /// </summary>
        [XmlEnum("25")]
        PB = 25,

        /// <summary>
        /// Paraná
        /// </summary>
        [XmlEnum("41")]
        PR = 41,

        /// <summary>
        /// Pernambuco
        /// </summary>
        [XmlEnum("26")]
        PE = 26,

        /// <summary>
        /// Piauí
        /// </summary>
        [XmlEnum("22")]
        PI = 22,

        /// <summary>
        /// Rio de Janeiro
        /// </summary>
        [XmlEnum("33")]
        RJ = 33,

        /// <summary>
        /// Rio Grande do Norte
        /// </summary>
        [XmlEnum("24")]
        RN = 24,

        /// <summary>
        /// Rio Grande do Sul
        /// </summary>
        [XmlEnum("43")]
        RS = 43,

        /// <summary>
        /// Rondônia
        /// </summary>
        [XmlEnum("11")]
        RO = 11,

        /// <summary>
        /// Roraima
        /// </summary>
        [XmlEnum("14")]
        RR = 14,

        /// <summary>
        /// Santa Catarina
        /// </summary>
        [XmlEnum("42")]
        SC = 42,

        /// <summary>
        /// São Paulo
        /// </summary>
        [XmlEnum("35")]
        SP = 35,

        /// <summary>
        /// Sergipe
        /// </summary>
        [XmlEnum("28")]
        SE = 28,

        /// <summary>
        /// Tocantins
        /// </summary>
        [XmlEnum("17")]
        TO = 17,

        /// <summary>
        /// Ambiente nacional
        /// </summary>
        [XmlEnum("91")]
        AN = 91,

        /// <summary>
        /// Exterior
        /// </summary>
        [XmlEnum("99")]
        EX = 99
    }
}