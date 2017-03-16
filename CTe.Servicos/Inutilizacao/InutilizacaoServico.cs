using System;
using CTe.Utils.Extencoes;
using CTeDLL.Classes.Servicos.Inutilizacao;
using CTeDLL.Servicos.Factory;
using CTeDLL.Utils.Inutilizacao;
using DFe.Classes.Flags;
using DFe.Utils.Assinatura;

namespace CTeDLL.Servicos.Inutilizacao
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

        public int Ano { get; }
        public string Cnpj { get; }
        public short Serie { get; }
        public long NumeroInicial { get; }
        public long NumeroFinal { get; }
        public string Justificativa { get; }
        public ModeloDocumento ModeloDocumento { get; }
    }

    public class InutilizacaoServico
    {
        private readonly ConfigInutiliza _configInutiliza;

        public InutilizacaoServico(ConfigInutiliza configInutiliza)
        {
            Validacoes(configInutiliza);

            _configInutiliza = configInutiliza;
        }

        public retInutCTe Inutilizar()
        {
            var inutCte = ClassesFactory.CriaInutCTe(_configInutiliza);
            inutCte.Assinar();
            inutCte.ValidarShcema();
            inutCte.SalvarXmlEmDisco();

            var webService = WsdlFactory.CriaWsdlCteInutilizacao();
            var retornoXml = webService.cteInutilizacaoCT(inutCte.CriaRequestWs());

            var retorno = retInutCTe.LoadXml(retornoXml.OuterXml, inutCte);
            retorno.SalvarXmlEmDisco();

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