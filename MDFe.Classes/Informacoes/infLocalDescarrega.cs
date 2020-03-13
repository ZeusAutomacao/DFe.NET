using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infLocalDescarrega
    {
        public string CEP { get; set; }

        [XmlIgnore]
        public decimal latitude { get; set; }

        [XmlElement("latitude")]
        public string latitudeProxy
        {
            get { return latitude.ToString(); }
            set { latitude = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal Longitude { get; set; }

        [XmlElement("Longitude")]
        public string LongitudeProxy
        {
            get { return Longitude.ToString(); }
            set { Longitude = decimal.Parse(value); }
        }
    }
}