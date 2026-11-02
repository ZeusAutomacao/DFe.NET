using System;
using System.Linq;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Enderecos;
using Xunit;

namespace NFe.Utils.Testes
{
    public class EnderecadorTestes
    {
        [Theory(DisplayName = "NFe -> Normal -> Autorização -> 4.0")]
        [ClassData(typeof(EstadosData))]
        public void DeveCadaUfTerServicoDeAutorizacaoParaNFe4(Estado uf, TipoAmbiente tipoAmbiente, ServicoNFe servicoAutorizacao)
        {
            var lista = Enderecador.ObterEnderecoServicosMaisRecentes(VersaoServico.Versao400, uf, tipoAmbiente, ModeloDocumento.NFe, TipoEmissao.teNormal);
            Assert.Equal(1, lista.Count(n => n.ServicoNFe == servicoAutorizacao));
        }

        [Theory(DisplayName = "NFe -> SVC-AN -> Autorização -> 4.0")]
        [ClassData(typeof(SvcAnData))]
        public void DeveUfSvcAnTerServicoDeAutorizacaoParaNFe4(Estado uf, TipoAmbiente tipoAmbiente, ServicoNFe servicoAutorizacao)
        {
            var lista = Enderecador.ObterEnderecoServicosMaisRecentes(VersaoServico.Versao400, uf, tipoAmbiente, ModeloDocumento.NFe, TipoEmissao.teSVCAN);
            Assert.Equal(1, lista.Count(n => n.ServicoNFe == servicoAutorizacao));
        }

        [Theory(DisplayName = "NFe -> SVC-RS -> Autorização -> 4.0")]
        [ClassData(typeof(SvcRsData))]
        public void DeveUfSvcRsTerServicoDeAutorizacaoParaNFe4(Estado uf, TipoAmbiente tipoAmbiente, ServicoNFe servicoAutorizacao)
        {
            var lista = Enderecador.ObterEnderecoServicosMaisRecentes(VersaoServico.Versao400, uf, tipoAmbiente, ModeloDocumento.NFe, TipoEmissao.teSVCRS);
            Assert.Equal(1, lista.Count(n => n.ServicoNFe == servicoAutorizacao));
        }

        [Fact]
        public void DeveUrlAmEstarCorretaParaNFeAutorizacaoVersao4()
        {
            var url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, Estado.AM,
                TipoAmbiente.Producao, ModeloDocumento.NFe, TipoEmissao.teNormal);
            Assert.Equal("https://nfe.sefaz.am.gov.br/services2/services/NfeAutorizacao4", url);
        }

