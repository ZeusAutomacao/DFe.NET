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
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;

namespace NFe.Classes.Informacoes.Detalhe.Tributacao.Federal
{
    public class IPITrib : IPIBasico
    {
        private decimal? _vBc;
        private decimal? _pIpi;
        private decimal? _qUnid;
        private decimal? _vUnid;
        private decimal? _vIpi;

        /// <summary>
        ///     O09 - Código da Situação Tributária do IPI:
        /// </summary>
        public CSTIPI CST { get; set; }

        /// <summary>
        ///     O10 - Valor da BC do IPI
        /// </summary>
        public decimal? vBC
        {
            get { return _vBc.Arredondar(2); }
            set { _vBc = value.Arredondar(2); }
        }

        /// <summary>
        ///     O13 - Alíquota do IPI
        /// </summary>
        public decimal? pIPI
        {
            get { return _pIpi.Arredondar(4); }
            set { _pIpi = value.Arredondar(4); }
        }

        /// <summary>
        ///     O11 - Quantidade total na unidade padrão para tributação (somente para os produtos tributados por unidade)
        /// </summary>
        public decimal? qUnid
        {
            get { return _qUnid.Arredondar(4); }
            set { _qUnid = value.Arredondar(4); }
        }

        /// <summary>
        ///     O12 - Valor por Unidade Tributável
        /// </summary>
        public decimal? vUnid
        {
            get { return _vUnid.Arredondar(4); }
            set { _vUnid = value.Arredondar(4); }
        }

        /// <summary>
        ///     O14 - Valor do IPI
        /// </summary>
        public decimal? vIPI
        {
            get { return _vIpi.Arredondar(2); }
            set { _vIpi = value.Arredondar(2); }
        }

        public bool ShouldSerializevBC()
        {
            return vBC.HasValue;
        }

        public bool ShouldSerializepIPI()
        {
            return pIPI.HasValue;
        }

        public bool ShouldSerializeqUnid()
        {
            return qUnid.HasValue;
        }

        public bool ShouldSerializevUnid()
        {
            return vUnid.HasValue;
        }

        public bool ShouldSerializevIPI()
        {
            return vIPI.HasValue;
        }
    }
}
