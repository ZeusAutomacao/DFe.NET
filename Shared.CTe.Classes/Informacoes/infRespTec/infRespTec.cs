using System.Xml.Serialization;

namespace CTe.Classes.Informacoes.infRespTec
{
    public class infRespTec
    {
        public string CNPJ { get; set; }
        public string xContato { get; set; }
        public string email { get; set; }
        public string fone { get; set; }

        [XmlIgnore]
        public int? idCSRT { get; set; }
        public bool idCSRTSpecified
        {
            get { return idCSRT.HasValue; }
        }


        [XmlElement(ElementName = "idCSRT")]
        public string ProxyidCSRT
        {
            get { return idCSRT != null ? idCSRT.Value.ToString("D3") : null; }
            set { idCSRT = int.Parse(value); }
        }

        public string hashCSRT { get; set; }
    }
}
