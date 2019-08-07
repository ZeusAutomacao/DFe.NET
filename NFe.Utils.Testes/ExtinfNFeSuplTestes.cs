using System;
using System.Linq;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes;
using NFe.Utils.InformacoesSuplementares;
using Xunit;

namespace NFe.Utils.Testes
{
    public class ExtinfNFeSuplTestes
    {
        [Theory]
        [ClassData(typeof(EstadosAmbientesData))]
        public void UrlQrCode2DeveTerminarComQueryPararametro(Estado estado, TipoAmbiente tipoAmbiente)
        {
           var infNFeSupl = new infNFeSupl();
            var url = infNFeSupl.ObterUrlQrCode2ComParametro(tipoAmbiente, estado, VersaoServico.Versao400);
            Assert.EndsWith("?p=", url);
        }
    }

    public class EstadosAmbientesData : TheoryData<Estado, TipoAmbiente>
    {
        public EstadosAmbientesData()
        {
            var todosOsEstados = Enum.GetValues(typeof(Estado)).Cast<Estado>().ToList();
            todosOsEstados.Remove(Estado.AN);
            todosOsEstados.Remove(Estado.EX);

            //Não divulgaram as urls para homologação e produção no ENCAT
            todosOsEstados.Remove(Estado.CE);
            todosOsEstados.Remove(Estado.ES);
            todosOsEstados.Remove(Estado.SC);
            todosOsEstados.Remove(Estado.MG);

            var tiposAmbiente = Enum.GetValues(typeof(TipoAmbiente)).Cast<TipoAmbiente>().ToList();

            foreach (var estado in todosOsEstados)
                foreach (var tipoAmbiente in tiposAmbiente)
                    Add(estado, tipoAmbiente);
        }
    }
}