        [Fact]
        public void DeveUrlBaEstarCorretaParaNFeAutorizacaoVersao4()
        {
            var url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, Estado.BA,
                TipoAmbiente.Producao, ModeloDocumento.NFe, TipoEmissao.teNormal);
            Assert.Equal("https://nfe.sefaz.ba.gov.br/webservices/NFeAutorizacao4/NFeAutorizacao4.asmx", url);
        }

        [Fact]
        public void DeveUrlSpEstarCorretaParaNFeAutorizacaoVersao4()
        {
            var url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, VersaoServico.Versao400, Estado.SP,
                TipoAmbiente.Producao, ModeloDocumento.NFe, TipoEmissao.teNormal);
            Assert.Equal("https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx", url);
        }

        /// <summary>
        /// Dado emitente exclusivo sem IE, quando autoriza NF-e, entao usa a SVRS e preserva a UF.
        /// </summary>
        [Theory]
        [InlineData(Estado.SP, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx")]
        [InlineData(Estado.SP, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx")]
        [InlineData(Estado.BA, TipoAmbiente.Homologacao, "https://nfe-homologacao.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx")]
        [InlineData(Estado.MA, TipoAmbiente.Producao, "https://nfe.svrs.rs.gov.br/ws/NfeAutorizacao/NFeAutorizacao4.asmx")]
        public void DadoEmitenteExclusivoSemIe_QuandoAutorizaNFe_EntaoUsaSvrsSemAlterarUf(
            Estado uf, TipoAmbiente ambiente, string urlEsperada)
        {
            var configuracao = new ConfiguracaoServico
            {
                cUF = uf,
                tpAmb = ambiente,
                ModeloDocumento = ModeloDocumento.NFe,
                tpEmis = TipoEmissao.teNormal,
                VersaoNFeAutorizacao = VersaoServico.Versao400,
                ContribuinteExclusivoIbsCbsSemIe = true
            };

            Assert.Equal(urlEsperada, Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, configuracao));
            Assert.Equal(uf, configuracao.cUF);
        }

        /// <summary>
        /// Dado emitente exclusivo sem IE, quando consulta ou cancela, entao usa a SVRS.
        /// </summary>
        [Fact]
        public void DadoEmitenteExclusivoSemIe_QuandoConsultaOuCancela_EntaoUsaSvrs()
        {
            var configuracao = new ConfiguracaoServico
            {
                cUF = Estado.SP,
                tpAmb = TipoAmbiente.Homologacao,
                ModeloDocumento = ModeloDocumento.NFe,
                tpEmis = TipoEmissao.teNormal,
                VersaoNfeConsultaProtocolo = VersaoServico.Versao400,
                VersaoRecepcaoEventoCceCancelamento = VersaoServico.Versao400,
                ContribuinteExclusivoIbsCbsSemIe = true
            };

            Assert.Contains("svrs.rs.gov.br", Enderecador.ObterUrlServico(ServicoNFe.NfeConsultaProtocolo, configuracao));
            Assert.Contains("svrs.rs.gov.br", Enderecador.ObterUrlServico(ServicoNFe.RecepcaoEventoCancelmento, configuracao));
        }

        /// <summary>
        /// Dada NFC-e, quando a opcao exclusiva esta ativa, entao preserva a rota normal.
        /// </summary>
        [Fact]
        public void DadaNfce_QuandoOpcaoExclusivaEstaAtiva_EntaoPreservaRotaNormal()
        {
            var configuracao = new ConfiguracaoServico
            {
                cUF = Estado.SP,
                tpAmb = TipoAmbiente.Producao,
                ModeloDocumento = ModeloDocumento.NFCe,
                tpEmis = TipoEmissao.teNormal,
                VersaoNFeAutorizacao = VersaoServico.Versao400,
                ContribuinteExclusivoIbsCbsSemIe = true
            };

            var url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, configuracao);
            Assert.DoesNotContain("svrs.rs.gov.br", url);
        }

        /// <summary>
        /// Dado emitente comum, quando autoriza NF-e, entao preserva a rota de sua UF.
        /// </summary>
        [Fact]
        public void DadoEmitenteComum_QuandoAutorizaNFe_EntaoPreservaRotaDaUf()
        {
            var configuracao = new ConfiguracaoServico
            {
                cUF = Estado.SP,
                tpAmb = TipoAmbiente.Producao,
                ModeloDocumento = ModeloDocumento.NFe,
                tpEmis = TipoEmissao.teNormal,
                VersaoNFeAutorizacao = VersaoServico.Versao400
            };

            var url = Enderecador.ObterUrlServico(ServicoNFe.NFeAutorizacao, configuracao);
            Assert.Equal("https://nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx", url);
        }
    }

    public class EstadosData : TheoryData<Estado, TipoAmbiente, ServicoNFe>
    {
        public EstadosData()
        {
            var todosOsEstados = Enum.GetValues(typeof(Estado)).Cast<Estado>().ToList();
            todosOsEstados.Remove(Estado.AN);
            todosOsEstados.Remove(Estado.EX);
            todosOsEstados.Remove(Estado.PA);

            // UF autorizadora
            todosOsEstados.Remove(Estado.SVRS);

            var tiposAmbiente = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();
            var servicosAutorizacao = new[] {ServicoNFe.NFeAutorizacao, ServicoNFe.NFeRetAutorizacao};

            foreach (var tipoAmbiente in tiposAmbiente)
                foreach (var estado in todosOsEstados)
                    foreach (var servico in servicosAutorizacao)
                        Add(estado, tipoAmbiente, servico);

        }
    }

    public class SvcAnData : TheoryData<Estado, TipoAmbiente, ServicoNFe>
    {
        public SvcAnData()
        {
            var tiposAmbiente = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();
            var servicosAutorizacao = new[] { ServicoNFe.NFeAutorizacao, ServicoNFe.NFeRetAutorizacao };

            foreach (var tipoAmbiente in tiposAmbiente)
                foreach (var estado in Enderecador.EstadosQueUsamSvcAnParaNfe())
                    foreach (var servico in servicosAutorizacao)
                        Add(estado, tipoAmbiente, servico);
        }
    }

    public class SvcRsData : TheoryData<Estado, TipoAmbiente, ServicoNFe>
    {
        public SvcRsData()
        {
            var tiposAmbiente = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();
            var servicosAutorizacao = new[] { ServicoNFe.NFeAutorizacao, ServicoNFe.NFeRetAutorizacao };

            foreach (var tipoAmbiente in tiposAmbiente)
                foreach (var estado in Enderecador.EstadosQueUsamSvcRsParaNfe())
                    foreach (var servico in servicosAutorizacao)
                        Add(estado, tipoAmbiente, servico);
        }
    }
}
