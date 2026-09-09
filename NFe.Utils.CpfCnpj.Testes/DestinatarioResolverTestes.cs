using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Utils.CpfCnpj;
using Xunit;

namespace NFe.Utils.CpfCnpj.Testes
{
    public class DestinatarioResolverTestes
    {
        private static PessoaResultado PessoaFisica()
        {
            return new PessoaResultado
            {
                Encontrado = true,
                Documento = "11144477735",
                Tipo = TipoPessoa.Fisica,
                Nome = "Joao Consumidor",
                Endereco = new EnderecoResultado
                {
                    Logradouro = "Rua das Flores",
                    Numero = "100",
                    Bairro = "Centro",
                    Cep = "31000000",
                    Municipio = "Belo Horizonte",
                    Uf = "MG",
                    CodigoMunicipioIbge = 3106200
                }
            };
        }

        private static PessoaResultado PessoaJuridica()
        {
            return new PessoaResultado
            {
                Encontrado = true,
                Documento = "06990590000123",
                Tipo = TipoPessoa.Juridica,
                Nome = "Empresa Exemplo LTDA",
                NomeFantasia = "Exemplo",
                OptanteSimplesNacional = false,
                Endereco = new EnderecoResultado
                {
                    Logradouro = "Avenida Central",
                    Numero = "1",
                    Complemento = "Sala 1",
                    Bairro = "Centro",
                    Cep = "39400000",
                    Municipio = "Montes Claros",
                    Uf = "MG",
                    CodigoMunicipioIbge = 3143302
                }
            };
        }

        [Fact]
        public async Task Cpf_preenche_documento_nome_endereco_e_nao_contribuinte()
        {
            var lookup = new FakePessoaLookup(PessoaFisica());
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "111.444.777-35");

            Assert.Equal("11144477735", destinatario.CPF);
            Assert.True(string.IsNullOrEmpty(destinatario.CNPJ));
            Assert.Equal("Joao Consumidor", destinatario.xNome);
            Assert.NotNull(destinatario.enderDest);
            Assert.Equal("Rua das Flores", destinatario.enderDest.xLgr);
            Assert.Equal("MG", destinatario.enderDest.UF);
            Assert.Equal(3106200, destinatario.enderDest.cMun);
            Assert.Equal("31000000", destinatario.enderDest.CEP);
            Assert.Equal(indIEDest.NaoContribuinte, destinatario.indIEDest);
            Assert.Equal(1058, destinatario.enderDest.cPais);
        }

        [Fact]
        public async Task Cnpj_sem_resolver_ie_marca_nao_contribuinte()
        {
            var lookup = new FakePessoaLookup(PessoaJuridica());
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "06.990.590/0001-23");

