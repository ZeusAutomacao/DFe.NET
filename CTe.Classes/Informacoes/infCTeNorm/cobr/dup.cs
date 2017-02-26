using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Utils;

namespace CTeDLL.Classes.Informacoes.InfCTeNormal
{
    public class dup
    {
        public string nDup { get; set; }

        [XmlIgnore]
        public DateTime? dVenc { get; set; }

        [XmlElement(ElementName = "dVenc")]
        public string ProxydVenc {
            get
            {
                if (dVenc == null) return null;

                return dVenc.Value.ParaDataString();
            }
            set { dVenc = Convert.ToDateTime(value); } }

        public decimal? vDup { get; set; }

        public bool vDupSpecified => vDup.HasValue;
    }
}
