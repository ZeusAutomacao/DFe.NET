using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NFe.Utils.CpfCnpj;

namespace NFe.Utils.CpfCnpj.Testes
{
    /// <summary>
    ///     Fonte de consulta deterministica para os testes de unidade (sem rede).
    /// </summary>
    internal class FakePessoaLookup : IPessoaLookup
    {
        private readonly PessoaResultado _pessoa;
        private readonly IList<InscricaoEstadual> _inscricoes;

        public int ConsultasPessoa { get; private set; }
        public int ConsultasInscricao { get; private set; }

        public FakePessoaLookup(PessoaResultado pessoa, IList<InscricaoEstadual> inscricoes = null)
        {
            _pessoa = pessoa;
            _inscricoes = inscricoes ?? new List<InscricaoEstadual>();
        }

        public Task<PessoaResultado> ConsultarAsync(string documento, CancellationToken cancellationToken = default(CancellationToken))
        {
            ConsultasPessoa++;
            return Task.FromResult(_pessoa);
        }

        public Task<IList<InscricaoEstadual>> ConsultarInscricoesEstaduaisAsync(string cnpj, CancellationToken cancellationToken = default(CancellationToken))
        {
            ConsultasInscricao++;
            return Task.FromResult(_inscricoes);
        }
    }
}
