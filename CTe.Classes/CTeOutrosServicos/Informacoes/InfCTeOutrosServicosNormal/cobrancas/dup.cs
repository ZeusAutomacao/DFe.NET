using System;
using System.Xml.Serialization;
using DFe.Classes;
using DFe.Utils;

namespace CTe.CTeOSDocumento.CTe.CTeOS.Informacoes.InfCTeNormal.cobrancas
{
    public class dup
    {
        private decimal? _vDup;
        public string nDup { get; set; }

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

        public decimal? vDup
        {
            get { return _vDup.Arredondar(2); }
            set { _vDup = value.Arredondar(2); }
        }

        public bool vDupSpecified { get { return vDup.HasValue; } }
    }
}