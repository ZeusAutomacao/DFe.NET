using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using DFe.Classes.Entidades;
using NFe.Utils;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace NFe.Testes.Gerais
{
    [TestClass]
    public class NFeTestes
    {

        /// <summary>
        /// Foram criados dois testes para análise do uso de multithread. 
        /// O teste baseia-se em verificar o uso da classe ConfiguracaoServico em Multithread, atribuindo um Estado e um Tempo de espera diferentes (simulando tempo de resposta). 
        /// Verifica-se no final a quantidade de Estados diferentes que foram atribuidos nas ConfiguracoesServico 
        /// 
        /// Primeiro método: A cada Task é criado uma nova instância
        /// Segundo método: Uso da forma estática
        /// 
        /// <result>
        /// Ao fazer uma soma de Estados distintos atribuídos, a forma estática não consegue usar diferentes Estados ao mesmo tempo (o que aconteceria com qualquer atributo).
        /// Já o primeiro teste, como cada instância é referente a sua thread, ela não possui influência nas demais.
        /// </result>
        /// </summary>

        [TestMethod]
        public void NFeConfiguracaoServico_WhenTryCallAsynchronously_ReturnsCorrectStateDistinctNumbers()
        {
            // Arrange
            var config1 = new { State = Estado.AC, SleepTime = TimeSpan.FromSeconds(10) };
            var config2 = new { State = Estado.AM, SleepTime = TimeSpan.FromSeconds(2) };
            var config3 = new { State = Estado.AP, SleepTime = TimeSpan.FromSeconds(0) };
            var config4 = new { State = Estado.SP, SleepTime = TimeSpan.FromSeconds(3) };

            var configurations = new[] { config1, config2, config3, config4 };

            var result = new List<Estado>();

            // Act
            foreach (var config in configurations)
            {
                Task.Run(() =>
                {
                    var service = new ConfiguracaoServico();
                    service.cUF = config.State;

                    Thread.Sleep(config.SleepTime);

                    result.Add(service.cUF);
                }
                );
            }

            // Assert
            Thread.Sleep(TimeSpan.FromSeconds(12));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Distinct().Count(), 4);
        }


        [TestMethod()]
        public void NFeConfiguracaoServico_WhenTryCallAsynchronously_ReturnsWrongStateDistinctNumbers()
        {

            // Arrange
            var config1 = new { State = Estado.AC, SleepTime = TimeSpan.FromSeconds(10) };
            var config2 = new { State = Estado.AM, SleepTime = TimeSpan.FromSeconds(2) };
            var config3 = new { State = Estado.AP, SleepTime = TimeSpan.FromSeconds(0) };
            var config4 = new { State = Estado.SP, SleepTime = TimeSpan.FromSeconds(3) };

            var configurations = new[] { config1, config2, config3, config4 };

            var result = new List<Estado>();

            // Act
            foreach (var config in configurations)
            {
                Task.Run(() =>
                {
                    ConfiguracaoServico.Instancia.cUF = config.State;

                    Thread.Sleep(config.SleepTime);

                    result.Add(ConfiguracaoServico.Instancia.cUF);
                }
                );
            }

            Thread.Sleep(TimeSpan.FromSeconds(12));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Distinct().Count() < 4);
        }
    }
}
