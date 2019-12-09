using System;

namespace DFeFacadeBase
{
    public class ConsultaStatusRetorno : IConsultaStatusRetorno
    {
        public ConsultaStatusRetorno(string versao, DFeAmbiente ambiente, string versaoAplicacao, string status, string motivo, DFeEstado uf, DateTimeOffset dataRecebimento, DateTimeOffset? dataRetorno, string observacao, string tempoMedio, string xmlEnvio, string xmlRetorno, object objetoOriginal)
        {
            Versao = versao;
            Ambiente = ambiente;
            VersaoAplicacao = versaoAplicacao;
            Status = status;
            Motivo = motivo;
            Uf = uf;
            DataRecebimento = dataRecebimento;
            DataRetorno = dataRetorno;
            Observacao = observacao;
            TempoMedio = tempoMedio;
            XmlEnvio = xmlEnvio;
            XmlRetorno = xmlRetorno;
            ObjetoOriginal = objetoOriginal;
        }

        public string Versao { get; }
        public DFeAmbiente Ambiente { get; }
        public string VersaoAplicacao { get; }
        public string Status { get; }
        public string Motivo { get; }
        public DFeEstado Uf { get; }
        public DateTimeOffset DataRecebimento { get; }
        public DateTimeOffset? DataRetorno { get; }
        public string Observacao { get; }
        public string TempoMedio { get; }
        public string XmlEnvio { get; }
        public string XmlRetorno { get; }
        public object ObjetoOriginal { get; }
    }
}