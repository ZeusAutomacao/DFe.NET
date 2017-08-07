using DFe.Configuracao;
using DFe.DocumentosEletronicos.Ext;
using DFe.DocumentosEletronicos.NFe.Classes.Servicos.Status;

namespace DFe.DocumentosEletronicos.NFe.Servicos.Factory
{
    public static class ClassesFactory
    {
        public static consStatServ CriaConsStatServ(DFeConfig config)
        {
            var consStatServ = new consStatServ
            {
                versao = config.VersaoServico.GetVersaoString(),
                tpAmb = config.TipoAmbiente,
                cUF = config.EstadoUf
            };

            return consStatServ;
        }
    }
}