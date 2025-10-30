using System;
using System.Xml.Serialization;
using DFe.Utils;

namespace NFe.Classes.Informacoes.Cobranca
{
    public class dup
    {
        private decimal _vDup;

        /// <summary>
        ///     Y08 - Número da Duplicata
        /// </summary>
        public string nDup { get; set; }

        /// <summary>
        ///     Y09 - Data de vencimento
        /// </summary>
        [XmlIgnore]
        public DateTime? dVenc { get; set; }

        [XmlElement(ElementName = "dVenc")]
        public string ProxydVenc
        {
            get
            {
                if (dVenc == null) return null;

                return dVenc.Value.ParaDataString();
            }
            set { dVenc = Convert.ToDateTime(value); }
        }

        /// <summary>
        ///     Y10 - Valor da duplicata
        /// </summary>
        public decimal vDup
        {
            get { return _vDup; }
            set { _vDup = value.Arredondar(2); }
        }
    }
}