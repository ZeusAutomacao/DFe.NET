using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Retorno.MDFeEvento;
using MDFe.Servicos.Factory;
using MDFe.Utils.Configuracoes;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;

namespace MDFe.Servicos.EventosMDFe
{
    public class EventoInclusaoCondutor
    {
        public MDFeRetEventoMDFe MDFeEventoIncluirCondutor(MDFeEletronico mdfe, byte sequenciaEvento, string nome, string cpf, MDFeConfiguracao cfgMdfe = null)
        {
            var config = cfgMdfe ?? MDFeConfiguracao.Instancia;
            var incluirCodutor = ClassesFactory.CriaEvIncCondutorMDFe(nome, cpf);

            var retorno = new ServicoController().Executar(mdfe, sequenciaEvento, incluirCodutor, MDFeTipoEvento.InclusaoDeCondutor, config);

            return retorno;
        }
    }
}