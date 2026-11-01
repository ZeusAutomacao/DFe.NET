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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Testes;

/// <summary>Testes da compra governamental acrescentada pela NT 2025.002.</summary>
[TestClass]
public class NotaTecnica2025002CompraGovTestes
{
    private const string NamespaceNFe = "http://www.portalfiscal.inf.br/nfe";

    /// <summary>Verifica a serialização das referências da operação governamental tipo 3.</summary>
    [TestMethod]
    public void DadasReferenciasGovernamentaisQuandoSerializarEntaoDevePreservarOrdemEQuantidade()
    {
        var compras = new gCompraGov
        {
            tpEnteGov = TpEnteGov.Uniao,
            tpOperGov = TpOperGov.FornecimentoPagamentoRealizado,
            refDFeAnt = new List<string> { new('1', 44), new('2', 44) }
        };

        var xml = Serializar(compras, "gCompraGov");

        CollectionAssert.AreEqual(new[] { "1", "2" }, xml.Elements(XName.Get("refDFeAnt", NamespaceNFe))
            .Select(elemento => elemento.Value[0].ToString()).ToArray());
        Assert.AreEqual("3", xml.Element(XName.Get("tpOperGov", NamespaceNFe))?.Value);
        Assert.AreEqual("tpOperGov", xml.Elements().Skip(2).First().Name.LocalName);
        ValidarFragmento(xml, "gCompraGov", false);
    }

    /// <summary>Verifica que a operação governamental tipo 4 não exige referências.</summary>
    [TestMethod]
    public void DadaOperacaoGovernamentalQuatroQuandoSerializarEntaoDeveOmitirReferencias()
    {
        var xml = Serializar(new gCompraGov { tpOperGov = TpOperGov.RecebimentoFornecimentoPosterior }, "gCompraGov");

        Assert.AreEqual("4", xml.Element(XName.Get("tpOperGov", NamespaceNFe))?.Value);
        Assert.IsFalse(xml.Elements(XName.Get("refDFeAnt", NamespaceNFe)).Any());
        ValidarFragmento(xml, "gCompraGov", false);
    }

    /// <summary>Serializa um modelo da NF-e para XML.</summary>
    private static XElement Serializar<T>(T objeto, string nome)
    {
        var serializador = new XmlSerializer(typeof(T), new XmlRootAttribute(nome) { Namespace = NamespaceNFe });
        using var escrita = new StringWriter();
        serializador.Serialize(escrita, objeto);
        return XElement.Parse(escrita.ToString());
    }
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
        if (deveRejeitar) Assert.IsTrue(erros.Count > 0, "O schema aceitou um grupo inválido.");
        else Assert.AreEqual(0, erros.Count, string.Join(Environment.NewLine, erros));
    }
}
