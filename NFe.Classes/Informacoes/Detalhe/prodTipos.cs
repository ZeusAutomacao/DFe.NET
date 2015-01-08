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

namespace NFe.Classes.Informacoes.Detalhe
{
    /// <summary>
    ///     <para>0 – o valor do item (vProd) não compõe o valor total da NF-e (vProd)</para>
    ///     <para>1  – o valor do item (vProd) compõe o valor total da NF-e (vProd)</para>
    /// </summary>
    public enum IndicadorTotal
    {
        [XmlEnum("0")] ValorDoItemNaoCompoeTotalNF,
        [XmlEnum("1")] ValorDoItemCompoeTotalNF
    }

    /// <summary>
    ///     1=Marítima;
    ///     2=Fluvial;
    ///     3=Lacustre;
    ///     4=Aérea;
    ///     5=Postal
    ///     6=Ferroviária;
    ///     7=Rodoviária;
    ///     8=Conduto / Rede Transmissão;
    ///     9=Meios Próprios;
    ///     10=Entrada / Saída ficta; 11=Courier; 12=Handcarry. (NT 2013/005 v 1.10).
    /// </summary>
    public enum TipoTransporteInternacional
    {
        [XmlEnum("1")] Maritima = 1,
        [XmlEnum("2")] Fluvial = 2,
        [XmlEnum("3")] Lacustre = 3,
        [XmlEnum("4")] Aerea = 4,
        [XmlEnum("5")] Postal = 5,
        [XmlEnum("6")] Ferroviaria = 6,
        [XmlEnum("7")] Rodoviaria = 7,
        [XmlEnum("8")] CondutoRedeTransmissão = 8,
        [XmlEnum("9")] MeiosProprios = 9,
        [XmlEnum("10")] EntradaSaidaficta = 10,
        [XmlEnum("11")] Courier = 11,
        [XmlEnum("12")] Handcarry = 12
    }

    /// <summary>
    ///     1=Importação por conta própria;
    ///     2=Importação por conta e ordem;
    ///     3=Importação por encomenda;
    /// </summary>
    public enum TipoIntermediacao
    {
        [XmlEnum("1")] ContaPropria = 1,
        [XmlEnum("2")] ContaeOrdem = 2,
        [XmlEnum("3")] PorEncomenda = 3
    }

    /// <summary>
    ///     1=Venda concessionária,
    ///     2=Faturamento direto para consumidor final
    ///     3=Venda direta para grandes consumidores (frotista, governo, ...)
    ///     0=Outros
    /// </summary>
    public enum TipoOperacao
    {
        [XmlEnum("0")] Outros = 0,
        [XmlEnum("1")] VendaConcessionaria = 1,
        [XmlEnum("2")] FaturamentodiretoparaconsumidorFinal = 2,
        [XmlEnum("3")] VendaDiretaParaGrandesConsumidores = 3
    }

    /// <summary>
    ///     Informa-se o veículo tem VIN (chassi) remarcado. R=Remarcado; N=Normal
    /// </summary>
    public enum CondicaoVin
    {
        [XmlEnum("R")] Remarcado,
        [XmlEnum("N")] Normal
    }

    /// <summary>
    ///     1=Acabado; 2=Inacabado; 3=Semiacabado
    /// </summary>
    public enum CondicaoVeiculo
    {
        [XmlEnum("1")] Acabado = 1,
        [XmlEnum("2")] Inacabado = 2,
        [XmlEnum("3")] Semiacabado = 3
    }

    /// <summary>
    ///     0=Não há; 1=Alienação Fiduciária;
    ///     2=Arrendamento Mercantil; 3=Reserva de Domínio;
    ///     4=Penhor de Veículos; 9=Outras. (v2.0)
    /// </summary>
    public enum TipoRestricao
    {
        [XmlEnum("0")] Nenhuma = 0,
        [XmlEnum("1")] AlienacaoFiduciaria = 1,
        [XmlEnum("2")] ArrendamentoMercantil = 2,
        [XmlEnum("3")] ReservaDeDominio = 3,
        [XmlEnum("4")] PenhorDeVeiculos = 4,
        [XmlEnum("9")] Outras = 9
    }

    /// <summary>
    ///     0=Uso permitido; 1=Uso restrito;
    /// </summary>
    public enum TipoArma
    {
        [XmlEnum("0")] UsoPermitido = 0,
        [XmlEnum("1")] UsoRestrito = 1
    }
}