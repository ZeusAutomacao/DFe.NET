using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazAM4 : SefazAmbienteNacional4
    {
        public SefazAM4() : base()
        {
            EstadoReferente = Estado.AM;
            EventoCceCancelamento = VersaoServico.ve400;
            NfeRecepcao = VersaoServico.ve400;
            NfeRetornoRecepcao = VersaoServico.ve200;
            NfeConsultaCadastro = VersaoServico.ve400;
            NfeInutilizacao = VersaoServico.ve400;
            NfeConsultaProtocolo = VersaoServico.ve400;
            NfeStatusServico = VersaoServico.ve400;
            NfeAutorizacao = VersaoServico.ve400;
            NfeRetornoAutorizacao = VersaoServico.ve400;
            NfeConsultaDest = VersaoServico.ve310;
        }

        public override Estado EstadoReferente { get; set; }
        public override VersaoServico EventoCceCancelamento { get; set; }
        public override VersaoServico NfeRecepcao { get; set; }
        public override VersaoServico NfeRetornoRecepcao { get; set; }
        public override VersaoServico NfeConsultaCadastro { get; set; }
        public override VersaoServico NfeInutilizacao { get; set; }
        public override VersaoServico NfeConsultaProtocolo { get; set; }
        public override VersaoServico NfeStatusServico { get; set; }
        public override VersaoServico NfeAutorizacao { get; set; }
        public override VersaoServico NfeRetornoAutorizacao { get; set; }
        public override VersaoServico NfeConsultaDest { get; set; }
    }
}