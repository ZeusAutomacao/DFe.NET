using System.Collections.Generic;
using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Extencoes;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class ferrov
    {
        public tpTraf tpTraf { get; set; }

        public trafMut trafMut { get; set; }

        public string fluxo { get; set; }
        public string idTrem { get; set; }
        public decimal vFrete { get; set; }

        [XmlElement(ElementName = "ferroEnv")]
        public List<ferroEnv> ferroEnv { get; set; }

        [XmlElement(ElementName = "detVag")]
        public List<detVag> detVag { get; set; }

    }

    public class trafMut
    {
        public respFat respFat { get; set; }
        public ferrEmi ferrEmi { get; set; }

        public decimal? vFrete { get; set; }
        public bool vFreteSpecified => vFrete.HasValue;
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
        public string CEP { get; set; }
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
        public string nVag { get; set; }
        public decimal? cap { get; set; }
        public bool capSpecified => cap.HasValue;

        public string tpVag { get; set; }
        public decimal pesoR { get; set; }
        public decimal pesoBC { get; set; }

    }
}