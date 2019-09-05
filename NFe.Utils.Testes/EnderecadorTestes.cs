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
    }

    public class EstadosData : TheoryData<Estado, TipoAmbiente, ServicoNFe>
    {
        public EstadosData()
        {
            var todosOsEstados = Enum.GetValues(typeof(Estado)).Cast<Estado>().ToList();
            todosOsEstados.Remove(Estado.AN);
            todosOsEstados.Remove(Estado.EX);
            todosOsEstados.Remove(Estado.PA);

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
