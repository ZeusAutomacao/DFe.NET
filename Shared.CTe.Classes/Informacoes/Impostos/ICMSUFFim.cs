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

namespace CTe.Classes.Informacoes.Impostos
{
    public class ICMSUFFim
    {
        private decimal _vBcufFim;
        private decimal _pFcpufFim;
        private decimal _pIcmsufFim;
        private decimal _pIcmsInter;
        private decimal? _pIcmsInterPart;
        private decimal _vFcpufFim;
        private decimal _vIcmsufFim;
        private decimal _vIcmsufIni;

        public decimal vBCUFFim
        {
            get { return _vBcufFim.Arredondar(2); }
            set { _vBcufFim = value.Arredondar(2); }
        }

        public decimal pFCPUFFim
        {
            get { return _pFcpufFim.Arredondar(2); }
            set { _pFcpufFim = value.Arredondar(2); }
        }

        public decimal pICMSUFFim
        {
            get { return _pIcmsufFim.Arredondar(2); }
            set { _pIcmsufFim = value.Arredondar(2); }
        }

        public decimal pICMSInter
        {
            get { return _pIcmsInter.Arredondar(2); }
            set { _pIcmsInter = value.Arredondar(2); }
        }

        public decimal? pICMSInterPart
        {
            get { return _pIcmsInterPart.Arredondar(2); }
            set { _pIcmsInterPart = value.Arredondar(2); }
        }

        public bool pICMSInterPartSpecified
        {
            get
            {
                return this._pIcmsInterPart.HasValue;
            }
        }

        public decimal vFCPUFFim
        {
            get { return _vFcpufFim.Arredondar(2); }
            set { _vFcpufFim = value.Arredondar(2); }
        }

        public decimal vICMSUFFim
        {
            get { return _vIcmsufFim.Arredondar(2); }
            set { _vIcmsufFim = value.Arredondar(2); }
        }

        public decimal vICMSUFIni
        {
            get { return _vIcmsufIni.Arredondar(2); }
            set { _vIcmsufIni = value.Arredondar(2); }
        }
    }
}