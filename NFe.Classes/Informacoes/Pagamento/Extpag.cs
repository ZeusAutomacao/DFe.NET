using System;

namespace NFe.Classes.Informacoes.Pagamento
{
    public static class Extpag
    {
        public static string ObtemDescricao(this pag pag)
        {
            switch (pag.tPag)
            {
                case FormaPagamento.fpDinheiro:
                    return "Dinheiro";
                case FormaPagamento.fpCheque:
                    return "Cheque";
                case FormaPagamento.fpCartaoCredito:
                    return "Cartão de Crédito";
                case FormaPagamento.fpCartaoDebito:
                    return "Cartão de Débito";
                case FormaPagamento.fpCreditoLoja:
                    return "Crédito Loja";
                case FormaPagamento.fpValeAlimentacao:
                    return "Vale Alimentação";
                case FormaPagamento.fpValeRefeicao:
                    return "Vale Refeição";
                case FormaPagamento.fpValePresente:
                    return "Vale Presente";
                case FormaPagamento.fpValeCombustivel:
                    return "Vale Combustível";
                case FormaPagamento.fpOutro:
                    return "Outros";
                default: throw new ArgumentException("Forma pagamento inválida");

            }
        }
    }
}