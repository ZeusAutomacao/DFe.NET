using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.Cana
{
    public class forDia
    {
        private decimal _qtde;

        /// <summary>
        ///     ZC05 - Dia
        /// </summary>
        [XmlAttribute]
        public int dia { get; set; }

        /// <summary>
        ///     ZC06 - Quantidade
        /// </summary>
        public decimal qtde
        {
            get { return _qtde; }
            set { _qtde = value.Arredondar(10); }
        }
    }
}