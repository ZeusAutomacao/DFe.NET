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

using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;

namespace CTe.Classes.Informacoes.infCTeNormal.infModals
{
    public class ferrov : ContainerModal
    {
        private decimal _vFrete;
        public tpTraf tpTraf { get; set; }

        public trafMut trafMut { get; set; }

        public string fluxo { get; set; }
        public string idTrem { get; set; }

        public decimal vFrete
        {
            get { return _vFrete.Arredondar(2); }
            set { _vFrete = value.Arredondar(2); }
        }

        [XmlElement(ElementName = "ferroEnv")]
        public List<ferroEnv> ferroEnv { get; set; }

        [XmlElement(ElementName = "detVag")]
        public List<detVag> detVag { get; set; }

    }

    public class trafMut
    {
        private decimal? _vFrete;
        public respFat respFat { get; set; }
        public ferrEmi ferrEmi { get; set; }

        public decimal? vFrete
        {
            get { return _vFrete.Arredondar(2); }
            set { _vFrete = value.Arredondar(2); }
        }

        public bool vFreteSpecified { get { return vFrete.HasValue; } }
        public string chCTeFerroOrigem { get; set; }

        [XmlElement(ElementName = "ferroEnv")]
        public List<ferroEnv> ferroEnv { get; set; }

        public string fluxo { get; set; }
    }

    public class ferroEnv
    {
        public string CNPJ { get; set; }
        public string cInt { get; set; }
        public string IE { get; set; }
        public string xNome { get; set; }

        public enderFerro enderFerro { get; set; }
    }

    public class enderFerro
    {
        public string xLgr { get; set; }
        public string nro { get; set; }
        public string xCpl { get; set; }
        public string xBairro { get; set; }
        public string cMun { get; set; }
        public string xMun { get; set; }
        /// <summary>
        /// 3 - CEP
        /// </summary>
        [XmlIgnore]
        public long CEP { get; set; }

        /// <summary>
        /// Proxy para colocar zeros a esquerda no CEP 
        /// </summary>
        [XmlElement(ElementName = "CEP")]
        public string ProxyCEP
        {
            get { return CEP.ToString("D8"); }
            set { CEP = long.Parse(value); }
        }

        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }
    }

    public class detVag
    {
        private decimal? _cap;
        public string nVag { get; set; }

        public decimal? cap
        {
            get { return _cap.Arredondar(3); }
            set { _cap = value.Arredondar(3); }
        }

        public bool capSpecified { get { return cap.HasValue; } }

        public string tpVag { get; set; }
        public decimal pesoR { get; set; }
        public decimal pesoBC { get; set; }

    }
}