using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazMG : SefazAmbienteNacional
    {
        public SefazMG() : base()
        {
            EstadoReferente = Estado.MG;
            EventoCceCancelamento = VersaoServico.ve100;
            NfeConsultaCadastro = VersaoServico.ve200;
            NfeRecepcao = VersaoServico.ve310;
            NfeRetornoRecepcao = VersaoServico.ve310;
            NfeInutilizacao = VersaoServico.ve310;
            NfeConsultaProtocolo = VersaoServico.ve310;
            NfeStatusServico = VersaoServico.ve310;
            NfeAutorizacao = VersaoServico.ve310;
            NfeRetornoAutorizacao = VersaoServico.ve310;
        }

        public override Estado EstadoReferente { get; set; }
        public override VersaoServico EventoCceCancelamento { get; set; }
        public override VersaoServico NfeConsultaCadastro { get; set; }
        public override VersaoServico NfeRecepcao { get; set; }
        public override VersaoServico NfeRetornoRecepcao { get; set; }
        public override VersaoServico NfeInutilizacao { get; set; }
        public override VersaoServico NfeConsultaProtocolo { get; set; }
        public override VersaoServico NfeStatusServico { get; set; }
        public override VersaoServico NfeAutorizacao { get; set; }
        public override VersaoServico NfeRetornoAutorizacao { get; set; }
    }
}