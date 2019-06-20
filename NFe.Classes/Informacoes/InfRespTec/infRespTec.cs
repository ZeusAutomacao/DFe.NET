using System.Xml.Serialization;

namespace NFe.Classes.Informacoes.InfRespTec
{
    /// <summary>
    /// Informação do responsável técnico pela emissão de nota fiscal
    /// </summary>
    public class infRespTec
    {
        public long CNPJ { get; set; }

        public string xContato { get; set; }

        public string email { get; set; }

        public long fone { get; set; }

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
