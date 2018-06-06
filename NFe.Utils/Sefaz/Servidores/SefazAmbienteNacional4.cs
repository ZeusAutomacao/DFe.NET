using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz.Servidores
{
    public abstract class SefazAmbienteNacional4 : IServidorSefaz
    {
        public SefazAmbienteNacional4()
        {
            EventoCceCancelamento = VersaoServico.ve400;
            NfeConsultaDest = VersaoServico.ve100;
            NFeDistribuicaoDFe = VersaoServico.ve100;
            ManifestacaoDestinatario = VersaoServico.ve400;
            VersaoRecepcaoEventoEpec = VersaoServico.ve100;
            VersaoNfceAministracaoCSC = VersaoServico.ve100;
        }

        public abstract Estado EstadoReferente { get; set; }
        public virtual VersaoServico EventoCceCancelamento { get; set; }
        public abstract VersaoServico NfeRecepcao { get; set; }
        public abstract VersaoServico NfeRetornoRecepcao { get; set; }
        public abstract VersaoServico NfeConsultaCadastro { get; set; }
        public abstract VersaoServico NfeInutilizacao { get; set; }
        public abstract VersaoServico NfeConsultaProtocolo { get; set; }
        public abstract VersaoServico NfeStatusServico { get; set; }
        public abstract VersaoServico NfeAutorizacao { get; set; }
        public abstract VersaoServico NfeRetornoAutorizacao { get; set; }
        public virtual VersaoServico NfeConsultaDest { get; set; }
        public VersaoServico NFeDistribuicaoDFe { get; set; }
        public VersaoServico ManifestacaoDestinatario { get; set; }
        public VersaoServico VersaoRecepcaoEventoEpec { get; set; }
        public VersaoServico VersaoNfceAministracaoCSC { get; set; }
    }
}