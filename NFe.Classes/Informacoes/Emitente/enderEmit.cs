using System;
using System.Linq;
using System.Xml.Serialization;
using DFe.Classes.Entidades;

namespace NFe.Classes.Informacoes.Emitente
{
    public class enderEmit
    {
        private string _cep;

        /// <summary>
        ///     C06 - Logradouro
        /// </summary>
        public string xLgr { get; set; }

        /// <summary>
        ///     C07 - Número
        /// </summary>
        public string nro { get; set; }

        /// <summary>
        ///     C08 - Complemento
        /// </summary>
        public string xCpl { get; set; }

        /// <summary>
        ///     C09 - Bairro
        /// </summary>
        public string xBairro { get; set; }

        /// <summary>
        ///     C10 - Código do município
        ///     <para>Código do município (utilizar a tabela do IBGE), informar 9999999 para operações com o exterior.</para>
        /// </summary>
        public long cMun { get; set; }

        /// <summary>
        ///     C11 - Nome do município, informar EXTERIOR para operações com o exterior.
        /// </summary>
        public string xMun { get; set; }

        /// <summary>
        ///     C12 - Sigla da UF, informar EX para operações com o exterior.
        /// </summary>
        [XmlIgnore]
        public Estado UF { get; set; }

        [XmlElement(ElementName = "UF")]
        public string ProxyUF
        {
            get { return Enum.GetName(typeof(Estado), UF); }
            set { UF = (Estado)Enum.Parse(typeof(Estado), value); }
        }

        /// <summary>
        ///     C13 - Código do CEP
        ///     <para>Informar os zeros não significativos. (NT 2011/004)</para>
        /// </summary>
        public string CEP
        {
            get { return _cep; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("-", "");
                    if (!value.All(char.IsDigit))
                        throw new Exception(@"enderEmit\CEP deve receber somente números!");
                    if (value.Length != 8)
                        throw new Exception(string.Format(@"enderEmit\CEP deve ter 8 números. Tamanho informado: {0}!", value.Length));
                }
                _cep = value;
            }
        }

        /// <summary>
        ///     C14 - Código do País
        ///     <para>1058 - Brasil</para>
        /// </summary>
        public int? cPais { get; set; }

        /// <summary>
        ///     C15 - Nome do País
        ///     <para>Brasil ou BRASIL</para>
        /// </summary>
        public string xPais { get; set; }

        /// <summary>
        ///     C16 - Telefone
        ///     <para>
        ///         Preencher com o Código DDD + número do telefone. Nas operações com exterior é permitido informar o código do
        ///         país + código da localidade + número do telefone (v.2.0)
        ///     </para>
        /// </summary>
        public long? fone { get; set; }

        public bool ShouldSerializecPais()
        {
            return cPais.HasValue;
        }

        public bool ShouldSerializefone()
        {
            return fone.HasValue;
        }

        public bool ShouldSerializexCpl()
        {
            return !string.IsNullOrEmpty(xCpl);
        }
    }
}