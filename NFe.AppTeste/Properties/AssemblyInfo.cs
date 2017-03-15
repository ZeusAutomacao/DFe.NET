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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

// Informações gerais sobre um assembly são controladas através do seguinte 
// conjunto de atributos. Altere o valor destes atributos para modificar a informação
// associada a um assembly.

[assembly: AssemblyTitle("NFe.AppTeste")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("NFe.AppTeste")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Definir ComVisible como false torna os tipos neste assembly não visíveis 
// para componentes COM.  Caso precise acessar um tipo neste assembly a partir de 
// COM, defina o atributo ComVisible como true nesse tipo.

[assembly: ComVisible(false)]

//Para iniciar a compilação de aplicações localizáveis, configure 
//<UICulture>CultureYouAreCodingWith</UICulture> no seu arquivo .csproj
//dentro de um <Grupo de Propriedade>.  Por exemplo, se você está usando o idioma inglês
//nos seus arquivos de origem, configure o <Cultura do IU> para en-US.  Em seguida, descomente
//o atributo NeutralResourceLanguage abaixo.  Atualize o "en-US" em
//a linha abaixo para coincidir com a configuração do Cultura do IU no arquivo do projeto.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]


[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //onde os dicionários de recursos de temas específicos estão localizados
    //(usado se algum recurso não é encontrado na página, 
    // ou dicionários de recursos de aplicação)
    ResourceDictionaryLocation.SourceAssembly //onde o dicionário de recursos genéricos está localizado
    //(usado se algum recurso não é encontrado na página, 
    // app, ou qualquer outro dicionário de recursos de tema específico)
    )]


// Informações de Versão para um assembly consistem nos quatro valores a seguir:
//
//      Versão Principal
//      Versão Secundária 
//      Número da Versão
//      Revisão
//
// É possível especificar todos os valores ou usar o padrão de Números de Compilação e Revisão 
// utilizando o '*' como mostrado abaixo:
// [assembly: AssemblyVersion("1.0.*")]

[assembly: AssemblyVersion("1.0.1.442")]
[assembly: AssemblyFileVersion("1.0.1.442")]