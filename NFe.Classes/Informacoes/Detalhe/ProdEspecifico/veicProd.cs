namespace NFe.Classes.Informacoes.Detalhe.ProdEspecifico
{
    public class veicProd : ProdutoEspecifico
    {
        /// <summary>
        ///     J02 - Tipo da operação
        /// </summary>
        
        public TipoOperacao tpOp { get; set; }

        /// <summary>
        ///     J03 - Chassi do veículo
        /// </summary>
        public string chassi { get; set; }

        /// <summary>
        ///     J04 - Cor(Código de cada montadora)
        /// </summary>
        public string cCor { get; set; }

        /// <summary>
        ///     J05 - Descrição da Cor
        /// </summary>
        public string xCor { get; set; }

        /// <summary>
        ///     J06 - Potência Motor (CV)
        /// </summary>
        public string pot { get; set; }

        /// <summary>
        ///     J07 - Cilindradas
        /// </summary>
        public string cilin { get; set; }

        /// <summary>
        ///     J08 - Peso Líquido
        /// </summary>
        public string pesoL { get; set; }

        /// <summary>
        ///     J09 - Peso Bruto
        /// </summary>
        public string pesoB { get; set; }

        /// <summary>
        ///     J10 - Serial (série)
        /// </summary>
        public string nSerie { get; set; }

        /// <summary>
        ///     J11 - Tipo de combustível. Utilizar Tabela RENAVAM (v2.0)
        /// </summary>
        public string tpComb { get; set; }

        /// <summary>
        ///     J12 - Número de Motor
        /// </summary>
        public string nMotor { get; set; }

        /// <summary>
        ///     J13 - Capacidade Máxima de Tração
        /// </summary>
        public string CMT { get; set; }

        /// <summary>
        ///     J14 - Distância entre eixos
        /// </summary>
        public string dist { get; set; }

        /// <summary>
        ///     J16 - Ano Modelo de Fabricação
        /// </summary>
        public int anoMod { get; set; }

        /// <summary>
        ///     J17 - Ano de Fabricação
        /// </summary>
        public int anoFab { get; set; }

        /// <summary>
        ///     J18 - Tipo de Pintura
        /// </summary>
        public string tpPint { get; set; }

        /// <summary>
        ///     J19 - Tipo de Veículo
        /// </summary>
        public string tpVeic { get; set; }

        /// <summary>
        ///     J20 - Espécie de Veículo
        /// </summary>
        public int espVeic { get; set; }

        /// <summary>
        ///     J21 - Condição do VIN
        /// </summary>
        public CondicaoVin VIN { get; set; }

        /// <summary>
        ///     J22 - Condição do Veículo
        /// </summary>
        public CondicaoVeiculo condVeic { get; set; }

        /// <summary>
        ///     J23 - Código Marca Modelo
        /// </summary>
        public string cMod { get; set; }

        /// <summary>
        ///     J24 - Código da Cor
        /// </summary>
        public string cCorDENATRAN { get; set; }

        /// <summary>
        ///     J25 - Capacidade máxima de lotação
        /// </summary>
        public int lota { get; set; }

        /// <summary>
        ///     J26 - Restrição
        /// </summary>
        public TipoRestricao tpRest { get; set; }
    }
}