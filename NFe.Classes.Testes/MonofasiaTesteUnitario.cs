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

using System.Globalization;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.Tributacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.Tipos;
using NFe.Classes.Servicos.Autorizacao;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Classes.Testes;

/// <summary>Regressões da retenção anterior Ad Rem no TMonofasia do PL_010g v1.03.</summary>
[TestClass]
public class MonofasiaTesteUnitario
{
    /// <summary>Namespace usado para conferir a estrutura de <see cref="gIBSCBSMono"/>.</summary>
    private static readonly XNamespace NamespaceNFe = SchemaTesteAuxiliar.NamespaceNFe;

    /// <summary>Confere valores, ordem e ausência de bases/alíquotas removidas no novo grupo.</summary>
    [DataTestMethod]
    [DataRow("1.23", "4.56")]
    [DataRow("0.00", "0.00")]
    [DataRow("1.23", "0.00")]
    [DataRow("0.00", "4.56")]
    public void DadaRetencaoAnteriorQuandoSerializarEntaoDeveValidarNoSchemaOficial(string ibs, string cbs)
    {
        var grupo = XElement.Parse(SchemaTesteAuxiliar.Serializar(CriarMonofasia(ibs, cbs)));

        CollectionAssert.AreEqual(new[] { "gIBSMonoAdRem", "gCBSMonoAdRem", "vTotIBSMonoItem", "vTotCBSMonoItem" },
            grupo.Elements().Select(elemento => elemento.Name.LocalName).ToArray());
        ConferirRetencao(grupo, "gIBSMonoAdRem", "vIBSMonoRet", ibs);
        ConferirRetencao(grupo, "gCBSMonoAdRem", "vCBSMonoRet", cbs);
        Assert.AreEqual(ibs, grupo.Element(NamespaceNFe + "vTotIBSMonoItem")!.Value);
        Assert.AreEqual(cbs, grupo.Element(NamespaceNFe + "vTotCBSMonoItem")!.Value);
        Assert.IsFalse(grupo.Descendants().Any(elemento => new[] { "gMonoPadrao", "qBCMonoRet", "adRemIBSRet", "adRemCBSRet" }.Contains(elemento.Name.LocalName)));
        Assert.AreEqual(0, SchemaTesteAuxiliar.ValidarMonofasia(grupo).Count);
    }

    /// <summary>Garante que aceitar zero não permite retenções negativas.</summary>
    [DataTestMethod]
    [DataRow("-0.01", "0.00", "vIBSMonoRet")]
    [DataRow("0.00", "-0.01", "vCBSMonoRet")]
    public void DadaRetencaoNegativaQuandoValidarEntaoDeveRejeitarOValor(string ibs, string cbs, string campo)
    {
        var grupo = XElement.Parse(SchemaTesteAuxiliar.Serializar(CriarMonofasia(ibs, cbs)));

        Assert.IsTrue(SchemaTesteAuxiliar.ValidarMonofasia(grupo).Any(mensagem => mensagem.Contains(campo)));
    }

