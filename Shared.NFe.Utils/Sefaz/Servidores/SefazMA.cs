using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Sefaz.Servidores
{
    public class SefazMA : SefazSVAN
    {
        public SefazMA()
        {
            EstadoReferente = Estado.MA;
            NfeConsultaCadastro = VersaoServico.ve200;
        }

        public override Estado EstadoReferente { get; set; }
        public override VersaoServico NfeConsultaCadastro { get; set; }
    }
}