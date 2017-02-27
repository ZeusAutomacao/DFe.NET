using CTe.Classes.Ext;
using CTeDLL.Classes.Servicos.Status;

namespace CTeDLL.Servicos.Factory
{
    public class ClassesFactory
    {
        public static consStatServCte CriaConsStatServCte()
        {
            var servicoInstancia = ConfiguracaoServico.Instancia;

            return new consStatServCte
            {
                versao = servicoInstancia.VersaoLayout,
                tpAmb = servicoInstancia.tpAmb
            };
        }
    }
}