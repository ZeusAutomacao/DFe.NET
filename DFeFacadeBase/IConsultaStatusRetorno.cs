using System;

namespace DFeFacadeBase
{
    public interface IConsultaStatusRetorno
    {
        string Versao { get; }
        DFeAmbiente Ambiente { get; }
        string VersaoAplicacao { get; }
        string Status { get; }
        string Motivo { get; }
        DFeEstado Uf { get; }
        DateTimeOffset DataRecebimento { get; }
        DateTimeOffset DataRetorno { get; }
        string Observacao { get; }
        string TempoMedio { get; }

        string XmlEnvio { get; }
        string XmlRetorno { get; }
    }
}