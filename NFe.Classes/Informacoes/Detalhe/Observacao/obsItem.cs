using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Detalhe.Observacao
{
    public class obsItem
    {
        /// <summary>
        ///     VA02 - Grupo de observações de uso livre do Contribuinte
        ///     <para>Ocorrência: 0-1</para>
        /// </summary>
        [XmlElement(Namespace = nameof(Observacao))]
        public obsCont obsCont { get; set; }

        /// <summary>
        ///     VA05 - Grupo de observações de uso livre do Fisco
        ///     <para>Ocorrência: 0-1</para>
        /// </summary>
        [XmlElement(Namespace = nameof(Observacao))]
        public obsFisco obsFisco { get; set; }
    }
}
