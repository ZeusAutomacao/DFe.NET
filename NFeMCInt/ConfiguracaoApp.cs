using System;
using System.IO;
using System.Net;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils;
using NFe.Utils.Email;
using NFeMCInt.Properties;

namespace NFeMCInt
{
    public class ConfiguracaoApp
    {
        private ConfiguracaoServico _cfgServico;

        public ConfiguracaoApp()
        {
            CfgServico = ConfiguracaoServico.Instancia;
            CfgServico.tpAmb = TipoAmbiente.taHomologacao;
            CfgServico.tpEmis = TipoEmissao.teNormal;
            CfgServico.ProtocoloDeSeguranca = ServicePointManager.SecurityProtocol;
            Emitente = new emit { CPF = "",  CRT = CRT.SimplesNacional };
            EnderecoEmitente = new enderEmit();
            //ConfiguracaoEmail = new ConfiguracaoEmail("email@dominio.com", "senha", "Envio de NFE", Resources.MensagemHtml, "smtp.dominio.com", 587, true, true);
            ConfiguracaoCsc = new ConfiguracaoCsc("000001", "");
        }

        public ConfiguracaoServico CfgServico
        {
            get
            {
                ConfiguracaoServico.Instancia.CopiarPropriedades(_cfgServico);
                return _cfgServico;
            }
            set
            {
                _cfgServico = value;
                ConfiguracaoServico.Instancia.CopiarPropriedades(value);
            }
        }

        public emit Emitente { get; set; }
        public enderEmit EnderecoEmitente { get; set; }
        public ConfiguracaoEmail ConfiguracaoEmail { get; set; }
        public ConfiguracaoCsc ConfiguracaoCsc { get; set; }

        /// <summary>
        ///     Salva os dados de CfgServico em um arquivo XML
        /// </summary>
        /// <param name="arquivo">Arquivo XML onde será salvo os dados</param>
        public void SalvarParaAqruivo(string arquivo)
        {
            var camposEmBranco = CfgServico.ObterPropriedadesEmBranco();

            var propinfo = _cfgServico.ObterPropriedadeInfo(c => c.DiretorioSalvarXml);
            camposEmBranco.Remove(propinfo.Name);

            if (camposEmBranco.Count > 0)
                throw new Exception("Informe os dados abaixo antes de salvar as Configurações:" + Environment.NewLine + string.Join(", ", camposEmBranco.ToArray()));

            var dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }
            FuncoesXml.ClasseParaArquivoXml(this, arquivo);
        }
    }
}
