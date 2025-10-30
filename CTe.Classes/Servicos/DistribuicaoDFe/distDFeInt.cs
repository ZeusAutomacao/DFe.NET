using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTe.Classes.Servicos.DistribuicaoDFe
{
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    [XmlRoot("distDFeInt", Namespace = "http://www.portalfiscal.inf.br/cte", IsNullable = false)]
    public class distDFeInt
    {
        private const string ErroCpfCnpjPreenchidos = "Somente preencher um dos campos: CNPJ ou CPF, para um objeto do tipo dest!";
        private string _cNPJ;
        private string _cPF;

        /// <summary>
        /// A02 - Versão do leiaute
        /// </summary>
        [XmlAttribute()]
        public string versao { get; set; }

        /// <summary>
        /// A03 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// A04 - Código da UF do Autor
        /// </summary>
        public Estado cUFAutor { get; set; }

        /// <summary>
        /// A05 - CNPJ do interessado no DF-e
        /// </summary>
        public string CNPJ
        {
            get { return _cNPJ; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(_cPF))
                    _cNPJ = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        /// A06 - CPF do interessado no DF-e
        /// </summary>
        public string CPF
        {
            get { return _cPF; }
            set
            {
                if (string.IsNullOrEmpty(value)) return;
                if (string.IsNullOrEmpty(_cNPJ))
                    _cPF = value;
                else
                {
                    throw new ArgumentException(ErroCpfCnpjPreenchidos);
                }
            }
        }

        /// <summary>
        /// A07 - Grupo para distribuir DF-e de interesse
        /// </summary>
        public distNSU distNSU { get; set; }

        /// <summary>
        /// A09 - Grupo para consultar um DF-e a partir de um NSU específico
        /// </summary>
        public consNSU consNSU { get; set; }  


    }
}