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

using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;
using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.Impostos.IBSCBS
{
    public class gTribRegular
    {
        private string _cClassTribReg;
        private decimal _pAliqEfetRegIbsUf;
        private decimal _vTribRegIbsUf;
        private decimal _pAliqEfetRegIbsMun;
        private decimal _vTribRegIbsMun;
        private decimal _pAliqEfetRegCbs;
        private decimal _vTribRegCbs;

        [XmlElement(Order = 1)]
        public CSTIBSCBS CSTReg { get; set; }

        [XmlElement(Order = 2)]
        public string cClassTribReg
        {
            get => _cClassTribReg;
            set => _cClassTribReg = value;
        }

        [XmlElement(Order = 3)]
        public decimal pAliqEfetRegIBSUF
        {
            get => _pAliqEfetRegIbsUf.Arredondar(4);
            set => _pAliqEfetRegIbsUf = value.Arredondar(4);
        }

        [XmlElement(Order = 4)]
        public decimal vTribRegIBSUF
        {
            get => _vTribRegIbsUf.Arredondar(2);
            set => _vTribRegIbsUf = value.Arredondar(2);
        }

        [XmlElement(Order = 5)]
        public decimal pAliqEfetRegIBSMun
        {
            get => _pAliqEfetRegIbsMun.Arredondar(4);
            set => _pAliqEfetRegIbsMun = value.Arredondar(4);
        }

        [XmlElement(Order = 6)]
        public decimal vTribRegIBSMun
        {
            get => _vTribRegIbsMun.Arredondar(2);
            set => _vTribRegIbsMun = value.Arredondar(2);
        }

        [XmlElement(Order = 7)]
        public decimal pAliqEfetRegCBS
        {
            get => _pAliqEfetRegCbs.Arredondar(4);
            set => _pAliqEfetRegCbs = value.Arredondar(4);
        }

        [XmlElement(Order = 8)]
        public decimal vTribRegCBS
        {
            get => _vTribRegCbs.Arredondar(2);
            set => _vTribRegCbs = value.Arredondar(2);
        }

        /// <summary>
        /// Define o valor de cClassTrib a partir de um inteiro
        /// </summary>
        public void SetcClassTrib(int intValue)
        {
            _cClassTribReg = intValue.ToString("D6");
        }
    }
}
