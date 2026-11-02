using System.Drawing;
using NFe.Danfe.PdfClown.Elementos;
using NFe.Danfe.PdfClown.Enumeracoes;
using NFe.Danfe.PdfClown.Graphics;
using NFe.Danfe.PdfClown.Modelo;
using NFe.Danfe.PdfClown.Tools;

namespace NFe.Danfe.PdfClown.Blocos
{
    internal class TabelaProdutosServicos : ElementoBase
    {
        public CabecalhoBloco CabecalhoBloco { get; private set; }
        public Tabela Tabela { get; private set; }
        public DanfeViewModel ViewModel { get; private set; }

        public TabelaProdutosServicos(DanfeViewModel viewModel, Estilo estilo) : base(estilo)
        {
            ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            CabecalhoBloco = new CabecalhoBloco(estilo, "DADOS DOS PRODUTOS / SERVIÇOS");

            var ad = AlinhamentoHorizontal.Direita;
            var ac = AlinhamentoHorizontal.Centro;
            var ae = AlinhamentoHorizontal.Esquerda;

            Tabela = new Tabela(Estilo);
            var cabecalho4 = ViewModel.Emitente.CRT == "3" ? "O/CST" : "O/CSOSN";

            if (ViewModel.ExibirTributosRtc)
            {
                Tabela
                    .ComColuna(0, ae, "DESCRIÇÃO / NCM / CLASS. TRIB.")
                    .ComColuna(7F, ac, "CST / CFOP")
                    .ComColuna(7F, ad, "QTD / UN")
                    .ComColunaDeValor(8F, ad, "VLR UNIT.")
                    .ComColunaDeValor(8F, ad, "VLR TOTAL")
                    .ComColuna(14F, ad, "BASES DE CÁLCULO")
                    .ComColuna(15F, ad, "ALÍQUOTAS")
                    .ComColuna(19F, ad, "VALOR DOS TRIBUTOS");
            }
            else if (ViewModel.Orientacao == Orientacao.Retrato)
            {
                Tabela
                .ComColuna(8.5f, ac, "CÓDIGO", "PRODUTO")
                .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                .ComColuna(5.6F, ac, "NCM/SH")
                .ComColuna(3.9F, ac, cabecalho4)
                .ComColuna(3.5F, ac, "CFOP")
                .ComColuna(3.25F, ac, "UN")
                .ComColunaDeValor(6F, ad, "QUANTI.")
                .ComColunaDeValor(6F, ad, "VALOR", "UNIT.")
                .ComColunaDeValor(6F, ad, "VALOR", "TOTAL")
                .ComColunaDeValor(6F, ad, "B CÁLC", "ICMS")
                .ComColunaDeValor(5, ad, "VALOR", "ICMS")
                .ComColunaDeValor(5, ad, "VALOR", "IPI")
                .ComColunaDeValor(3.5F, ad, "ALIQ.", "ICMS")
                .ComColunaDeValor(3.5F, ad, "ALIQ.", "IPI");
            }
            else if (ViewModel.Orientacao == Orientacao.Paisagem)
            {
                Tabela
                .ComColuna(8.1f, ac, "CÓDIGO PRODUTO")
                .ComColuna(0, ae, "DESCRIÇÃO DO PRODUTO / SERVIÇO")
                .ComColuna(5.5F, ac, "NCM/SH")
                .ComColuna(3.1F, ac, cabecalho4)
                .ComColuna(3.1F, ac, "CFOP")
                .ComColuna(3F, ac, "UN")
                .ComColunaDeValor(5.25F, ad, "QUANTI.")
                .ComColunaDeValor(5.6F, ad, "VALOR UNIT.")
                .ComColunaDeValor(5.6F, ad, "VALOR TOTAL")
                .ComColunaDeValor(5.6F, ad, "B CÁLC ICMS")
                .ComColunaDeValor(5.6F, ad, "VALOR ICMS")
                .ComColunaDeValor(5.6F, ad, "VALOR IPI")
                .ComColunaDeValor(3F, ad, "ALIQ.", "ICMS")
                .ComColunaDeValor(3F, ad, "ALIQ.", "IPI");
            }

            Tabela.AjustarLarguraColunas();

            foreach (var p in ViewModel.Produtos)
            {
                if (ViewModel.ExibirTributosRtc)
                {
                    Tabela.AdicionarLinha(new List<string>
                    {
                        p.DescricaoCompleta + "\nNCM " + p.Ncm +
                            (string.IsNullOrEmpty(p.ClassificacaoTributariaIbsCbs) ? "" : " / cClassTrib " + p.ClassificacaoTributariaIbsCbs),
                        p.OCst + "\n" + p.Cfop.Formatar("N0"),
                        p.Quantidade.Formatar() + "\n" + p.Unidade,
                        p.ValorUnitario.Formatar(),
                        p.ValorTotal.Formatar(),
                        "ICMS " + Numero(p.BaseIcms) + "\nIBS/CBS " + Numero(p.BaseIbsCbs) +
                            "\nIS " + Numero(p.BaseImpostoSeletivo) + "\nIPI " + Numero(p.ValorIpi),
                        "ICMS " + Percentual(p.AliquotaIcms) + " / CBS " + Percentual(p.AliquotaCbs) +
                            "\nIBS UF " + Percentual(p.AliquotaIbsUf) + " / IPI " + Percentual(p.AliquotaIpi) +
                            "\nIBS MUN " + Percentual(p.AliquotaIbsMunicipio) + " / IS " + Percentual(p.AliquotaImpostoSeletivo),
                        "ICMS " + Numero(p.ValorIcms) + " / CBS " + Numero(p.ValorCbs) +
                            "\nIBS UF " + Numero(p.ValorIbsUf) + " / IPI " + Numero(p.ValorIpi) +
                            "\nIBS MUN " + Numero(p.ValorIbsMunicipio) + " / IS " + Numero(p.ValorImpostoSeletivo)
                    });
                    continue;
                }
                var linha = new List<string>
                {
                    p.Codigo,
                    p.DescricaoCompleta,
                    p.Ncm,
                    p.OCst,
                    p.Cfop.Formatar("N0"),
                    p.Unidade,
                    p.Quantidade.Formatar(),
                    p.ValorUnitario.Formatar(),
                    p.ValorTotal.Formatar(),
                    p.BaseIcms.Formatar(),
                    p.ValorIcms.Formatar(),
                    p.ValorIpi.Formatar(),
                    p.AliquotaIcms.Formatar(),
                    p.AliquotaIpi.Formatar()
                };

                Tabela.AdicionarLinha(linha);
            }
        }

        private static string Numero(decimal? valor) => valor?.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")) ?? "";
        private static string Numero(double? valor) => valor?.ToString("N2", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")) ?? "";
        private static string Percentual(decimal? valor) => valor.HasValue ? Numero(valor) + "%" : "";
        private static string Percentual(double? valor) => valor.HasValue ? Numero(valor) + "%" : "";

        public override void Draw(Gfx gfx)
        {
            base.Draw(gfx);

            Tabela.SetPosition(RetanguloTabela.Location);
            Tabela.SetSize(RetanguloTabela.Size);
            Tabela.Draw(gfx);

            CabecalhoBloco.SetPosition(X, Y);
            CabecalhoBloco.Width = Width;
            CabecalhoBloco.Draw(gfx);
        }

        public RectangleF RetanguloTabela => BoundingBox.CutTop(CabecalhoBloco.Height);
        public Boolean CompletamenteDesenhada => Tabela.LinhaAtual == ViewModel.Produtos.Count;
        public override bool PossuiContono => false;
    }
}
