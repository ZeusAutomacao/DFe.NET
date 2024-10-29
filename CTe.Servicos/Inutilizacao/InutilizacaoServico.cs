using System;
using System.Threading.Tasks;
using CTe.Classes;
using CTe.Classes.Servicos.Inutilizacao;
using CTe.Servicos.Factory;
using CTe.Utils.Extencoes;
using CTe.Utils.Inutilizacao;
using DFe.Classes.Flags;

namespace CTe.Servicos.Inutilizacao
{
    public class ConfigInutiliza
    { 
        public ConfigInutiliza(string cnpj, short serie, long numeroInicial, long numeroFinal, int ano,
            string justificativa, ModeloDocumento modeloDocumento = ModeloDocumento.CTe)
        {
            Cnpj = cnpj;
            Serie = serie;
            NumeroInicial = numeroInicial;
            NumeroFinal = numeroFinal;
            Ano = ano;
            Justificativa = justificativa;
            ModeloDocumento = modeloDocumento;
        }

        public int Ano { get; private set; }
        public string Cnpj { get; private set; }
        public short Serie { get; private set; }
        public long NumeroInicial { get; private set; }
        public long NumeroFinal { get; private set; }
        public string Justificativa { get; private set; }
        public ModeloDocumento ModeloDocumento { get; private set; }
    }

    public class InutilizacaoServico
    {
        private readonly ConfigInutiliza _configInutiliza;

        public InutilizacaoServico(ConfigInutiliza configInutiliza)
        {
            Validacoes(configInutiliza);

            _configInutiliza = configInutiliza;
        }

        public retInutCTe Inutilizar(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var inutCte = ClassesFactory.CriaInutCTe(_configInutiliza, configServico);
            inutCte.Assinar(configServico);
            inutCte.ValidarSchema(configServico);
            inutCte.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlCteInutilizacao(configServico);
            var retornoXml = webService.cteInutilizacaoCT(inutCte.CriaRequestWs());

            var retorno = retInutCTe.LoadXml(retornoXml.OuterXml, inutCte);
            retorno.SalvarXmlEmDisco(inutCte.infInut.Id.Substring(2), configServico);

            return retorno;
        }

        public async Task<retInutCTe> InutilizarAsync(ConfiguracaoServico configuracaoServico = null)
        {
            var configServico = configuracaoServico ?? ConfiguracaoServico.Instancia;

            var inutCte = ClassesFactory.CriaInutCTe(_configInutiliza, configServico);
            inutCte.Assinar(configServico);
            inutCte.ValidarSchema(configServico);
            inutCte.SalvarXmlEmDisco(configServico);

            var webService = WsdlFactory.CriaWsdlCteInutilizacao(configServico);
            var retornoXml = await webService.cteInutilizacaoCTAsync(inutCte.CriaRequestWs());

            var retorno = retInutCTe.LoadXml(retornoXml.OuterXml, inutCte);
            retorno.SalvarXmlEmDisco(inutCte.infInut.Id.Substring(2), configServico);

            return retorno;
        }

        private static void Validacoes(ConfigInutiliza configInutiliza)
        {
            if (configInutiliza == null) throw new ArgumentNullException("Preciso de uma configuração de inutilização");

            if (string.IsNullOrEmpty(configInutiliza.Cnpj))
                throw new InvalidOperationException("Para inutilizar a númeração eu preciso do cnpj do emitente");

            if (configInutiliza.Serie <= 0)
                throw new InvalidOperationException("Preciso que a série seja maior que 0");

            if (configInutiliza.NumeroInicial <= 0)
                throw new InvalidOperationException("Preciso que o número inicial seja maior que 0");

            if (configInutiliza.NumeroFinal <= 0)
                throw new InvalidOperationException("Preciso que o número final seja maior que 0");

            if (configInutiliza.NumeroInicial > configInutiliza.NumeroFinal)
                throw new InvalidOperationException("Preciso que o número inicial seja maior que o número final");
        }
    }
}