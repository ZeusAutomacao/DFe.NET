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

using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Total;

namespace NFe.Classes.Testes;

/// <summary>Testes dos modelos XML acrescentados pela NT 2026.008.</summary>
[TestClass]
public class NotaTecnica2026008Testes
{
    private const string NamespaceNFe = "http://www.portalfiscal.inf.br/nfe";

    /// <summary>Verifica a ordem e a precisão dos valores líquidos no item.</summary>
    [TestMethod]
    public void DadoItemComValoresLiquidosQuandoSerializarEntaoDeveRespeitarOrdemEPrecisao()
    {
        var item = Produto();
        item.vUnComLiq = 1.1234567891m;
        item.vProdLiq = 2.25m;

        var xml = Serializar(item, "prod");

        Assert.AreEqual("1.1234567891", xml.Element(XName.Get("vUnComLiq", NamespaceNFe))?.Value);
        Assert.AreEqual("2.25", xml.Element(XName.Get("vProdLiq", NamespaceNFe))?.Value);
        ValidarFragmento(xml, "prod", false);
    }

    /// <summary>Verifica a ausência de valores líquidos opcionais.</summary>
    [TestMethod]
    public void DadoItemSemValoresLiquidosQuandoSerializarEntaoDeveOmitirOsDoisCampos()
    {
        var xml = Serializar(Produto(), "prod");

        Assert.IsNull(xml.Element(XName.Get("vUnComLiq", NamespaceNFe)));
        Assert.IsNull(xml.Element(XName.Get("vProdLiq", NamespaceNFe)));
    }

    /// <summary>Verifica que o schema exige ambos os valores líquidos quando o grupo é informado.</summary>
    [TestMethod]
    public void DadoSomenteUmValorLiquidoQuandoValidarEntaoSchemaDeveRejeitar()
    {
        var item = Produto();
        item.vUnComLiq = 1m;
        var xml = Serializar(item, "prod");

        ValidarFragmento(xml, "prod", true);
    }

    /// <summary>Verifica a serialização do ICMS previsto.</summary>
    [TestMethod]
    public void DadoIcmsPrevistoQuandoSerializarEntaoDeveGerarGrupoEValor()
    {
        var xml = Serializar(new ICMS { TipoICMS = new ICMSPrevistoPagtoAntecip { vICMSPrevisto = 0m } }, "ICMS");

        Assert.AreEqual("0.00", xml.Element(XName.Get("ICMSPrevistoPagtoAntecip", NamespaceNFe))
            ?.Element(XName.Get("vICMSPrevisto", NamespaceNFe))?.Value);
        ValidarFragmento(xml, "ICMS", false);
    }

    /// <summary>Verifica a posição do total líquido antes dos totais de ICMS.</summary>
    [TestMethod]
    public void DadoTotalLiquidoQuandoSerializarEntaoDeveGerarCampoAntesDoIcmsTot()
    {
        var xml = Serializar(new total { vProdLiqTot = 10.25m, ICMSTot = new ICMSTot() }, "total");

        Assert.AreEqual("vProdLiqTot", xml.Elements().First().Name.LocalName);
        Assert.AreEqual("10.25", xml.Elements().First().Value);
    }


    /// <summary>Serializa um modelo da NF-e para XML.</summary>
    private static XElement Serializar<T>(T objeto, string nome)
    {
        var serializador = new XmlSerializer(typeof(T), new XmlRootAttribute(nome) { Namespace = NamespaceNFe });
        using var escrita = new StringWriter();
        serializador.Serialize(escrita, objeto);
        return XElement.Parse(escrita.ToString());
    }

    /// <summary>Cria um produto válido para os testes de schema.</summary>
    private static prod Produto() => new()
    {
        cProd = "1", cEAN = "SEM GTIN", xProd = "PRODUTO TESTE", NCM = "22021000", CFOP = 5102,
        uCom = "UN", qCom = 2, vUnCom = 10, vProd = 20, cEANTrib = "SEM GTIN", uTrib = "UN",
        qTrib = 2, vUnTrib = 10, indTot = (IndicadorTotal)1
    };

    /// <summary>Valida um fragmento contra o schema do projeto de exemplo.</summary>
    private static void ValidarFragmento(XElement xml, string nome, bool deveRejeitar)
    {
        var pasta = new DirectoryInfo(AppContext.BaseDirectory);
        while (pasta != null && !Directory.Exists(Path.Combine(pasta.FullName, "NFe.AppTeste", "Schemas")))
            pasta = pasta.Parent;
        Assert.IsNotNull(pasta);

        var schemas = new XmlSchemaSet { XmlResolver = new XmlUrlResolver() };
        schemas.Add(NamespaceNFe, Path.Combine(pasta.FullName, "NFe.AppTeste", "Schemas", "nfe_v4.00.xsd"));
        schemas.Compile();
        var raiz = schemas.GlobalElements[new XmlQualifiedName("NFe", NamespaceNFe)];
        var visitados = new HashSet<XmlSchemaObject>();
        XmlSchemaElement? Buscar(XmlSchemaObject? objeto)
        {
            if (objeto == null || !visitados.Add(objeto)) return null;
            if (objeto is XmlSchemaElement elemento)
                return elemento.Name == nome ? elemento : Buscar(elemento.ElementSchemaType);
            if (objeto is XmlSchemaComplexType tipo) return Buscar(tipo.ContentTypeParticle);
            if (objeto is XmlSchemaGroupBase grupo)
                foreach (XmlSchemaObject filho in grupo.Items)
                {
                    var encontrado = Buscar(filho);
                    if (encontrado != null) return encontrado;
                }
            return null;
        }

        var fragmento = Buscar(raiz);
        Assert.IsNotNull(fragmento);
        var erros = new List<string>();
        xml.Validate(fragmento, schemas, (_, evento) => erros.Add(evento.Message), true);
        if (deveRejeitar) Assert.IsTrue(erros.Count > 0, "O schema aceitou apenas um dos valores líquidos.");
        else Assert.AreEqual(0, erros.Count, string.Join(Environment.NewLine, erros));
    }
}
