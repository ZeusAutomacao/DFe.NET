using DFe.Utils;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System;

namespace NFe.Utils.Tributacao.Federal
{
    public class PISGeral
    {
        public PISGeral(PISBasico pisBasico)
        {
            this.CopiarPropriedades(pisBasico);
        }

        public PISGeral()
        {

        }

        public PISBasico ObterPISBasico()
        {
            PISBasico pisBasico;

            switch (CST)
            {
                case CSTPIS.pis01:
                case CSTPIS.pis02:
                    pisBasico = new PISAliq();
                    break;
                case CSTPIS.pis03:
                    pisBasico = new PISQtde();
                    break;
                case CSTPIS.pis04:
                case CSTPIS.pis05:
                case CSTPIS.pis06:
                case CSTPIS.pis07:
                case CSTPIS.pis08:
                case CSTPIS.pis09:
                    pisBasico = new PISNT();
                    break;
                case CSTPIS.pis49:
                case CSTPIS.pis50:
                case CSTPIS.pis51:
                case CSTPIS.pis52:
                case CSTPIS.pis53:
                case CSTPIS.pis54:
                case CSTPIS.pis55:
                case CSTPIS.pis56:
                case CSTPIS.pis60:
                case CSTPIS.pis61:
                case CSTPIS.pis62:
                case CSTPIS.pis63:
                case CSTPIS.pis64:
                case CSTPIS.pis65:
                case CSTPIS.pis66:
                case CSTPIS.pis67:
                case CSTPIS.pis70:
                case CSTPIS.pis71:
                case CSTPIS.pis72:
                case CSTPIS.pis73:
                case CSTPIS.pis74:
                case CSTPIS.pis75:
                case CSTPIS.pis98:
                case CSTPIS.pis99:
                    pisBasico = new PISOutr();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            pisBasico.CopiarPropriedades(this);
            return pisBasico;
        }

        /** São obrigatórias apenas as propriedades referentes as tags que existam em todos os grupos do PIS **/

        public CSTPIS CST { get; set; }

        public decimal? vBC { get; set; }

        public decimal? pPIS { get; set; }

        public decimal? vPIS { get; set; }

        public decimal? qBCProd { get; set; }

        public decimal? vAliqProd { get; set; }

    }
}
