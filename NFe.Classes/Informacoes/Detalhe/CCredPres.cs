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

using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe
{
    /// <summary>
    ///     <para>01 - Crédito presumido da aquisição de bens e serviços de produtor rural e produtor rural integrado não contribuinte, observado o art. 168 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>02 - Crédito presumido da aquisição de serviço de transportador autônomo de carga pessoa física não contribuinte, observado o art. 169 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>03 - Crédito presumido da aquisição de resíduos e demais materiais destinados à reciclagem, reutilização ou logística reversa adquiridos de pessoa física, cooperativa ou outra forma de organização popular, observado o art. 170 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>04 - Crédito presumido da aquisição de bens móveis usados de pessoa física não contribuinte para revenda, observado o art. 171 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>05 - Crédito presumido no regime automotivo, observado o art. 311 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>06 - Crédito presumido no regime automotivo, observado o art. 312 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>07 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 444 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>08 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 447 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>09 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 449 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>10 - Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 450 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>11 - Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 462 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>12 - Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 465 da Lei Complementar nº 214, de 2025.</para>
    ///     <para>13 - Crédito presumido na aquisição pela indústria na Área de Livre Comércio, observado o art. 467 da Lei Complementar nº 214, de 2025.</para>
    /// </summary>
    public enum CCredPres
    {
        [Description("Crédito presumido da aquisição de bens e serviços de produtor rural e produtor rural integrado não contribuinte, observado o art. 168 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("01")]
        CCP01,
        
        [Description("Crédito presumido da aquisição de serviço de transportador autônomo de carga pessoa física não contribuinte, observado o art. 169 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("02")]
        CCP02,
        
        [Description("Crédito presumido da aquisição de resíduos e demais materiais destinados à reciclagem, reutilização ou logística reversa adquiridos de pessoa física, cooperativa ou outra forma de organização popular, observado o art. 170 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("03")]
        CCP03,
        
        [Description("Crédito presumido da aquisição de bens móveis usados de pessoa física não contribuinte para revenda, observado o art. 171 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("04")]
        CCP04,
        
        [Description("Crédito presumido no regime automotivo, observado o art. 311 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("05")]
        CCP05,
        
        [Description("Crédito presumido no regime automotivo, observado o art. 312 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("06")]
        CCP06,
        
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 444 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("07")]
        CCP07,
        
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 447 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("08")]
        CCP08,
        
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 449 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("09")]
        CCP09,
        
        [Description("Crédito presumido na aquisição por contribuinte na Zona Franca de Manaus, observado o art. 450 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("10")]
        CCP10,
        
        [Description("Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 462 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("11")]
        CCP11,
        
        [Description("Crédito presumido na aquisição por contribuinte na Área de Livre Comércio, observado o art. 465 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("12")]
        CCP12,
        
        [Description("Crédito presumido na aquisição pela indústria na Área de Livre Comércio, observado o art. 467 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("13")]
        CCP13,
    }
}