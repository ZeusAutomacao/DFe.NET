using DFe.Utils;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System;

namespace NFe.Utils.Tributacao.Federal
{
    public class COFINSGeral
    {
        public COFINSGeral(COFINSBasico cofinsBasico)
        {
            this.CopiarPropriedades(cofinsBasico);
        }

        public COFINSGeral()
        {

        }

        public COFINSBasico ObterCOFINSBasico()
        {
            COFINSBasico cofinsBasico;

            switch (CST)
            {
                case CSTCOFINS.cofins01:
                case CSTCOFINS.cofins02:
                    cofinsBasico = new COFINSAliq();
                    break;
                case CSTCOFINS.cofins03:
                    cofinsBasico = new COFINSQtde();
                    break;
                case CSTCOFINS.cofins04:
                case CSTCOFINS.cofins05:
                case CSTCOFINS.cofins06:
                case CSTCOFINS.cofins07:
                case CSTCOFINS.cofins08:
                case CSTCOFINS.cofins09:
                    cofinsBasico = new COFINSNT();
                    break;
                case CSTCOFINS.cofins49:
                case CSTCOFINS.cofins50:
                case CSTCOFINS.cofins51:
                case CSTCOFINS.cofins52:
                case CSTCOFINS.cofins53:
                case CSTCOFINS.cofins54:
                case CSTCOFINS.cofins55:
                case CSTCOFINS.cofins56:
                case CSTCOFINS.cofins60:
                case CSTCOFINS.cofins61:
                case CSTCOFINS.cofins62:
                case CSTCOFINS.cofins63:
                case CSTCOFINS.cofins64:
                case CSTCOFINS.cofins65:
                case CSTCOFINS.cofins66:
                case CSTCOFINS.cofins67:
                case CSTCOFINS.cofins70:
                case CSTCOFINS.cofins71:
                case CSTCOFINS.cofins72:
                case CSTCOFINS.cofins73:
                case CSTCOFINS.cofins74:
                case CSTCOFINS.cofins75:
                case CSTCOFINS.cofins98:
                case CSTCOFINS.cofins99:
                    cofinsBasico = new COFINSOutr();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            cofinsBasico.CopiarPropriedades(this);
            return cofinsBasico;
        }

        /** São obrigatórias apenas as propriedades referentes as tags que existam em todos os grupos do COFINS **/

        public CSTCOFINS CST { get; set; }

        public decimal? vBC { get; set; }

        public decimal? pCOFINS { get; set; }

        public decimal? vCOFINS { get; set; }

        public decimal? qBCProd { get; set; }

        public decimal? vAliqProd { get; set; }

    }
}
