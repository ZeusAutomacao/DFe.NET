using CTe.Classes.Simplificado.Carga;
using DFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CTe.Classes.Simplificado.Informacoes
{
    /// <summary>
    /// Informações do CT-e.
    /// </summary>
    public class infCte
    {
        /// <summary>
        /// Código da UF do emitente do CT-e. Utilizar a Tabela do IBGE.
        /// </summary>
        [XmlElement(ElementName = "cUF")]
        public int cUf { get; set; }

        /// <summary>
        /// Código numérico que compõe a Chave de Acesso.
        /// Número aleatório gerado pelo emitente para cada CT-e, com o objetivo de evitar acessos indevidos ao documento.
        /// </summary>
        [XmlElement(ElementName = "cCT")]
        public string cCt { get; set; }

        /// <summary>
        /// Código Fiscal de Operações e Prestações.
        /// </summary>
        [XmlElement(ElementName = "CFOP")]
        public string cfop { get; set; }

        /// <summary>
        /// Natureza da Operação.
        /// </summary>
        [XmlElement(ElementName = "natOp")]
        public string natOp { get; set; }

        /// <summary>
        /// Modelo do documento fiscal. Utilizar o código 57 para identificação do CT-e.
        /// </summary>
        [XmlElement(ElementName = "mod")]
        public int mod { get; set; }

        /// <summary>
        /// Série do CT-e. Preencher com "0" no caso de série única.
        /// </summary>
        [XmlElement(ElementName = "serie")]
        public int serie { get; set; }

        /// <summary>
        /// Número do CT-e.
        /// </summary>
        [XmlElement(ElementName = "nCT")]
        public string nCt { get; set; }

        /// <summary>
        /// Data e hora de emissão do CT-e.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhEmi { get; set; }

        /// <summary>
        /// Data e hora de emissão do CT-e. Formato AAAA-MM-DDTHH:MM:DD TZD.
        /// </summary>
        [XmlElement(ElementName = "dhEmi")]
        public string DhEmi
        {
            get { return dhEmi.ParaDataHoraStringUtc(); }
            set { dhEmi = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Formato de impressão do DACTE. Preencher com: 1 - Retrato; 2 - Paisagem.
        /// </summary>
        [XmlElement(ElementName = "tpImp")]
        public int tpImp { get; set; }

        /// <summary>
        /// Forma de emissão do CT-e.
        /// </summary>
        [XmlElement(ElementName = "tpEmis")]
        public int tpEmis { get; set; }

        /// <summary>
        /// Dígito Verificador da chave de acesso do CT-e.
        /// </summary>
        [XmlElement(ElementName = "cDV")]
        public int cDv { get; set; }

        /// <summary>
        /// Tipo do Ambiente. Preencher com: 1 - Produção; 2 - Homologação.
        /// </summary>
        [XmlElement(ElementName = "tpAmb")]
        public int tpAmb { get; set; }

        /// <summary>
        /// Tipo do CT-e Simplificado. Preencher com:
        /// 5 - CTe Simplificado;
        /// 6 - Substituição CTe Simplificado.
        /// </summary>
        [XmlElement(ElementName = "tpCTe")]
        public int tpCte { get; set; }

        /// <summary>
        /// Identificador do processo de emissão do CT-e.
        /// </summary>
        [XmlElement(ElementName = "procEmi")]
        public int procEmi { get; set; }

        /// <summary>
        /// Versão do processo de emissão.
        /// </summary>
        [XmlElement(ElementName = "verProc")]
        public string verProc { get; set; }

        /// <summary>
        /// Código do Município de envio do CT-e (de onde o documento foi transmitido). Utilizar a tabela do IBGE.
        /// </summary>
        [XmlElement(ElementName = "cMunEnv")]
        public int cMunEnv { get; set; }

        /// <summary>
        /// Nome do Município de envio do CT-e (de onde o documento foi transmitido).
        /// </summary>
        [XmlElement(ElementName = "xMunEnv")]
        public string xMunEnv { get; set; }

        /// <summary>
        /// Sigla da UF de envio do CT-e (de onde o documento foi transmitido).
        /// </summary>
        [XmlElement(ElementName = "UFEnv")]
        public string ufEnv { get; set; }

        /// <summary>
        /// Modal. Preencher com:
        /// 01-Rodoviário;
        /// 02-Aéreo;
        /// 03-Aquaviário.
        /// </summary>
        [XmlElement(ElementName = "modal")]
        public string modal { get; set; }

        /// <summary>
        /// Tipo do Serviço. Preencher com:
        /// 0 - Normal;
        /// 1 - Subcontratação;
        /// 2 - Redespacho.
        /// </summary>
        [XmlElement(ElementName = "tpServ")]
        public int tpServ { get; set; }

        /// <summary>
        /// UF do início da prestação.
        /// </summary>
        [XmlElement(ElementName = "UFIni")]
        public string ufIni { get; set; }

        /// <summary>
        /// UF do término da prestação.
        /// </summary>
        [XmlElement(ElementName = "UFFim")]
        public string ufFim { get; set; }

        /// <summary>
        /// Indicador se o Recebedor retira no Aeroporto, Filial, Porto ou Estação de Destino.
        /// Preencher com: 0 - sim; 1 - não.
        /// </summary>
        [XmlElement(ElementName = "retira")]
        public int retira { get; set; }

        /// <summary>
        /// Detalhes do retira.
        /// </summary>
        [XmlElement(ElementName = "detRetira")]
        public string detRetira { get; set; }

        /// <summary>
        /// Data e Hora da entrada em contingência.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhCont { get; set; }

        /// <summary>
        /// Data e Hora da entrada em contingência. Formato AAAA-MM-DDTHH:MM:SS.
        /// </summary>
        [XmlElement(ElementName = "dhCont")]
        public string DhCont
        {
            get { return dhCont.ParaDataHoraStringUtc(); }
            set { dhCont = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        /// Justificativa da entrada em contingência.
        /// </summary>
        [XmlElement(ElementName = "xJust")]
        public string xJust { get; set; }

        /// <summary>
        /// Característica adicional do transporte.
        /// </summary>
        [XmlElement(ElementName = "caracAd")]
        public string caracAd { get; set; }

        /// <summary>
        /// Característica adicional do serviço.
        /// </summary>
        [XmlElement(ElementName = "caracSer")]
        public string caracSer { get; set; }

        /// <summary>
        /// Detalhamento das entregas/prestações do CT-e Simplificado.
        /// </summary>
        [XmlElement(ElementName = "detEntrega")]
        public List<DetEntrega> detEntrega { get; set; }


        /// <summary>
        /// Detalhamento das entregas/prestações do CT-e Simplificado.
        /// </summary>
        [XmlElement(ElementName = "det")]
        public List<det> det { get; set; }

        /// <summary>
        /// Informações da carga do CT-e.
        /// </summary>
        [XmlElement(ElementName = "infCarga")]
        public infCarga infCarga { get; set; }
    }

    /// <summary>
    /// Detalhamento das entregas/prestações do CT-e Simplificado.
    /// </summary>
    public class DetEntrega
    {
        /// <summary>
        /// Código do Município de início da prestação. Utilizar a tabela do IBGE.
        /// </summary>
        [XmlElement(ElementName = "cMunIni")]
        public int cMunIni { get; set; }

        /// <summary>
        /// Nome do Município do início da prestação.
        /// </summary>
        [XmlElement(ElementName = "xMunIni")]
        public string xMunIni { get; set; }

        /// <summary>
        /// Código do Município de término da prestação. Utilizar a tabela do IBGE.
        /// </summary>
        [XmlElement(ElementName = "cMunFim")]
        public int cMunFim { get; set; }

        /// <summary>
        /// Nome do Município do término da prestação.
        /// </summary>
        [XmlElement(ElementName = "xMunFim")]
        public string xMunFim { get; set; }

        /// <summary>
        /// Valor da Prestação do Serviço.
        /// </summary>
        [XmlElement(ElementName = "vPrest")]
        public decimal vPrest { get; set; }

        /// <summary>
        /// Valor a Receber.
        /// </summary>
        [XmlElement(ElementName = "vRec")]
        public decimal vRec { get; set; }
    }
}
