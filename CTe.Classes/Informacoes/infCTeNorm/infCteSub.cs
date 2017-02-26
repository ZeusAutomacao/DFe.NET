using System;
using System.Xml.Serialization;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class infCteSub
    {
        public string chCte { get; set; }

        /// <summary>
        /// Versao 3.0
        /// </summary>
        public string refCteAnu { get; set; }

        public tomaICMS tomaICMS { get; set; }

        /// <summary>
        /// Versao 3.0
        /// Tag com efeito e utilização aguardando
        /// legislação, não utilizar antes de NT
        /// específica tratar desse procedimento
        /// </summary>
        public byte indAlteraToma { get; set; }

        /// <summary>
        /// Versao 2.0
        /// </summary>
        public tomaNaoICMS tomaNaoICMS { get; set; }
    }
}
