using System.Collections.Generic;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Classes;

namespace CTe.Classes.Informacoes.Identificacao
{
    public class gCompraGov
    {
        private decimal _pRedutor;

        public TipoEnteGov tpEnteGov { get; set; }

        public decimal pRedutor
        {
            get => _pRedutor.Arredondar(4);
            set => _pRedutor = value.Arredondar(4);
        }

        /// <summary>
        ///     Tipo da operação com o ente governamental
        /// </summary>
        public TipoOperacaoGov? tpOperGov { get; set; }

        public bool ShouldSerializetpOperGov()
        {
            return tpOperGov.HasValue;
        }

        /// <summary>
        ///     Chave de acesso do documento fiscal anterior (0 a 99 ocorrências).
        /// </summary>
        [XmlElement("refDFeAnt")]
        public List<string> refDFeAnt { get; set; }

        public bool ShouldSerializerefDFeAnt()
        {
            return refDFeAnt != null && refDFeAnt.Count > 0;
        }
    }
}
