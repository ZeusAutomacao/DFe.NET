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

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Compartilhado.InformacoesIbsCbs.InformacoesIbs
{
  public class gIBSUF
  {
    private decimal _pIbsUf;
    private decimal _vIbsUf;

    /// <summary>
    ///     UB18 - Alíquota do IBS de competência das UF
    /// </summary>

    [XmlElement(Order = 1)]
    public decimal pIBSUF
    {
      get => _pIbsUf.Arredondar(4);
      set => _pIbsUf = value.Arredondar(4);
    }

    /// <summary>
    ///     UB21 - Grupo de Informações do Diferimento
    /// </summary>
    [XmlElement(Order = 2)]
    public gDif gDif { get; set; }

    /// <summary>
    ///     UB24 - Grupo de Informações da devolução de tributos
    /// </summary>
    [XmlElement(Order = 3)]
    public gDevTrib gDevTrib { get; set; }

    /// <summary>
    ///     UB26 - Grupo de informações da redução da alíquota
    /// </summary>
    [XmlElement(Order = 4)]
    public gRed gRed { get; set; }

    /// <summary>
    ///     UB35 - Valor do IBS de competência da UF
    /// </summary>
    [XmlElement(Order = 5)]
    public decimal vIBSUF
    {
      get => _vIbsUf.Arredondar(2);
      set => _vIbsUf = value.Arredondar(2);
    }
  }
}