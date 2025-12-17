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

using DFe.Classes;

namespace CTe.Classes.Informacoes.Impostos.InformacoesIbsCbs
{
    /// <summary>
    /// Grupo de Informações da Tributação Regular
    /// </summary>
    public class gTribRegular
    {
        private decimal _pAliqEfetRegIBSUF;
        private decimal _vTribRegIBSUF;
        private decimal _pAliqEfetRegIBSMun;
        private decimal _vTribRegIBSMun;
        private decimal _pAliqEfetRegCBS;
        private decimal _vTribRegCBS;

        /// <summary>
        /// Código de Situação Tributária da Tributação Regular
        /// </summary>
        public string CSTReg { get; set; }

        /// <summary>
        /// Código de Classificação Tributária da Tributação Regular
        /// </summary>
        public string cClassTribReg { get; set; }

        /// <summary>
        /// Percentual da alíquota efetiva da Tributação Regular do IBS UF (em percentual)
        /// </summary>
        public decimal pAliqEfetRegIBSUF
        {
            get { return _pAliqEfetRegIBSUF.Arredondar(2); }
            set { _pAliqEfetRegIBSUF = value.Arredondar(2); }
        }

        /// <summary>
        /// Valor da Tributação Regular do IBS UF
        /// </summary>
        public decimal vTribRegIBSUF
        {
            get { return _vTribRegIBSUF.Arredondar(2); }
            set { _vTribRegIBSUF = value.Arredondar(2); }
        }

        /// <summary>
        /// Percentual da alíquota efetiva da Tributação Regular do IBS Município (em percentual)
        /// </summary>
        public decimal pAliqEfetRegIBSMun
        {
            get { return _pAliqEfetRegIBSMun.Arredondar(2); }
            set { _pAliqEfetRegIBSMun = value.Arredondar(2); }
        }

        /// <summary>
        /// Valor da Tributação Regular do IBS Município
        /// </summary>
        public decimal vTribRegIBSMun
        {
            get { return _vTribRegIBSMun.Arredondar(2); }
            set { _vTribRegIBSMun = value.Arredondar(2); }
        }

        /// <summary>
        /// Percentual da alíquota efetiva da Tributação Regular da CBS (em percentual)
        /// </summary>
        public decimal pAliqEfetRegCBS
        {
            get { return _pAliqEfetRegCBS.Arredondar(2); }
            set { _pAliqEfetRegCBS = value.Arredondar(2); }
        }

        /// <summary>
        /// Valor da Tributação Regular da CBS
        /// </summary>
        public decimal vTribRegCBS
        {
            get { return _vTribRegCBS.Arredondar(2); }
            set { _vTribRegCBS = value.Arredondar(2); }
        }
    }
}
