using System;
using System.IO;
using CTeDLL.Classes.Informacoes.Emitente;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Flags;

namespace CTeDLL
{
    public class ConfiguracaoApp
    {
        // todo rever ela toda, pois o mesmo se encontra no app teste e em nem um lugar mais
        /*private ConfiguracaoServico _cfgServico;

        public ConfiguracaoApp()
        {
            CfgServico = ConfiguracaoServico.Instancia;
            CfgServico.tpAmb = TipoAmbiente.Homologacao;
            CfgServico.tpEmis = TipoEmissao.teNormal;
            Emitente = new emit { CRT = CRT.SimplesNacional };
            EnderecoEmitente = new enderEmit();
            ConfiguracaoDacte = new ConfiguracaoDacte();
            //ConfiguracaoDanfeNfce = new ConfiguracaoDanfeNfce(NfceDetalheVendaNormal.UmaLinha, NfceDetalheVendaContigencia.UmaLinha, "", "");
            ConfiguracaoEmail = new ConfiguracaoEmail();
        }

        public ConfiguracaoServico CfgServico
        {
            get
            {
                Funcoes.CopiarPropriedades(_cfgServico, ConfiguracaoServico.Instancia);
                return _cfgServico;
            }
            set
            {
                _cfgServico = value;
                Funcoes.CopiarPropriedades(value, ConfiguracaoServico.Instancia);
            }
        }

        public emit Emitente { get; set; }
        public enderEmit EnderecoEmitente { get; set; }
        public ConfiguracaoDacte ConfiguracaoDacte { get; set; }
        //public ConfiguracaoDanfeNfce ConfiguracaoDanfeNfce { get; set; }
        public ConfiguracaoEmail ConfiguracaoEmail { get; set; }

        /// <summary>
        ///     Salva os dados de CfgServico em um arquivo XML
        /// </summary>
        /// <param name="arquivo">Arquivo XML onde será salvo os dados</param>
        public void Salvar(string arquivo)
        {

            _cfgServico.VersaoCteAutorizacao = VersaoServico.ve300;
            _cfgServico.VersaoCteConsultaCadastro = VersaoServico.ve200;
            _cfgServico.VersaoCteConsultaDest = VersaoServico.ve300;
            _cfgServico.VersaoCteConsultaProtocolo = VersaoServico.ve200;
            _cfgServico.VersaoCteDistribuicaoDFe = VersaoServico.ve100;
            _cfgServico.VersaoCteDownloadNF = VersaoServico.ve300;
            _cfgServico.VersaoCteInutilizacao = VersaoServico.ve200;
            _cfgServico.VersaoCteRecepcao = VersaoServico.ve200;
            _cfgServico.VersaoCteRetAutorizacao = VersaoServico.ve300;
            _cfgServico.VersaoCteRetRecepcao = VersaoServico.ve200;
            _cfgServico.VersaoCteStatusServico = VersaoServico.ve200;
            _cfgServico.VersaoRecepcaoEvento = VersaoServico.ve100;
            _cfgServico.VersaoRecepcaoEventoCceCancelamento = VersaoServico.ve100;
            _cfgServico.VersaoRecepcaoEventoEpec = VersaoServico.ve100;
            _cfgServico.VersaoRecepcaoEventoManifestacaoDestinatario = VersaoServico.ve100;
            
            var camposEmBranco = Funcoes.ObterPropriedadesEmBranco(CfgServico);

            var propinfo = Funcoes.ObterPropriedadeInfo(_cfgServico, c => c.DiretorioSalvarXml);
            camposEmBranco.Remove(propinfo.Name);

            if (camposEmBranco.Count > 0)
                throw new Exception("Informe os dados abaixo antes de salvar as Configurações:" + Environment.NewLine + string.Join(", ", camposEmBranco.ToArray()));

            var dir = Path.GetDirectoryName(arquivo);
            if (dir != null && !Directory.Exists(dir))
            {
                throw new DirectoryNotFoundException("Diretório " + dir + " não encontrado!");
            }

            FuncoesXml.ClasseParaArquivoXml(this, arquivo);
        }*/
    }
}