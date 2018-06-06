using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazSP : SefazAmbienteNacional
    {
        public SefazSP() : base()
        {
            EstadoReferente = Estado.SP;
            EventoCceCancelamento = VersaoServico.ve100;
            NfeConsultaCadastro = VersaoServico.ve200;
            NfeInutilizacao = VersaoServico.ve310;
            NfeConsultaProtocolo = VersaoServico.ve310;
            NfeStatusServico = VersaoServico.ve310;
            NfeAutorizacao = VersaoServico.ve310;
            NfeRetornoAutorizacao = VersaoServico.ve310;
            NfeRecepcao = VersaoServico.ve100;
            NfeRetornoRecepcao = VersaoServico.ve100;
        }

        public override Estado EstadoReferente { get; set; }
        public override VersaoServico NfeRecepcao { get; set; }
        public override VersaoServico NfeRetornoRecepcao { get; set; }
        public override VersaoServico NfeConsultaCadastro { get; set; }
        public override VersaoServico NfeInutilizacao { get; set; }
        public override VersaoServico NfeConsultaProtocolo { get; set; }
        public override VersaoServico NfeStatusServico { get; set; }
        public override VersaoServico NfeAutorizacao { get; set; }
        public override VersaoServico NfeRetornoAutorizacao { get; set; }
    }
}