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
    public class infTribFed
    {
        private decimal? _vPis;
        private decimal? _vCofins;
        private decimal? _vIr;
        private decimal? _vInss;
        private decimal? _vCsll;

        public decimal? vPIS
        {
            get { return _vPis.Arredondar(2); }
            set { _vPis = value.Arredondar(2); }
        }

        public decimal? vCOFINS
        {
            get { return _vCofins.Arredondar(2); }
            set { _vCofins = value.Arredondar(2); }
        }

        public decimal? vIR
        {
            get { return _vIr.Arredondar(2); }
            set { _vIr = value.Arredondar(2); }
        }

        public decimal? vINSS
        {
            get { return _vInss.Arredondar(2); }
            set { _vInss = value.Arredondar(2); }
        }

        public decimal? vCSLL
        {
            get { return _vCsll.Arredondar(2); }
            set { _vCsll = value.Arredondar(2); }
        }


        public bool vPISSpecified { get { return vPIS.HasValue; } }
        public bool vCOFINSSpecified { get { return vCOFINS.HasValue; } }
        public bool vIRSpecified { get { return vIR.HasValue; } }
        public bool vINSSSpecified { get { return vINSS.HasValue; } }
        public bool vCSLLSpecified { get { return vCSLL.HasValue; } }
    }
}