    /// <summary>Permite somente IBS ou somente CBS, omitindo o ramo nulo do outro tributo.</summary>
    [DataTestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void DadoApenasUmTributoQuandoSerializarEntaoDeveOmitirORamoNuloEValidar(bool somenteIbs)
    {
        var mono = CriarMonofasia(somenteIbs ? "1.23" : "0.00", somenteIbs ? "0.00" : "4.56");
        if (somenteIbs) mono.gCBSMonoAdRem = null;
        else mono.gIBSMonoAdRem = null;

        var grupo = XElement.Parse(SchemaTesteAuxiliar.Serializar(mono));
        var relido = SchemaTesteAuxiliar.Desserializar<gIBSCBSMono>(grupo.ToString());

        Assert.IsNull(grupo.Element(NamespaceNFe + (somenteIbs ? "gCBSMonoAdRem" : "gIBSMonoAdRem")));
        Assert.AreEqual(0, SchemaTesteAuxiliar.ValidarMonofasia(grupo).Count);
        Assert.AreEqual(mono.vTotIBSMonoItem, relido.vTotIBSMonoItem);
        Assert.AreEqual(mono.vTotCBSMonoItem, relido.vTotCBSMonoItem);
        if (somenteIbs)
        {
            Assert.IsNull(relido.gCBSMonoAdRem);
            Assert.AreEqual(1.23m, relido.gIBSMonoAdRem.gMonoRet.vIBSMonoRet);
        }
        else
        {
            Assert.IsNull(relido.gIBSMonoAdRem);
            Assert.AreEqual(4.56m, relido.gCBSMonoAdRem.gMonoRet.vCBSMonoRet);
        }
    }

    /// <summary>Confere o arredondamento do modelo no XML, inclusive o desempate ABNT.</summary>
    [DataTestMethod]
    [DataRow("1.234", "1.23")]
    [DataRow("1.235", "1.24")]
    [DataRow("1.225", "1.22")]
    public void DadoValorComMaisCasasQuandoSerializarEntaoDeveArredondarParaDuas(string valor, string esperado)
    {
        var mono = CriarMonofasia(valor, valor);
        var grupo = XElement.Parse(SchemaTesteAuxiliar.Serializar(mono));

        ConferirRetencao(grupo, "gIBSMonoAdRem", "vIBSMonoRet", esperado);
        ConferirRetencao(grupo, "gCBSMonoAdRem", "vCBSMonoRet", esperado);
        Assert.AreEqual(decimal.Parse(esperado, CultureInfo.InvariantCulture), mono.gIBSMonoAdRem.gMonoRet.vIBSMonoRet);
        Assert.AreEqual(decimal.Parse(esperado, CultureInfo.InvariantCulture), mono.gCBSMonoAdRem.gMonoRet.vCBSMonoRet);
        Assert.AreEqual(0, SchemaTesteAuxiliar.ValidarMonofasia(grupo).Count);
    }

    /// <summary>Preserva o novo grupo nos três modelos reais de documento e envelope.</summary>
    [TestMethod]
    public void DadaNotaComRetencaoQuandoRelerNotaLoteEProtocoloEntaoDevePreservarOsGrupos()
    {
        var nota = new NFe
        {
            infNFe = new infNFe
            {
                versao = "4.00",
                det = new List<det>
                {
                    new() { nItem = 1, imposto = new imposto { IBSCBS = new IBSCBS { CST = CST.Cst620, cClassTrib = "620006", gIBSCBSMono = CriarMonofasia("1.23", "4.56") } } }
                }
            }
        };
        var relida = SchemaTesteAuxiliar.Desserializar<NFe>(SchemaTesteAuxiliar.Serializar(nota));
        var lote = new enviNFe4("4.00", 1, IndicadorSincronizacao.Sincrono, new List<NFe> { relida });
        var loteRelido = SchemaTesteAuxiliar.Desserializar<enviNFe4>(SchemaTesteAuxiliar.Serializar(lote));
        var processo = new nfeProc { versao = "4.00", NFe = relida };
        var processoRelido = SchemaTesteAuxiliar.Desserializar<nfeProc>(SchemaTesteAuxiliar.Serializar(processo));

        foreach (var xml in new[] { SchemaTesteAuxiliar.Serializar(relida), SchemaTesteAuxiliar.Serializar(loteRelido), SchemaTesteAuxiliar.Serializar(processoRelido) })
        {
            var grupo = XDocument.Parse(xml).Descendants(NamespaceNFe + "gIBSCBSMono").Single();
            ConferirRetencao(grupo, "gIBSMonoAdRem", "vIBSMonoRet", "1.23");
            ConferirRetencao(grupo, "gCBSMonoAdRem", "vCBSMonoRet", "4.56");
            Assert.AreEqual(0, SchemaTesteAuxiliar.ValidarMonofasia(grupo).Count);
        }
    }

    /// <summary>Não perde bases, alíquotas ou valores dos documentos emitidos no leiaute anterior.</summary>
    [TestMethod]
    public void DadoXmlLegadoQuandoRelerESerializarEntaoDevePreservarARetencaoAnterior()
    {
        const string xml = "<gIBSCBSMono xmlns='http://www.portalfiscal.inf.br/nfe'><gMonoRet><qBCMonoRet>12.3456</qBCMonoRet><adRemIBSRet>7.6543</adRemIBSRet><vIBSMonoRet>1.23</vIBSMonoRet><adRemCBSRet>8.7654</adRemCBSRet><vCBSMonoRet>4.56</vCBSMonoRet></gMonoRet><vTotIBSMonoItem>1.23</vTotIBSMonoItem><vTotCBSMonoItem>4.56</vTotCBSMonoItem></gIBSCBSMono>";
        var original = SchemaTesteAuxiliar.Desserializar<gIBSCBSMono>(xml);
        var relido = SchemaTesteAuxiliar.Desserializar<gIBSCBSMono>(SchemaTesteAuxiliar.Serializar(original));

        foreach (var mono in new[] { original, relido })
        {
            Assert.IsNull(mono.gIBSMonoAdRem);
            Assert.IsNull(mono.gCBSMonoAdRem);
            Assert.AreEqual(12.3456m, mono.gMonoRet.qBCMonoRet);
            Assert.AreEqual(7.6543m, mono.gMonoRet.adRemIBSRet);
            Assert.AreEqual(8.7654m, mono.gMonoRet.adRemCBSRet);
            Assert.AreEqual(1.23m, mono.gMonoRet.vIBSMonoRet);
            Assert.AreEqual(4.56m, mono.gMonoRet.vCBSMonoRet);
            Assert.AreEqual(1.23m, mono.vTotIBSMonoItem);
            Assert.AreEqual(4.56m, mono.vTotCBSMonoItem);
        }
    }

    /// <summary>Cria valores independentes de quantidade e alíquota, ausentes no novo grupo retido.</summary>
    private static gIBSCBSMono CriarMonofasia(string ibs, string cbs) => new()
    {
        gIBSMonoAdRem = new gIBSMonoAdRem { gMonoRet = new gMonoRetIBS { vIBSMonoRet = decimal.Parse(ibs, CultureInfo.InvariantCulture) } },
        gCBSMonoAdRem = new gCBSMonoAdRem { gMonoRet = new gMonoRetCBS { vCBSMonoRet = decimal.Parse(cbs, CultureInfo.InvariantCulture) } },
        vTotIBSMonoItem = decimal.Parse(ibs, CultureInfo.InvariantCulture),
        vTotCBSMonoItem = decimal.Parse(cbs, CultureInfo.InvariantCulture)
    };

    /// <summary>Confere que cada retenção contém exclusivamente o valor correspondente.</summary>
    private static void ConferirRetencao(XElement grupo, string ramo, string campo, string valor)
    {
        var retencao = grupo.Element(NamespaceNFe + ramo)!.Elements().Single();
        Assert.AreEqual(NamespaceNFe + "gMonoRet", retencao.Name);
        var elemento = retencao.Elements().Single();
        Assert.AreEqual(NamespaceNFe + campo, elemento.Name);
        Assert.AreEqual(valor, elemento.Value);
    }
}
