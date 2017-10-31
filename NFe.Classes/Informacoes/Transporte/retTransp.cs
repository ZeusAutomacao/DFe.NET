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
namespace NFe.Classes.Informacoes.Transporte
{
    public class retTransp
    {
        private decimal _vServ;
        private decimal _vBcRet;
        private decimal _pIcmsRet;
        private decimal _vIcmsRet;

        /// <summary>
        ///     X12 - Valor do Serviço
        /// </summary>
        public decimal vServ
        {
            get { return _vServ; }
            set { _vServ = value.Arredondar(2); }
        }

        /// <summary>
        ///     X13 - BC da Retenção do ICMS
        /// </summary>
        public decimal vBCRet
        {
            get { return _vBcRet; }
            set { _vBcRet = value.Arredondar(2); }
        }

        /// <summary>
        ///     X14 - Alíquota da Retenção
        /// </summary>
        public decimal pICMSRet
        {
            get { return _pIcmsRet; }
            set { _pIcmsRet = value.Arredondar(4); }
        }

        /// <summary>
        ///     X15 - Valor do ICMS Retido
        /// </summary>
        public decimal vICMSRet
        {
            get { return _vIcmsRet; }
            set { _vIcmsRet = value.Arredondar(2); }
        }

        /// <summary>
        ///     X16 - CFOP
        /// </summary>
        public int CFOP { get; set; }

        /// <summary>
        ///     X17 - Código do município de ocorrência do fato gerador do ICMS do transporte
        /// </summary>
        public long cMunFG { get; set; }
    }
}