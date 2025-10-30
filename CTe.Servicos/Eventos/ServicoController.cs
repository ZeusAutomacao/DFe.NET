using System.Threading.Tasks;
using System.Xml;
using CTe.Classes;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Classes.Servicos.Tipos;
using CTe.Servicos.Eventos.Contratos;
using CTe.Servicos.Factory;
using CTe.Utils.CTe;
using CTe.Utils.Evento;
using CteEletronico = CTe.Classes.CTe;
using CteEletronicoOS = CTe.CTeOSClasses.CTeOS;

namespace CTe.Servicos.Eventos
{
    public class ServicoController : IServicoController
    {
        public retEventoCTe Executar(CteEletronico cte, int sequenciaEvento, EventoContainer container, CTeTipoEvento cTeTipoEvento, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            return Executar(cTeTipoEvento, sequenciaEvento, cte.Chave(), cte.infCte.emit.CNPJ, container, configServico);
        }

        public retEventoCTe Executar(CteEletronicoOS cte, int sequenciaEvento, EventoContainer container, CTeTipoEvento cTeTipoEvento, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            return Executar(cTeTipoEvento, sequenciaEvento, cte.Chave(), cte.InfCte.emit.CNPJ, container, configServico);
        }

        public async Task<retEventoCTe> ExecutarAsync(CteEletronico cte, int sequenciaEvento, EventoContainer container, CTeTipoEvento cTeTipoEvento, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            return await ExecutarAsync(cTeTipoEvento, sequenciaEvento, cte.Chave(), cte.infCte.emit.CNPJ, container, configServico);
        }

        public retEventoCTe Executar(CTeTipoEvento cTeTipoEvento, int sequenciaEvento, string chave, string cnpj, EventoContainer container, ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
            var evento = FactoryEvento.CriaEvento(cTeTipoEvento, sequenciaEvento, chave, cnpj, container, configServico);
            evento.Assina(configServico);

            if (configuracaoServico.IsValidaSchemas)
                evento.ValidarSchema(configServico);

            evento.SalvarXmlEmDisco(configServico);

            XmlNode retornoXml = null;

            if (evento.versao == versao.ve200 || evento.versao == versao.ve300)
            {
                var webService = WsdlFactory.CriaWsdlCteEvento(configServico);
                retornoXml = webService.cteRecepcaoEvento(evento.CriaXmlRequestWs());
            }

            if (evento.versao == versao.ve400)
            {
                var webService = WsdlFactory.CriaWsdlCteEventoV4(configServico);
                retornoXml = webService.cteRecepcaoEvento(evento.CriaXmlRequestWs());
            }

            var retorno = retEventoCTe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(configServico);

            return retorno;
        }

        public async Task<retEventoCTe> ExecutarAsync(CTeTipoEvento cTeTipoEvento,
            int sequenciaEvento,
            string chave, string
            cnpj, EventoContainer container,
            ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var evento = FactoryEvento.CriaEvento(cTeTipoEvento, sequenciaEvento, chave, cnpj, container, configServico);
            evento.Assina(configServico);

            if (configServico.IsValidaSchemas)
                evento.ValidarSchema(configServico);

            evento.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlCteEvento(configServico);
            var retornoXml = await webService.cteRecepcaoEventoAsync(evento.CriaXmlRequestWs());

            var retorno = retEventoCTe.LoadXml(retornoXml.OuterXml, evento);
            retorno.SalvarXmlEmDisco(configServico);

            return retorno;
        }
    }
}