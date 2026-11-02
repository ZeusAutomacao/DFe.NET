namespace NFe.Danfe.PdfClown.Modelo
{
    public class ProdutoViewModel
    {
        /// <summary>Classificação tributária IBS/CBS informada no item.</summary>
        public string ClassificacaoTributariaIbsCbs { get; set; }
        /// <summary>Base de cálculo do IBS e CBS.</summary>
        public decimal? BaseIbsCbs { get; set; }
        /// <summary>Alíquota do IBS da UF, efetiva quando houver redução.</summary>
        public decimal? AliquotaIbsUf { get; set; }
        /// <summary>Alíquota do IBS municipal, efetiva quando houver redução.</summary>
        public decimal? AliquotaIbsMunicipio { get; set; }
        /// <summary>Alíquota da CBS, efetiva quando houver redução.</summary>
        public decimal? AliquotaCbs { get; set; }
        /// <summary>Valor do IBS da UF.</summary>
        public decimal? ValorIbsUf { get; set; }
        /// <summary>Valor do IBS municipal.</summary>
        public decimal? ValorIbsMunicipio { get; set; }
        /// <summary>Valor da CBS.</summary>
        public decimal? ValorCbs { get; set; }
        /// <summary>Base de cálculo do Imposto Seletivo.</summary>
        public decimal? BaseImpostoSeletivo { get; set; }
        /// <summary>Alíquota do Imposto Seletivo.</summary>
        public decimal? AliquotaImpostoSeletivo { get; set; }
        /// <summary>Valor do Imposto Seletivo.</summary>
        public decimal? ValorImpostoSeletivo { get; set; }

        /// <summary>
        /// <para>Código do produto ou serviço</para>
        /// <para>Tag cProd</para>
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// <para>Informações Adicionais do Produto</para>
        /// <para>Tag infAdProd</para>
        /// </summary>
        public string InformacoesAdicionais { get; set; }

        /// <summary>
        /// <para>Descrição do produto ou serviço</para>
        /// <para>Tag xProd</para>
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// <para>Código NCM com 8 dígitos ou 2 dígitos (gênero)</para>
        /// <para>Tag NCM</para>
        /// </summary>
        public string Ncm { get; set; }


        /// <summary>
        /// <para>Origem da mercadoria + Tributação do ICMS</para>
        /// <para>Tag orig e CST</para>
        /// </summary>
        public string OCst { get; set; }

        /// <summary>Identifica CSOSN quando informado no grupo ICMS do item.</summary>
        public bool PossuiCsosn { get; set; }

        /// <summary>
        /// <para>Código Fiscal de Operações e Prestações</para>
        /// <para>Tag CFOP</para>
        /// </summary>
        public int Cfop { get; set; }

        /// <summary>
        /// <para>Unidade Comercial</para>
        /// <para>Tag uCom</para>
        /// </summary>
        public string Unidade { get; set; }

        /// <summary>
        /// <para>Quantidade Comercial</para>
        /// <para>Tag qCom</para>
        /// </summary>
        public double Quantidade { get; set; }

        /// <summary>
        /// <para>Valor Unitário de Comercialização</para>
        /// <para>Tag vUnCom</para>
        /// </summary>
        public double ValorUnitario { get; set; }

        /// <summary>
        /// <para>Valor Total Bruto dos Produtos ou Serviços</para>
        /// <para>Tag vProd</para>
        /// </summary>
        public double ValorTotal { get; set; }

        /// <summary>
        /// <para>Valor da BC do ICMS</para>
        /// <para>Tag vBC</para>
        /// </summary>
        public double BaseIcms { get; set; }

        /// <summary>
        /// <para>Valor do ICMS</para>
        /// <para>Tag vICMS</para>
        /// </summary>
        public double ValorIcms { get; set; }

        /// <summary>
        /// <para>Alíquota do imposto</para>
        /// <para>Tag pICMS</para>
        /// </summary>
        public double AliquotaIcms { get; set; }

        /// <summary>
        /// <para>Valor do IPI</para>
        /// <para>Tag vIPI</para>
        /// </summary>
        public double? ValorIpi { get; set; }

        /// <summary>Base de cálculo do IPI informada no XML.</summary>
        public double? BaseIpi { get; set; }

        /// <summary>
        /// <para>Alíquota do IPI</para>
        /// <para>Tag pIPI</para>
        /// </summary>
        public double? AliquotaIpi { get; set; }

        /// <summary>
        /// <para>Valor aproximado total de tributos federais, estaduais e municipais. [NT2013.003]</para>
        /// <para>Tag vTotTrib</para>
        /// </summary>
        public double? ValorAproximadoTributos { get; set; }

        public ProdutoViewModel()
        {
            AliquotaIpi = null;
            ValorIpi = null;
        }

        public string DescricaoCompleta
        {
            get
            {
                string descriCaoCompleta = Descricao;

                if (!string.IsNullOrWhiteSpace(InformacoesAdicionais))
                {
                    descriCaoCompleta += "\r\n" + InformacoesAdicionais;
                }

                return descriCaoCompleta;
            }
        }
    }
}
