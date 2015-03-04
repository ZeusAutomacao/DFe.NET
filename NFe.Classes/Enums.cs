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
using System.Xml.Serialization;

namespace NFe.Classes
{
    public enum Estado
    {
        [XmlEnum("12")] AC = 12,
        [XmlEnum("27")] AL = 27,
        [XmlEnum("13")] AM = 13,
        [XmlEnum("16")] AP = 16,
        [XmlEnum("29")] BA = 29,
        [XmlEnum("23")] CE = 23,
        [XmlEnum("53")] DF = 53,
        [XmlEnum("32")] ES = 32,
        [XmlEnum("52")] GO = 52,
        [XmlEnum("21")] MA = 21,
        [XmlEnum("31")] MG = 31,
        [XmlEnum("50")] MS = 50,
        [XmlEnum("51")] MT = 51,
        [XmlEnum("15")] PA = 15,
        [XmlEnum("25")] PB = 25,
        [XmlEnum("26")] PE = 26,
        [XmlEnum("22")] PI = 22,
        [XmlEnum("41")] PR = 41,
        [XmlEnum("33")] RJ = 33,
        [XmlEnum("24")] RN = 24,
        [XmlEnum("11")] RO = 11,
        [XmlEnum("14")] RR = 14,
        [XmlEnum("43")] RS = 43,
        [XmlEnum("42")] SC = 42,
        [XmlEnum("28")] SE = 28,
        [XmlEnum("35")] SP = 35,
        [XmlEnum("17")] TO = 17,
        [XmlEnum("91")] AN = 91
    }
}