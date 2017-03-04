using System.Xml.Serialization;
using CTe.Classes.Ext;
using CTeDLL.Classes.Informacoes;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Assinatura;
using DFe.Utils;

namespace CTe.Classes
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class CTe
    {
        /// <summary>
        /// CTe Modelo 67/ CTE Ordem de Serviço
        /// CTeOs
        /// </summary>
        [XmlIgnore]
        public versao? versao { get; set; }

        [XmlAttribute(AttributeName = "versao")]
        public string ProxyVersao
        {
            get
            {
                if (versao == null) return null;
                return versao.Value.GetString();
            }
            set
            {
                if(value.Equals("2.00"))
                    versao = CTeDLL.Classes.Servicos.Tipos.versao.ve200;

                if(value.Equals("3.00"))
                    versao = CTeDLL.Classes.Servicos.Tipos.versao.ve300;
            }
        }

        public bool versaoSpecified => versao.HasValue;

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public infCte infCte;

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature;

        public static CTe LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<CTe>(xml);
        }

        public static CTe LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<CTe>(caminhoArquivoXml);
        }
    }
}