using System;
using System.Xml.Serialization;
using CTe.Classes.Informacoes.Tipos;
using DFe.Utils;

namespace CTe.Classes.Informacoes.infCTeNormal.docAnteriores
{
    public class idDocAntPap
    {
        public tpDocAnterior tpDoc { get; set; }

        public string serie { get; set; }

        public string subser { get; set; }

        public string nDoc { get; set; }

        [XmlIgnore]
        public DateTime dEmi { get; set; }

        [XmlElement(ElementName = "dEmi")]
        public string ProxydEmi
        {
            get { 
                return dEmi.ParaDataString();
            }
            set { 
                dEmi = Convert.ToDateTime(value); 
            }
        }
    }
}