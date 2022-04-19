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
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual
{
    public class ICMS
    {
        /// <summary>
        ///     <para>N02 (ICMS00) - Grupo Tributação do ICMS= 00</para>
        ///     <para>N03 (ICMS10) - Grupo Tributação do ICMS = 10 </para>
        ///     <para>N04 (ICMS20) - Grupo Tributação do ICMS = 20</para>
        ///     <para>N05 (ICMS30) - Grupo Tributação do ICMS = 30</para>
        ///     <para>N06 (ICMS40) - Grupo Tributação ICMS = 40, 41, 50</para>
        ///     <para>N07 (ICMS51) - Grupo Tributação do ICMS = 51</para>
        ///     <para>N08 (ICMS60) - Grupo Tributação do ICMS = 60</para>
        ///     <para>N09 (ICMS70) - Grupo Tributação do ICMS = 70</para>
        ///     <para>N10 (ICMS90) - Grupo Tributação do ICMS = 90</para>
        ///     <para>
        ///         N10a (ICMSPart) - Grupo de Partilha do ICMS entre a UF de origem e UF de destino ou a UF definida na
        ///         legislação.
        ///     </para>
        ///     <para>
        ///         N10b (ICMSST) - Grupo de Repasse de ICMS ST retido anteriormente em operações interestaduais com repasses
        ///         através do Substituto Tributário
        ///     </para>
        ///     <para>N10c (ICMSSN101) - Grupo CRT=1 – Simples Nacional e CSOSN=101</para>
        ///     <para>N10d (ICMSSN102) - Grupo CRT=1 – Simples Nacional e CSOSN=102, 103, 300 ou 400</para>
        ///     <para>N10e (ICMSSN201) - Grupo CRT=1 – Simples Nacional e CSOSN=201</para>
        ///     <para>N10f (ICMSSN202) - Grupo CRT=1 – Simples Nacional e CSOSN=202 ou 203</para>
        ///     <para>N10g (ICMSSN500) - Grupo CRT=1 – Simples Nacional e CSOSN = 500</para>
        ///     <para>N10h (ICMSSN900) - Grupo CRT=1 – Simples Nacional e CSOSN=900</para>
        /// </summary>
        [XmlElement("ICMS00", typeof (ICMS00))]
        [XmlElement("ICMS10", typeof (ICMS10))]
        [XmlElement("ICMS20", typeof (ICMS20))]
        [XmlElement("ICMS30", typeof (ICMS30))]
        [XmlElement("ICMS40", typeof (ICMS40))]
        [XmlElement("ICMS51", typeof (ICMS51))]
        [XmlElement("ICMS60", typeof (ICMS60))]
        [XmlElement("ICMS70", typeof (ICMS70))]
        [XmlElement("ICMS90", typeof (ICMS90))]
        [XmlElement("ICMSPart", typeof (ICMSPart))]
        [XmlElement("ICMSST", typeof (ICMSST))]
        [XmlElement("ICMSSN101", typeof (ICMSSN101))]
        [XmlElement("ICMSSN102", typeof (ICMSSN102))]
        [XmlElement("ICMSSN201", typeof (ICMSSN201))]
        [XmlElement("ICMSSN202", typeof (ICMSSN202))]
        [XmlElement("ICMSSN500", typeof (ICMSSN500))]
        [XmlElement("ICMSSN900", typeof (ICMSSN900))]
        public ICMSBasico TipoICMS { get; set; }
    }
}