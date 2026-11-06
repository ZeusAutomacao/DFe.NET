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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NFe.Classes.Testes;

/// <summary>
///     Serialização real e validação dos grupos contra os schemas distribuídos em NFe.AppTeste.
/// </summary>
internal static class SchemaTesteAuxiliar
{
    /// <summary>Namespace dos documentos da NF-e.</summary>
    internal const string NamespaceNFe = "http://www.portalfiscal.inf.br/nfe";

    /// <summary>Namespace da linguagem XML Schema.</summary>
    private const string NamespaceSchema = "http://www.w3.org/2001/XMLSchema";

    /// <summary>Localiza um schema copiado pelo projeto de testes.</summary>
    internal static string CaminhoSchema(string arquivo) => Path.Combine(AppContext.BaseDirectory, "Schemas", arquivo);

    /// <summary>Serializa o modelo com <see cref="XmlSerializer"/> e seu namespace fiscal.</summary>
    internal static string Serializar<T>(T modelo)
    {
        using var writer = new StringWriter(CultureInfo.InvariantCulture);
        new XmlSerializer(typeof(T), NamespaceNFe).Serialize(writer, modelo);
        return writer.ToString();
    }

    /// <summary>Materializa o modelo com o mesmo <see cref="XmlSerializer"/> da escrita.</summary>
    internal static T Desserializar<T>(string xml)
    {
        using var reader = new StringReader(xml);
        return (T)new XmlSerializer(typeof(T), NamespaceNFe).Deserialize(reader)!;
    }

    /// <summary>Compila uma raiz funcional sem ignorar erros ou avisos de includes/imports.</summary>
    internal static XmlSchemaSet CompilarRaiz(string arquivo)
    {
        var mensagens = new List<string>();
        var schemas = new XmlSchemaSet { XmlResolver = new XmlUrlResolver() };
        schemas.ValidationEventHandler += (_, evento) => mensagens.Add(evento.Message);
        schemas.Add(NamespaceNFe, CaminhoSchema(arquivo));
        schemas.Compile();
        Assert.AreEqual(0, mensagens.Count, string.Join(Environment.NewLine, mensagens));
        Assert.IsTrue(schemas.IsCompiled);
        Assert.IsTrue(schemas.GlobalElements.Count > 0);
        return schemas;
    }

    /// <summary>
    ///     Expõe o tipo TMonofasia oficial como raiz para testar <see cref="Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.gIBSCBSMono"/> isoladamente.
    /// </summary>
    internal static IReadOnlyList<string> ValidarMonofasia(XElement grupo)
    {
        var auxiliar = CriarSchemaAuxiliar("DFeTiposBasicos_v1.00.xsd");
        var elemento = auxiliar.CreateElement("xs", "element", NamespaceSchema);
        elemento.SetAttribute("name", "gIBSCBSMono");
        elemento.SetAttribute("type", "TMonofasia");
        auxiliar.DocumentElement!.AppendChild(elemento);
        return Validar(grupo.ToString(SaveOptions.DisableFormatting), auxiliar);
    }

    /// <summary>
    ///     Valida <see cref="Informacoes.Total.total"/> usando sua declaração integral no leiaute oficial.
    /// </summary>
    internal static IReadOnlyList<string> ValidarTotal(string xml)
    {
        var oficial = new XmlDocument();
        oficial.Load(CaminhoSchema("leiauteNFe_v4.00.xsd"));
        var namespaces = new XmlNamespaceManager(oficial.NameTable);
        namespaces.AddNamespace("xs", NamespaceSchema);
        var elemento = oficial.SelectNodes("//xs:element[@name='total']", namespaces)!.Cast<XmlNode>().Single();
        var auxiliar = CriarSchemaAuxiliar("leiauteNFe_v4.00.xsd");
        auxiliar.DocumentElement!.AppendChild(auxiliar.ImportNode(elemento, true));
        return Validar(xml, auxiliar);
    }

    /// <summary>Inclui o schema original sem redefinir suas regras fiscais.</summary>
    private static XmlDocument CriarSchemaAuxiliar(string arquivo)
    {
        var auxiliar = new XmlDocument();
        auxiliar.LoadXml($"<xs:schema xmlns:xs='{NamespaceSchema}' xmlns='{NamespaceNFe}' targetNamespace='{NamespaceNFe}' elementFormDefault='qualified'><xs:include/></xs:schema>");
        ((XmlElement)auxiliar.DocumentElement!.FirstChild!).SetAttribute("schemaLocation", new Uri(CaminhoSchema(arquivo)).AbsoluteUri);
        return auxiliar;
    }

    /// <summary>Separa falhas de compilação do schema das rejeições esperadas do documento.</summary>
    private static IReadOnlyList<string> Validar(string xml, XmlDocument auxiliar)
    {
        var mensagens = new List<string>();
        var schemas = new XmlSchemaSet { XmlResolver = new XmlUrlResolver() };
        schemas.ValidationEventHandler += (_, evento) => mensagens.Add(evento.Message);
        using var reader = new XmlNodeReader(auxiliar);
        schemas.Add(NamespaceNFe, reader);
        schemas.Compile();
        Assert.AreEqual(0, mensagens.Count, string.Join(Environment.NewLine, mensagens));
        Assert.IsTrue(schemas.IsCompiled);
        var documento = new XmlDocument { Schemas = schemas };
        documento.LoadXml(xml);
        documento.Validate((_, evento) => mensagens.Add(evento.Message));
        return mensagens;
    }
}
