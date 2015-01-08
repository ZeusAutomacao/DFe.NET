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
namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos
{
    public abstract class ICMSNormalST : ICMSNormal
    {
        /// <summary>
        ///     Modalidade de determinação da BC do ICMS ST:
        ///     <para>0 – Preço tabelado ou máximo  sugerido;</para>
        ///     <para>1 - Lista Negativa (valor);</para>
        ///     <para>2 - Lista Positiva (valor);</para>
        ///     <para>3 - Lista Neutra (valor);</para>
        ///     <para>4 - Margem Valor Agregado (%);</para>
        ///     <para>5 - Pauta (valor);</para>
        /// </summary>
        public int modBCST { get; set; }

        /// <summary>
        ///     Percentual da Margem de Valor Adicionado ICMS ST
        /// </summary>
        public decimal pMVAST { get; set; }

        /// <summary>
        ///     Percentual de redução da BC ICMS ST
        /// </summary>
        public decimal pRedBCST { get; set; }

        /// <summary>
        ///     Valor da BC do ICMS ST
        /// </summary>
        public decimal vBCST { get; set; }

        /// <summary>
        ///     Alíquota do ICMS ST
        /// </summary>
        public decimal pICMSST { get; set; }

        /// <summary>
        ///     Valor do ICMS ST
        /// </summary>
        public decimal vICMSST { get; set; }
    }
}