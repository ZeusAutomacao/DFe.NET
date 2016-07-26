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

namespace NFe.Classes.Informacoes.Identificacao.Tipos
{
    /// <summary>
    ///     Indicador da forma de pagamento
    ///     <para>0 – pagamento à vista;</para>
    ///     <para>1 – pagamento à prazo;</para>
    ///     <para>2 - outros.</para>
    /// </summary>
    public enum IndicadorPagamento
    {
        [XmlEnum("0")] ipVista = 0,
        [XmlEnum("1")] ipPrazo = 1,
        [XmlEnum("2")] ipOutras = 2
    }

    /// <summary>
    ///     Código do modelo do Documento Fiscal. 55 = NF-e; 65 = NFC-e.
    /// </summary>
    public enum ModeloDocumento
    {
        [XmlEnum("55")] NFe = 55,
        [XmlEnum("65")] NFCe = 65
    }

    /// <summary>
    ///     Tipo do Documento Fiscal (0 - entrada; 1 - saída)
    /// </summary>
    public enum TipoNFe
    {
        [XmlEnum("0")] tnEntrada = 0,
        [XmlEnum("1")] tnSaida = 1
    }

    /// <summary>
    ///     Identificador de Local de destino da operação (1-Interna;2-Interestadual;3-Exterior)
    /// </summary>
    public enum DestinoOperacao
    {
        [XmlEnum("1")] doInterna = 1,
        [XmlEnum("2")] doInterestadual = 2,
        [XmlEnum("3")] doExterior = 3
    }

    /// <summary>
    ///     Formato de impressão do DANFE (0-sem DANFE;1-DANFe Retrato; 2-DANFe Paisagem;3-DANFe Simplificado; 4-DANFe
    ///     NFC-e;5-DANFe NFC-e em mensagem eletrônica)
    /// </summary>
    public enum TipoImpressao
    {
        [XmlEnum("0")] tiSemGeracao = 0,
        [XmlEnum("1")] tiRetrato = 1,
        [XmlEnum("2")] tiPaisagem = 2,
        [XmlEnum("3")] tiSimplificado = 3,
        [XmlEnum("4")] tiNFCe = 4,
        [XmlEnum("5")] tiMsgEletronica = 5
    }

    /// <summary>
    ///     Forma de emissão da NF-e
    ///     <para>1 - Emissão normal (não em contingência)</para>
    ///     <para>2 - Contingência FS-IA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>3 - Contingência SCAN (Sistema de Contingência do Ambiente Nacional)</para>
    ///     <para>4 - Contingência DPEC (Declaração Prévia da Emissão em Contingência)</para>
    ///     <para>5 - Contingência FS-DA, com impressão do DANFE em formulário de segurança</para>
    ///     <para>6 - Contingência SVC-AN (SEFAZ Virtual de Contingência do AN)</para>
    ///     <para>7 - Contingência SVC-RS (SEFAZ Virtual de Contingência do RS)</para>
    ///     <para>9 - Contingência off-line da NFC-e</para>
    ///     <para>Nota: Para a NFC-e somente estão disponíveis e são válidas as opções de contingência 5 e 9</para>
    /// </summary>
    public enum TipoEmissao
    {
        [XmlEnum("1")] teNormal = 1,
        [XmlEnum("2")] teFSIA = 2,
        [XmlEnum("3")] teSCAN = 3,
        [XmlEnum("4")] teEPEC = 4,
        [XmlEnum("5")] teFSDA = 5,
        [XmlEnum("6")] teSVCAN = 6,
        [XmlEnum("7")] teSVCRS = 7,
        [XmlEnum("9")] teOffLine = 9
    }

    /// <summary>
    ///     Identificação do Ambiente
    ///     <para>1 - Produção</para>
    ///     <para>2 - Homologação</para>
    /// </summary>
    public enum TipoAmbiente
    {
        [XmlEnum("1")] 
        [Description("Produção")]
        taProducao = 1,
        
        [XmlEnum("2")]
        [Description("Homologação")]
        taHomologacao = 2
    }

    /// <summary>
    ///     Finalidade da emissão da NF-e
    ///     <para>1 - NFe normal</para>
    ///     <para>2 - NFe complementar</para>
    ///     <para>3 - NFe de ajuste</para>
    ///     <para>4 - Devolução/Retorno</para>
    /// </summary>
    public enum FinalidadeNFe
    {
        [XmlEnum("1")] fnNormal = 1,
        [XmlEnum("2")] fnComplementar = 2,
        [XmlEnum("3")] fnAjuste = 3,
        [XmlEnum("4")] fnDevolucao = 4
    }

    /// <summary>
    ///     Processo de emissão utilizado com a seguinte codificação:
    ///     <para>0 - emissão de NF-e com aplicativo do contribuinte;</para>
    ///     <para>1 - emissão de NF-e avulsa pelo Fisco;</para>
    ///     <para>2 - emissão de NF-e avulsa, pelo contribuinte com seu certificado digital, através do site do Fisco;</para>
    ///     <para>3- emissão de NF-e pelo contribuinte com aplicativo fornecido pelo Fisco.</para>
    /// </summary>
    public enum ProcessoEmissao
    {
        [XmlEnum("0")] peAplicativoContribuinte = 0,
        [XmlEnum("1")] peAvulsaFisco = 1,
        [XmlEnum("2")] peAvulsaContribuinte = 2,
        [XmlEnum("3")] peContribuinteAplicativoFisco = 3
    }

    /// <summary>
    ///     Indica operação com Consumidor final
    ///     <para>0 - Normal;</para>
    ///     <para>1 - Consumidor final;</para>
    /// </summary>
    public enum ConsumidorFinal
    {
        [Description("Normal")]
        [XmlEnum("0")] cfNao = 0,

        [Description("Consumidor final")]
        [XmlEnum("1")] cfConsumidorFinal = 1
    }

    /// <summary>
    /// Indicador de presença do comprador no estabelecimento comercial no momento da operação
    /// <para>0 - Não se aplica</para>
    /// <para>1 - Operação presencial;</para>
    /// <para>2 - Operação não presencial, pela Internet;</para>
    /// <para>3 - Operação não presencial, Teleatendimento;</para>
    /// <para>4 - NFC-e em operação com entrega a domicílio;</para>
    /// <para>9 - Operação não presencial, outros.</para>
    /// </summary>
    public enum PresencaComprador
    {
        [XmlEnum("0")] pcNao = 0,
        [XmlEnum("1")] pcPresencial = 1,
        [XmlEnum("2")] pcInternet = 2,
        [XmlEnum("3")] pcTeleatendimento = 3,
        [XmlEnum("4")] pcEntregaDomicilio = 4,
        [XmlEnum("9")] pcOutros = 9
    }

    /// <summary>
    /// Indicador do tipo de Operação do CSC
    /// <para>1 - Consulta CSC Ativos;</para>
    /// <para>2 - Solicita novo CSC;</para>
    /// <para>3 - Revoga CSC Ativo</para>
    /// </summary>
    public enum IdentificadorOperacaoCsc
    {
        [XmlEnum("1")]
        ioConsultaCscAtivos = 1,
        [XmlEnum("2")]
        ioSolicitaNovoCsc = 2,
        [XmlEnum("3")]
        ioRevogaCscAtivo = 3
    }
}