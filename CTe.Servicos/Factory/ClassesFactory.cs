using CTe.Classes.Ext;
using CTeDLL.Classes.Servicos.Consulta;
using CTeDLL.Classes.Servicos.Status;

namespace CTeDLL.Servicos.Factory
{
    public class ClassesFactory
    {
        public static consStatServCte CriaConsStatServCte()
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consStatServCte
            {
                versao = configuracaoServico.VersaoLayout,
                tpAmb = configuracaoServico.tpAmb
            };
        }

        public static consSitCTe CriarconsSitCTe(string chave)
        {
            var configuracaoServico = ConfiguracaoServico.Instancia;

            return new consSitCTe
            {
                tpAmb = configuracaoServico.tpAmb,
                versao = configuracaoServico.VersaoLayout,
                chCTe = chave
            };
        }
    }
}