            Assert.Equal("06990590000123", destinatario.CNPJ);
            Assert.True(string.IsNullOrEmpty(destinatario.CPF));
            Assert.Equal("Empresa Exemplo LTDA", destinatario.xNome);
            Assert.Equal("Montes Claros", destinatario.enderDest.xMun);
            Assert.Equal(3143302, destinatario.enderDest.cMun);
            Assert.Equal(indIEDest.NaoContribuinte, destinatario.indIEDest);
            Assert.Equal(0, lookup.ConsultasInscricao);
        }

        [Fact]
        public async Task Cnpj_com_ie_ativa_na_uf_marca_contribuinte_icms()
        {
            var inscricoes = new List<InscricaoEstadual>
            {
                new InscricaoEstadual { Numero = "0010101010101", Ativa = true, Uf = "MG", CodigoEstadoIbge = 31 }
            };
            var lookup = new FakePessoaLookup(PessoaJuridica(), inscricoes);
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "06990590000123",
                new DestinatarioResolverOpcoes { ResolverInscricaoEstadual = true });

            Assert.Equal(indIEDest.ContribuinteICMS, destinatario.indIEDest);
            Assert.Equal("0010101010101", destinatario.IE);
            Assert.Equal(1, lookup.ConsultasInscricao);
        }

        [Fact]
        public async Task Cnpj_com_ie_inativa_ou_outra_uf_fica_nao_contribuinte_sem_ie()
        {
            var inscricoes = new List<InscricaoEstadual>
            {
                new InscricaoEstadual { Numero = "111", Ativa = false, Uf = "MG", CodigoEstadoIbge = 31 },
                new InscricaoEstadual { Numero = "222", Ativa = true, Uf = "SP", CodigoEstadoIbge = 35 }
            };
            var lookup = new FakePessoaLookup(PessoaJuridica(), inscricoes);
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "06990590000123",
                new DestinatarioResolverOpcoes { ResolverInscricaoEstadual = true });

            Assert.Equal(indIEDest.NaoContribuinte, destinatario.indIEDest);
            Assert.True(string.IsNullOrEmpty(destinatario.IE));
        }

        [Fact]
        public async Task Nunca_marca_isento()
        {
            var inscricoes = new List<InscricaoEstadual>
            {
                new InscricaoEstadual { Numero = "333", Ativa = false, Uf = "MG", CodigoEstadoIbge = 31 }
            };
            var lookup = new FakePessoaLookup(PessoaJuridica(), inscricoes);
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "06990590000123",
                new DestinatarioResolverOpcoes { ResolverInscricaoEstadual = true });

            Assert.NotEqual(indIEDest.Isento, destinatario.indIEDest);
        }

        [Fact]
        public async Task Nao_sobrescreve_campos_ja_informados()
        {
            var lookup = new FakePessoaLookup(PessoaJuridica());
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400)
            {
                xNome = "Nome Original",
                indIEDest = indIEDest.ContribuinteICMS,
                enderDest = new enderDest { xLgr = "Logradouro Original", UF = "SP" }
            };

            await resolver.PreencherAsync(destinatario, "06990590000123");

            Assert.Equal("Nome Original", destinatario.xNome);
            Assert.Equal("Logradouro Original", destinatario.enderDest.xLgr);
            Assert.Equal("SP", destinatario.enderDest.UF);
            Assert.Equal(indIEDest.ContribuinteICMS, destinatario.indIEDest);
            // campos vazios sao complementados
            Assert.Equal("Centro", destinatario.enderDest.xBairro);
        }

        [Fact]
        public async Task Nao_define_documento_quando_ja_ha_identidade()
        {
            var lookup = new FakePessoaLookup(PessoaFisica());
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400) { CNPJ = "06990590000123" };

            await resolver.PreencherAsync(destinatario, "11144477735");

            Assert.Equal("06990590000123", destinatario.CNPJ);
            Assert.True(string.IsNullOrEmpty(destinatario.CPF));
        }

        [Fact]
        public async Task Cep_invalido_e_ignorado_sem_excecao()
        {
            var pessoa = PessoaFisica();
            pessoa.Endereco.Cep = "0000111"; // 7 digitos, invalido para NF-e
            var lookup = new FakePessoaLookup(pessoa);
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "11144477735");

            Assert.True(string.IsNullOrEmpty(destinatario.enderDest.CEP));
            Assert.Equal("Rua das Flores", destinatario.enderDest.xLgr);
        }

        [Fact]
        public async Task Documento_nao_encontrado_nao_altera_destinatario()
        {
            var lookup = new FakePessoaLookup(new PessoaResultado { Encontrado = false, Tipo = TipoPessoa.Fisica });
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "11144477735");

            Assert.True(string.IsNullOrEmpty(destinatario.CPF));
            Assert.True(string.IsNullOrEmpty(destinatario.xNome));
            Assert.Null(destinatario.indIEDest);
        }

        [Fact]
        public void DeterminarTipo_rejeita_tamanho_invalido()
        {
            Assert.Equal(TipoPessoa.Fisica, DocumentoUtil.DeterminarTipo("11144477735"));
            Assert.Equal(TipoPessoa.Juridica, DocumentoUtil.DeterminarTipo("06990590000123"));
            Assert.Throws<ArgumentException>(() => DocumentoUtil.DeterminarTipo("123"));
        }

        [Fact]
        public void Normalizar_preserva_cnpj_alfanumerico()
        {
            // CNPJ alfanumerico: 12 posicoes alfanumericas + 2 digitos verificadores.
            var normalizado = DocumentoUtil.Normalizar("12.abc.345/01de-35");
            Assert.Equal("12ABC34501DE35", normalizado);
            Assert.Equal(14, normalizado.Length);
            Assert.Equal(TipoPessoa.Juridica, DocumentoUtil.DeterminarTipo(normalizado));
        }

        [Fact]
        public void Normalizar_cpf_mantem_apenas_digitos()
        {
            Assert.Equal("11144477735", DocumentoUtil.Normalizar("111.444.777-35"));
        }

        [Fact]
        public async Task Cnpj_alfanumerico_e_atribuido_sem_perder_letras()
        {
            var pessoa = new PessoaResultado
            {
                Encontrado = true,
                Tipo = TipoPessoa.Juridica,
                Nome = "Empresa Alfanumerica LTDA"
            };
            var lookup = new FakePessoaLookup(pessoa);
            var resolver = new DestinatarioResolver(lookup);
            var destinatario = new dest(VersaoServico.Versao400);

            await resolver.PreencherAsync(destinatario, "12.ABC.345/01DE-35");

            Assert.Equal("12ABC34501DE35", destinatario.CNPJ);
            Assert.True(string.IsNullOrEmpty(destinatario.CPF));
        }

        [Fact]
        public void UfExtensoes_converte_sigla_em_estado()
        {
            Assert.Equal(Estado.MG, UfExtensoes.ParaEstado("mg"));
            Assert.Equal(Estado.SP, UfExtensoes.ParaEstado("SP"));
            Estado ignorado;
            Assert.False(UfExtensoes.TentarParaEstado("XX", out ignorado));
        }
    }
}
