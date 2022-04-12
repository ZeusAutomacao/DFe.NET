using System.Xml.Serialization;

namespace Shared.NFe.Classes.Informacoes.InfRespTec
{
    public class infRespTec
    {
        public string CNPJ { get; set; }

        public string xContato { get; set; }

        public string email { get; set; }

        public string fone { get; set; }

        [XmlIgnore]
        public int? idCSRT { get; set; }

        [XmlElement(ElementName = "idCSRT")]
        public string ProxyidCSRT
        {
            get
            {
                if (idCSRT == null) return null;

                return idCSRT.Value.ToString("D2");
            }
            set
            {
                if (value == null)
                {
                    idCSRT = null;
                    return;
                }
                idCSRT = int.Parse(value);
            }
        }

        public string hashCSRT { get; set; }
    }
}
