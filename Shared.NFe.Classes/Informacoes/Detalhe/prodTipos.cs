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
    ///     <para>0 – o valor do item (vProd) não compõe o valor total da NF-e (vProd)</para>
    ///     <para>1  – o valor do item (vProd) compõe o valor total da NF-e (vProd)</para>
    /// </summary>
    public enum IndicadorTotal
    {
        /// <summary>
        /// 0 – o valor do item (vProd) não compõe o valor total da NF-e (vProd)
        /// </summary>
        [Description("Valor do item não compõe o total da NF-e")]
        [XmlEnum("0")]
        ValorDoItemNaoCompoeTotalNF = 0,

        /// <summary>
        /// 1  – o valor do item (vProd) compõe o valor total da NF-e (vProd)
        /// </summary>
        [Description("Valor do item compõe o total da NF-e")]
        [XmlEnum("1")]
        ValorDoItemCompoeTotalNF = 1
    }

    /// <summary>
    ///     <para>1=Marítima;</para>
    ///     <para>2=Fluvial;</para>
    ///     <para>3=Lacustre;</para>
    ///     <para>4=Aérea;</para>
    ///     <para>5=Postal</para>
    ///     <para>6=Ferroviária;</para>
    ///     <para>7=Rodoviária;</para>
    ///     <para>8=Conduto / Rede Transmissão;</para>
    ///     <para>9=Meios Próprios;</para>
    ///     <para>10=Entrada / Saída ficta; 11=Courier; 12=Handcarry. (NT 2013/005 v 1.10).</para>
    /// </summary>
    public enum TipoTransporteInternacional
    {
        /// <summary>
        /// 1=Marítima
        /// </summary>
        [Description("Marítima")]
        [XmlEnum("1")]
        Maritima = 1,

        /// <summary>
        /// 2=Fluvial
        /// </summary>
        [Description("Fluvial")]
        [XmlEnum("2")]
        Fluvial = 2,

        /// <summary>
        /// 3=Lacustre
        /// </summary>
        [Description("Lacustre")]
        [XmlEnum("3")]
        Lacustre = 3,

        /// <summary>
        /// 4=Aérea
        /// </summary>
        [Description("Aérea")]
        [XmlEnum("4")]
        Aerea = 4,

        /// <summary>
        /// 5=Postal
        /// </summary>
        [Description("Postal")]
        [XmlEnum("5")]
        Postal = 5,

        /// <summary>
        /// 6=Ferroviária
        /// </summary>
        [Description("Ferroviária")]
        [XmlEnum("6")]
        Ferroviaria = 6,

        /// <summary>
        /// 7=Rodoviária
        /// </summary>
        [Description("Rodoviária")]
        [XmlEnum("7")]
        Rodoviaria = 7,

        /// <summary>
        /// 8=Conduto / Rede de Transmissão
        /// </summary>
        [Description("Conduto / Rede de Transmissão")]
        [XmlEnum("8")]
        CondutoRedeTransmissão = 8,

        /// <summary>
        /// 9=Meios Próprios
        /// </summary>
        [Description("Meios próprios")]
        [XmlEnum("9")]
        MeiosProprios = 9,

        /// <summary>
        /// 10=Entrada / Saída ficta
        /// </summary>
        [Description("Entrada / Saída ficta")]
        [XmlEnum("10")]
        EntradaSaidaficta = 10,

        /// <summary>
        /// 11=Courier
        /// </summary>
        [Description("Courier")]
        [XmlEnum("11")]
        Courier = 11,

        /// <summary>
        /// 12=Handcarry (NT 2013/005 v 1.10)
        /// </summary>
        [Description("Handcarry")]
        [XmlEnum("12")]
        Handcarry = 12
    }

    /// <summary>
    ///     <para>1=Importação por conta própria;</para>
    ///     <para>2=Importação por conta e ordem;</para>
    ///     <para>3=Importação por encomenda;</para>
    /// </summary>
    public enum TipoIntermediacao
    {
        /// <summary>
        /// 1=Importação por conta própria
        /// </summary>
        [Description("")]
        [XmlEnum("1")]
        ContaPropria = 1,

        /// <summary>
        /// 2=Importação por conta e ordem
        /// </summary>
        [Description("")]
        [XmlEnum("2")]
        ContaeOrdem = 2,

        /// <summary>
        /// 3=Importação por encomenda
        /// </summary>
        [Description("")]
        [XmlEnum("3")]
        PorEncomenda = 3
    }

    /// <summary>
    ///     <para>0=Outros</para>
    ///     <para>1=Venda concessionária,</para>
    ///     <para>2=Faturamento direto para consumidor final</para>
    ///     <para>3=Venda direta para grandes consumidores (frotista, governo, ...)</para>
    /// </summary>
    public enum TipoOperacao
    {
        /// <summary>
        /// 0=Outros
        /// </summary>
        [Description("Outros")]
        [XmlEnum("0")]
        Outros = 0,

        /// <summary>
        /// 1=Venda concessionária
        /// </summary>
        [Description("Venda concessionária")]
        [XmlEnum("1")]
        VendaConcessionaria = 1,

        /// <summary>
        /// 2=Faturamento direto para consumidor final
        /// </summary>
        [Description("Faturamento direto para consumidor final")]
        [XmlEnum("2")]
        FaturamentodiretoparaconsumidorFinal = 2,

        /// <summary>
        /// 3=Venda direta para grandes consumidores (frotista, governo, ...)
        /// </summary>
        [Description("Venda direta para grandes consumidores (frotista, governo, ...)")]
        [XmlEnum("3")]
        VendaDiretaParaGrandesConsumidores = 3
    }

    /// <summary>
    ///     <para>Informa-se o veículo tem VIN (chassi) remarcado.</para>
    ///     <para>R=Remarcado;</para>
    ///     <para>N=Normal</para>
    /// </summary>
    public enum CondicaoVin
    {
        /// <summary>
        /// R=Remarcado
        /// </summary>
        [Description("Remarcado")]
        [XmlEnum("R")]
        Remarcado = 'R',

        /// <summary>
        /// N=Normal
        /// </summary>
        [Description("Normal")]
        [XmlEnum("N")]
        Normal = 'N'
    }

    /// <summary>
    ///     <para>1=Acabado;</para>
    ///     <para>2=Inacabado;</para>
    ///     <para>3=Semiacabado</para>
    /// </summary>
    public enum CondicaoVeiculo
    {
        /// <summary>
        /// 1=Acabado
        /// </summary>
        [Description("Acabado")]
        [XmlEnum("1")]
        Acabado = 1,

        /// <summary>
        /// 2=Inacabado
        /// </summary>
        [Description("Inacabado")]
        [XmlEnum("2")]
        Inacabado = 2,

        /// <summary>
        /// >3=Semiacabado
        /// </summary>
        [Description("Semi-acabado")]
        [XmlEnum("3")]
        Semiacabado = 3
    }

    /// <summary>
    ///     <para>0=Não há;</para>
    ///     <para>1=Alienação Fiduciária;</para>
    ///     <para>2=Arrendamento Mercantil;</para>
    ///     <para>3=Reserva de Domínio;</para>
    ///     <para>4=Penhor de Veículos;</para>
    ///     <para>9=Outras. (v2.0)</para>
    /// </summary>
    public enum TipoRestricao
    {
        /// <summary>
        /// 0=Não há
        /// </summary>
        [Description("Não há")]
        [XmlEnum("0")]
        Nenhuma = 0,

        /// <summary>
        /// 1=Alienação Fiduciária
        /// </summary>
        [Description("Alienação Fiduciária")]
        [XmlEnum("1")]
        AlienacaoFiduciaria = 1,

        /// <summary>
        /// 2=Arrendamento Mercantil
        /// </summary>
        [Description("Arrendamento Mercantil")]
        [XmlEnum("2")]
        ArrendamentoMercantil = 2,

        /// <summary>
        /// 3=Reserva de Domínio
        /// </summary>
        [Description("Reserva de Domínio")]
        [XmlEnum("3")]
        ReservaDeDominio = 3,

        /// <summary>
        /// 4=Penhor de Veículos
        /// </summary>
        [Description("Penhor de Veículos")]
        [XmlEnum("4")]
        PenhorDeVeiculos = 4,

        /// <summary>
        /// 9=Outras. (v2.0)
        /// </summary>
        [Description("Outras")]
        [XmlEnum("9")]
        Outras = 9
    }

    /// <summary>
    ///     <para>0=Uso permitido;</para>
    ///     <para>1=Uso restrito;</para>
    /// </summary>
    public enum TipoArma
    {
        /// <summary>
        /// 0=Uso permitido
        /// </summary>
        [Description("Uso permitido")]
        [XmlEnum("0")]
        UsoPermitido = 0,

        /// <summary>
        /// 1=Uso restrito
        /// </summary>
        [Description("Uso restrito")]
        [XmlEnum("1")]
        UsoRestrito = 1
    }

    /// <summary>
    ///     <para>Indicador de Escala Relevante:</para>
    ///     <para>S - Produzido em Escala Relevante;</para>
    ///     <para>N – Produzido em Escala NÃO Relevante.</para>
    ///     <para>Versão 4.00</para>
    /// </summary>
    public enum indEscala
    {
        /// <summary>
        /// S -  Produzido em Escala Relevante
        /// </summary>
        [Description("Produzido em Escala Relevante")]
        [XmlEnum("S")]
        S = 'S',

        /// <summary>
        /// N – Produzido em Escala NÃO Relevante
        /// </summary>
        [Description("Produzido em Escala NÃO Relevante")]
        [XmlEnum("N")]
        N = 'N'
    }
}