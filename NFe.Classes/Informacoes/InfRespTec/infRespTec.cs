using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.InfRespTec
{
    /// <summary>
    /// Informação do responsável técnico pela emissão de nota fiscal
    /// </summary>
    public class infRespTec
    {
        public string CNPJ { get; set; }

        public string xContato { get; set; }

        public string email { get; set; }

        public string fone { get; set; }

        [XmlIgnore]
        public string idCSRT { get; set; }

        [XmlElement(ElementName = "idCSRT")]
        public string ProxyidCSRT
        {
            get
            {
                if (idCSRT == null) return null;

                return string.Format("D2", idCSRT);/* idCSRT.ToString("D2");*/
            }
            set
            {
                if (value == null)
                {
                    idCSRT = null;
                    return;
                }
                idCSRT = value;
            }
        }

        public string hashCSRT { get; set; }
    }
}
