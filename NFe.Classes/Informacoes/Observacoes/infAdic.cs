using System.Collections.Generic;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Observacoes
{
    public class infAdic
    {
        /// <summary>
        ///     Z02 - Informações Adicionais de Interesse do Fisco
        /// </summary>
        public string infAdFisco { get; set; }

        /// <summary>
        ///     Z03 - Informações Complementares de interesse do Contribuinte
        /// </summary>
        public string infCpl { get; set; }

        public bool ShouldSerializeinfCpl()
        {
            return !string.IsNullOrEmpty(infCpl);
        }

        /// <summary>
        ///     Z04 - Grupo Campo de uso livre do contribuinte
        ///     <para>Ocorrência: 0-10</para>
        /// </summary>
        [XmlElement(ElementName = nameof(obsCont))]
        public List<obsCont> obsCont { get; set; }

        /// <summary>
        ///     Z07 - Grupo Campo de uso livre do Fisco
        ///     <para>Ocorrência: 0-10</para>
        /// </summary>
        [XmlElement(ElementName = nameof(obsFisco))]
        public List<obsFisco> obsFisco { get; set; }

        /// <summary>
        ///     Z10 - Grupo Processo referenciado
        ///     <para>Ocorrência: 0-100</para>
        /// </summary>
        [XmlElement(nameof(procRef))]
        public List<procRef> procRef { get; set; }
    }
}