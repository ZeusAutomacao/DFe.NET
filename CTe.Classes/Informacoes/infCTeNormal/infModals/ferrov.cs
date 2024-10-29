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