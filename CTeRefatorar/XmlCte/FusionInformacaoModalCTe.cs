using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte
{
    [Serializable]
    public class FusionInformacaoModalCTe
    {
        [XmlAttribute(AttributeName = "versaoModal")]
        public string Versao { get; set; } = "2.00";

        [XmlElement(ElementName = "rodo")]
        public FusionRodoviarioCTe Rodoviario { get; set; }

        public FusionInformacaoModalCTe()
        {
            Rodoviario = new FusionRodoviarioCTe();
        }
    }

    [Serializable]
    public class FusionRodoviarioCTe
    {
        [XmlElement(ElementName = "RNTRC")]
        public string Rntrc { get; set; }

        [XmlElement(ElementName = "dPrev")]
        public string DataPrevisaoEntrega { get; set; }

        [XmlElement(ElementName = "lota")]
        public FusionIndicadorLotacaoCTe IndicadorLotacao { get; set; }

        [XmlElement(ElementName = "CIOT")]
        public string Ciot { get; set; }

        [XmlElement(ElementName = "veic")]
        public List<FusionVeiculoRodoviarioCTe> VeiculosRodoviarios { get; set; }

        [XmlElement(ElementName = "moto")]
        public List<FusionMotoristaRodoviarioCTe> Motoristas { get; set; }

        public FusionRodoviarioCTe()
        {
            VeiculosRodoviarios = new List<FusionVeiculoRodoviarioCTe>();
            Motoristas = new List<FusionMotoristaRodoviarioCTe>();
        }
    }

    [Serializable]
    public class FusionMotoristaRodoviarioCTe
    {
        [XmlElement(ElementName = "xNome")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "CPF")]
        public string Cpf { get; set; }
    }

    [Serializable]
    public enum FusionIndicadorLotacaoCTe
    {
        [XmlEnum("0")]
        Nao,

        [XmlEnum("1")]
        Sim
    }

    [Serializable]
    public class FusionVeiculoRodoviarioCTe
    {
        [XmlElement(ElementName = "cInt")]
        public string CodigoInterno { get; set; }

        [XmlElement(ElementName = "RENAVAM")]
        public string Renavam { get; set; }

        [XmlElement(ElementName = "placa")]
        public string Placa { get; set; }

        [XmlElement(ElementName = "tara")]
        public int Tara { get; set; }

        [XmlElement(ElementName = "capKG")]
        public int CapacidadeEmKg { get; set; }

        [XmlElement(ElementName = "capM3")]
        public short CapacidadeEmM3 { get; set; }

        [XmlElement(ElementName = "tpProp")]
        public FusionProprietarioVeiculoCTe Proprietario { get; set; }

        [XmlElement(ElementName = "tpVeic")]
        public FusionTipoVeiculoCTe TipoVeiculo { get; set; }

        [XmlElement(ElementName = "tpRod")]
        public FusionRodadoCTe TipoRodado { get; set; }

        [XmlElement(ElementName = "tpCar")]
        public FusionTipoCarroceriaCTe TipoCarroceria { get; set; }

        [XmlElement(ElementName = "UF")]
        public string SiglaUF { get; set; }

        [XmlElement(ElementName = "prop")]
        public FusionProprietarioVeiculoRodoviarioCTe ProprietarioVeiculo { get; set; }
    }

    [Serializable]
    public class FusionProprietarioVeiculoRodoviarioCTe
    {
        [XmlElement(ElementName = "CPF")]
        public string Cpf { get; set; }

        [XmlElement(ElementName = "CNPJ")]
        public string Cnpj { get; set; }

        [XmlElement(ElementName = "RNTRC")]
        public string Rntrc { get; set; }

        [XmlElement(ElementName = "xNome")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "IE")]
        public string InscricaoEstadual { get; set; }

        [XmlElement(ElementName = "UF")]
        public string SiglaUf { get; set; }

        [XmlElement(ElementName = "tpProp")]
        public FusionTipoProprietarioCTe TipoProprietario { get; set; }
    }

    public enum FusionProprietarioVeiculoCTe
    {
        [XmlEnum("P")]
        Proprietario,

        [XmlEnum("T")]
        Terceiro
    }

    public enum FusionTipoVeiculoCTe
    {
        [XmlEnum("0")]
        Tracao,

        [XmlEnum("1")]
        Reboque
    }

    public enum FusionRodadoCTe
    {
        [XmlEnum("00")]
        NaoAplicavel,

        [XmlEnum("01")]
        Truck,

        [XmlEnum("02")]
        Toco,

        [XmlEnum("03")]
        CavaloMecanico,

        [XmlEnum("04")]
        Van,

        [XmlEnum("05")]
        Utilitario,

        [XmlEnum("06")]
        Outro
    }

    public enum FusionTipoCarroceriaCTe
    {
        [XmlEnum("00")]
        NaoAplicavel,

        [XmlEnum("01")]
        Aberta,

        [XmlEnum("02")]
        FechadaBau,

        [XmlEnum("03")]
        Granelera,

        [XmlEnum("04")]
        PortaContainer,

        [XmlEnum("05")]
        Sider
    }

    public enum FusionTipoProprietarioCTe
    {
        [XmlEnum("0")]
        TacAgregado,

        [XmlEnum("1")]
        TacIndependente,

        [XmlEnum("2")]
        Outros
    }
}