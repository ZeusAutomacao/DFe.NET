using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz.Servidores
{
    public abstract class SefazSVRS : IServidorSefaz
    {
        public SefazSVRS()
        {
            EventoCceCancelamento = VersaoServico.ve100;
            NfeInutilizacao = VersaoServico.ve310;
            NfeConsultaProtocolo = VersaoServico.ve310;
            NfeStatusServico = VersaoServico.ve310;
            NfeAutorizacao = VersaoServico.ve310;
            NfeRetornoAutorizacao = VersaoServico.ve310;
            NfeConsultaDest = VersaoServico.ve310;
            NFeDistribuicaoDFe = VersaoServico.ve100;
            ManifestacaoDestinatario = VersaoServico.ve100;
            NfeRecepcao = VersaoServico.ve310;
            NfeRetornoRecepcao = VersaoServico.ve310;
            NfeConsultaCadastro = VersaoServico.ve310;
            VersaoRecepcaoEventoEpec = VersaoServico.ve100;
            VersaoNfceAministracaoCSC = VersaoServico.ve100;
        }

        public abstract Estado EstadoReferente { get; set; }
        public virtual VersaoServico EventoCceCancelamento { get; set; }
        public virtual VersaoServico NfeInutilizacao { get; set; }
        public virtual VersaoServico NfeConsultaProtocolo { get; set; }
        public virtual VersaoServico NfeStatusServico { get; set; }
        public virtual VersaoServico NfeAutorizacao { get; set; }
        public virtual VersaoServico NfeRetornoAutorizacao { get; set; }
        public virtual VersaoServico NfeConsultaDest { get; set; }
        public VersaoServico NFeDistribuicaoDFe { get; set; }
        public VersaoServico ManifestacaoDestinatario { get; set; }
        public VersaoServico VersaoRecepcaoEventoEpec { get; set; }
        public VersaoServico VersaoNfceAministracaoCSC { get; set; }
        public virtual VersaoServico NfeRecepcao { get; set; }
        public virtual VersaoServico NfeRetornoRecepcao { get; set; }
        public virtual VersaoServico NfeConsultaCadastro { get; set; }
    }
}