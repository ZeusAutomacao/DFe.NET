using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using SMDFe.TestesServicos.Entidades;


namespace SMDFe.TestesServicos
{
    public class ConfiguracaoDao
    {
        private readonly string _caminhoAplicacao;
        private string _nomeArquivoXml = "Configuracao.xml";

        public ConfiguracaoDao()
        {
            _caminhoAplicacao = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public void SalvarConfiguracao(ConfiguracaoMdfe configuracao)
        {
            using (var stream = new StreamWriter(_caminhoAplicacao + @"\" + _nomeArquivoXml))
            {
                var xmlSerializer = new XmlSerializer(typeof(ConfiguracaoMdfe));

                xmlSerializer.Serialize(XmlWriter.Create(stream), configuracao);

                stream.Flush();
            }
        }

        public ConfiguracaoMdfe BuscarConfiguracao()
        {
            if (!File.Exists(_caminhoAplicacao + @"\" + _nomeArquivoXml)) return null;

            ConfiguracaoMdfe configuracao;

            using (var reader = new StreamReader(_caminhoAplicacao + @"\" + _nomeArquivoXml))
            {
                var xmlSerializer = new XmlSerializer(typeof(ConfiguracaoMdfe));

                var objeto = xmlSerializer.Deserialize(XmlReader.Create(reader));

                configuracao = objeto as ConfiguracaoMdfe;
            }

            return configuracao;
        }
    }
}