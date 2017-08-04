using System;
using System.Xml.Serialization;
using DFe.Entidades;

namespace DFe.MDFe.Classes.Informacoes.Emitente
{
    [Serializable]
    public class enderEmit
    {
        /// <summary>
        /// 3 - Logradouro
        /// </summary>
        [XmlElement(ElementName = "xLgr")]
        public string xLgr { get; set; }

        /// <summary>
        /// 3 - Número 
        /// </summary>
        [XmlElement(ElementName = "nro")]
        public string nro { get; set; }

        /// <summary>
        /// 3 - Complemento
        /// </summary>
        [XmlElement(ElementName = "xCpl")]
        public string xCpl { get; set; }

        /// <summary>
        /// 3 - Bairro
        /// </summary>
        [XmlElement(ElementName = "xBairro")]
        public string xBairro { get; set; }

        /// <summary>
        /// 3 - Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.
        /// </summary>
        [XmlElement(ElementName = "cMun")]
        public long cMun { get; set; }

        /// <summary>
        /// 3 - Nome do município, , informar EXTERIOR para operações com o exterior
        /// </summary>
        [XmlElement(ElementName = "xMun")]
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

        /// <summary>
        /// 3 - Sigla da UF, , informar EX para operações com o exterior.
        /// </summary>
        [XmlIgnore]
        public Estado UF { get; set; }

        /// <summary>
        /// Proxy para pegar SiglaUF do estado
        /// </summary>
        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return UF.GetSiglaUfString(); }
            set { UF = UF.SiglaParaEstado(value); }
        }

        /// <summary>
        /// 3 - Telefone
        /// </summary>
        [XmlElement(ElementName = "fone")]
        public string fone { get; set; }

        /// <summary>
        /// 3 - Endereço de E-mail 
        /// </summary>
        [XmlElement(ElementName = "email")]
        public string email { get; set; }
    }
}