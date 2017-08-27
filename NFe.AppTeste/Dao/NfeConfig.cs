using System.Net;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.NFe.Classes.Informacoes.Identificacao.Tipos;
using DFe.DocumentosEletronicos.NFe.Configuracao;
using DFe.DocumentosEletronicos.NFe.Flags;

namespace NFe.AppTeste.Dao
{
    public class NfeConfig : NFeBaseConfig
    {
        public override TipoAmbiente TipoAmbiente { get; set; }
        public override VersaoServico VersaoServico { get; set; }
        public override Estado EstadoUf { get; set; }
        public override string CnpjEmitente { get; set; }
        public override TipoEmissao TipoEmissao { get; set; }
        public override ModeloDocumento ModeloDocumento { get; set; }
        public override ServicoNFe ServicoNFe { get; set; }
        public override VersaoServico VersaoRecepcaoEventoCceCancelamento { get; set; }
        public override VersaoServico VersaoRecepcaoEventoEpec { get; set; }
        public override VersaoServico VersaoNfeRecepcao { get; set; }
        public override VersaoServico VersaoNfeRetRecepcao { get; set; }
        public override VersaoServico VersaoNfeConsultaCadastro { get; set; }
        public override VersaoServico VersaoNfeInutilizacao { get; set; }
        public override VersaoServico VersaoNfeConsultaProtocolo { get; set; }
        public override VersaoServico VersaoNfeStatusServico { get; set; }
        public override VersaoServico VersaoNFeAutorizacao { get; set; }
        public override VersaoServico VersaoNFeRetAutorizacao { get; set; }
        public override VersaoServico VersaoNFeDistribuicaoDFe { get; set; }
        public override VersaoServico VersaoNfeConsultaDest { get; set; }
        public override VersaoServico VersaoNfeDownloadNF { get; set; }
        public override VersaoServico VersaoNfceAministracaoCSC { get; set; }
        public override SecurityProtocolType ProtocoloDeSeguranca { get; set; }
    }
}