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
    /// Grupo de Informações da Composição do Valor do IBS e da CBS em Compras Governamentais
    /// </summary>
    public class gTribCompraGov
    {
        private decimal _pAliqIBSUF;
        private decimal _vTribIBSUF;
        private decimal _pAliqIBSMun;
        private decimal _vTribIBSMun;
        private decimal _pAliqCBS;
        private decimal _vTribCBS;

        /// <summary>
        /// Alíquota do IBS UF nas compras governamentais (em percentual)
        /// </summary>
        public decimal pAliqIBSUF
        {
            get { return _pAliqIBSUF.Arredondar(2); }
            set { _pAliqIBSUF = value.Arredondar(2); }
        }

        /// <summary>
        /// Valor do tributo do IBS UF nas compras governamentais
        /// </summary>
        public decimal vTribIBSUF
        {
            get { return _vTribIBSUF.Arredondar(2); }
            set { _vTribIBSUF = value.Arredondar(2); }
        }

        /// <summary>
        /// Alíquota do IBS Município nas compras governamentais (em percentual)
        /// </summary>
        public decimal pAliqIBSMun
        {
            get { return _pAliqIBSMun.Arredondar(2); }
            set { _pAliqIBSMun = value.Arredondar(2); }
        }

        /// <summary>
        /// Valor do tributo do IBS Município nas compras governamentais
        /// </summary>
        public decimal vTribIBSMun
        {
            get { return _vTribIBSMun.Arredondar(2); }
            set { _vTribIBSMun = value.Arredondar(2); }
        }

        /// <summary>
        /// Alíquota da CBS nas compras governamentais (em percentual)
        /// </summary>
        public decimal pAliqCBS
        {
            get { return _pAliqCBS.Arredondar(2); }
            set { _pAliqCBS = value.Arredondar(2); }
        }

        /// <summary>
        /// Valor do tributo da CBS nas compras governamentais
        /// </summary>
        public decimal vTribCBS
        {
            get { return _vTribCBS.Arredondar(2); }
            set { _vTribCBS = value.Arredondar(2); }
        }
    }
}
