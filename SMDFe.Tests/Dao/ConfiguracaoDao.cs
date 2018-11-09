
using System.IO;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using SMDFe.Tests.Entidades;
using SMDFe.Utils.Flags;

namespace SMDFe.Tests.Dao
{
    public class ConfiguracaoDao
    {
        private Configuracao configura;

        public ConfiguracaoDao()
        {
            this.configura = new Configuracao()
            {
                Empresa = CarregaDadosEmpresa(),
                ConfigWebService = CarregaConfigWebServices(),
                CertificadoDigital = CarregaDadosCertificadoDigital(),
                DiretorioSalvarXml = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName,
                IsSalvarXml = true

            };

        }

        private Empresa CarregaDadosEmpresa()
        {
            var empresa = new Empresa()
            {
                Cnpj = "00000000000000",
                Bairro = "INEXISTENTE",
                Cep = "00000000",
                CodigoIbgeMunicipio = 2802908,
                Complemento = "1 andar",
                Email = "ficticia@gmail.com",
                InscricaoEstadual = "000000000",
                Logradouro = "RUA EXISTE EM CANTO NENHUM",
                Nome = "FICTICIA TESTE LTDA ME",
                NomeFantasia = "FICTICIA TESTE",
                NomeMunicipio = "LUGAR NENHUM",
                Numero = "000",
                RNTRC = "000000",
                SiglaUf = Estado.SE,
                Telefone = "00000000"
            };
            return empresa;
        }

        private ConfigCertificadoDigital CarregaDadosCertificadoDigital()
        {
            var certificado = new ConfigCertificadoDigital()
            {
                CaminhoArquivo = "\\DefinaCaminhoDoCertificado",
                ManterEmCache = false,
                NumeroDeSerie = "00000000000000000000000000000000",
                Senha = "0000"

            };
            return certificado;
        }

        private ConfigWebService CarregaConfigWebServices()
        {
            var configWebServices = new ConfigWebService()
            {
                Ambiente = TipoAmbiente.Homologacao,
                Serie = 1,
                VersaoLayout = VersaoServico.Versao300,
                CaminhoSchemas = "C:\\Users\\Usuario\\DFe.NET\\DFe.NET\\SMDFe.Tests\\Schemas",
                Numeracao = 1,
                TimeOut = 1000,
                UfEmitente = Estado.SE

            };
            return configWebServices;
        }

        public Configuracao GetConfiguracao()
        {
            return this.configura;
        }
    
    }
}