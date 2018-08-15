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

using DFe.Utils;
using NFe.Classes;
using System.IO;

public static class ExtNfeProc
{
    /// <summary>
    ///     Carrega um arquivo XML para um objeto da classe nfeProc
    /// </summary>
    /// <param name="nfeProc"></param>
    /// <param name="arquivoXml">arquivo XML</param>
    /// <returns>Retorna um nfeProc carregada com os dados do XML</returns>
    public static nfeProc CarregarDeArquivoXml(this nfeProc nfeProc, string arquivoXml)
    {
        var s = FuncoesXml.ObterNodeDeArquivoXml(typeof (nfeProc).Name, arquivoXml);
        return FuncoesXml.XmlStringParaClasse<nfeProc>(s);
    }

    /// <summary>
    ///     Carrega um arquivo XML para um objeto da classe nfeProc
    /// </summary>
    /// <param name="nfeProc"></param>
    /// <param name="stream">Stream contendo o arquivo xml</param>
    /// <returns>Retorna um nfeProc carregada com os dados do XML</returns>
    public static nfeProc CarregardeStream(this nfeProc nfeProc, StreamReader stream)
    {
        var s = FuncoesXml.ObterNodeDeStream(typeof(nfeProc).Name, stream);
        return FuncoesXml.XmlStringParaClasse<nfeProc>(s);
    }

    /// <summary>
    ///     Converte o objeto nfeProc para uma string no formato XML
    /// </summary>
    /// <param name="nfeProc"></param>
    /// <returns>Retorna uma string no formato XML com os dados do nfeProc</returns>
    public static string ObterXmlString(this nfeProc nfeProc)
    {
        return FuncoesXml.ClasseParaXmlString(nfeProc);
    }

    /// <summary>
    ///     Coverte uma string XML no formato nfeProc para um objeto nfeProc
    /// </summary>
    /// <param name="nfeProc"></param>
    /// <param name="xmlString"></param>
    /// <returns>Retorna um objeto do tipo nfeProc</returns>
    public static nfeProc CarregarDeXmlString(this nfeProc nfeProc, string xmlString)
    {
        var s = FuncoesXml.ObterNodeDeStringXml(typeof (nfeProc).Name, xmlString);
        return FuncoesXml.XmlStringParaClasse<nfeProc>(s);
    }

    /// <summary>
    ///     Grava os dados do objeto nfeProc em um arquivo XML
    /// </summary>
    /// <param name="nfeProc">Objeto nfeProc</param>
    /// <param name="arquivoXml">Diretório com nome do arquivo a ser gravado</param>
    public static void SalvarArquivoXml(this nfeProc nfeProc, string arquivoXml)
    {
        FuncoesXml.ClasseParaArquivoXml(nfeProc, arquivoXml);
    }
}
