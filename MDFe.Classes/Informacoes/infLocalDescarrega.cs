using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes
{
    [Serializable]
    public class infLocalDescarrega
    {
        public string CEP { get; set; }

        [XmlIgnore]
        public decimal? latitude { get; set; }

        [XmlElement("latitude")]
        public string latitudeProxy
        {
            get
            {
                if (latitude == null) return null;
                return latitude.ToString();
            }
            set { latitude = decimal.Parse(value); }
        }

        [XmlIgnore]
        public decimal? Longitude { get; set; }

        [XmlElement("Longitude")]
        public string LongitudeProxy
        {
            get
            {
                if (Longitude == null) return null;
                return Longitude.ToString();
            }
            set { Longitude = decimal.Parse(value); }
        }
    }
}