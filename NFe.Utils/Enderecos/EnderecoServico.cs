using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Enderecos
{
    /// <summary>
    ///     Classe com estrutura para armazenamento dos dados dos webservices.
    /// </summary>
    public class EnderecoServico
    {
        public EnderecoServico(ServicoNFe servicoNFe, VersaoServico versaoServico, TipoAmbiente tipoAmbiente, TipoEmissao tipoEmissao, Estado estado, ModeloDocumento modeloDocumento, string url)
        {
            ServicoNFe = servicoNFe;
            VersaoServico = versaoServico;
            TipoAmbiente = tipoAmbiente;
            TipoEmissao = tipoEmissao;
            Estado = estado;
            Url = url;
            ModeloDocumento = modeloDocumento;
        }

        public ServicoNFe ServicoNFe { get; private set; }
        public VersaoServico VersaoServico { get; private set; }
        public TipoAmbiente TipoAmbiente { get; private set; }
        public TipoEmissao TipoEmissao { get; private set; }
        public Estado Estado { get; private set; }
        public ModeloDocumento ModeloDocumento { get; private set; }
        public string Url { get; private set; }
    }
}