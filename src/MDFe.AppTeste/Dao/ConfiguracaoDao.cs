using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using MDFe.AppTeste.Entidades;

namespace MDFe.AppTeste.Dao
{
    public class ConfiguracaoDao
    {
        private readonly string _caminhoAplicacao;
        private string _nomeArquivoXml = "Configuracao.xml";

        public ConfiguracaoDao()
        {
            _caminhoAplicacao = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public void SalvarConfiguracao(Configuracao configuracao)
        {
            using (var stream = new StreamWriter(_caminhoAplicacao + @"\" + _nomeArquivoXml))
            {
                var xmlSerializer = new XmlSerializer(typeof(Configuracao));

                xmlSerializer.Serialize(XmlWriter.Create(stream), configuracao);

                stream.Flush();
            }
        }

        public Configuracao BuscarConfiguracao()
        {
            if (!File.Exists(_caminhoAplicacao + @"\" + _nomeArquivoXml)) return null;

            Configuracao configuracao;

            using (var reader = new StreamReader(_caminhoAplicacao + @"\" + _nomeArquivoXml))
            {
                var xmlSerializer = new XmlSerializer(typeof(Configuracao));

                var objeto = xmlSerializer.Deserialize(XmlReader.Create(reader));

                configuracao = objeto as Configuracao;
            }

            return configuracao;
        }
    }
}