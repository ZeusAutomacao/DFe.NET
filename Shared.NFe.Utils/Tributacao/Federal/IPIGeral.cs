using DFe.Utils;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using System;

namespace NFe.Utils.Tributacao.Federal
{
    public class IPIGeral
    {
        public IPIGeral(IPIBasico ipiBasico)
        {
            this.CopiarPropriedades(ipiBasico);
        }

        public IPIGeral()
        {

        }

        public IPIBasico ObterIPIBasico()
        {
            IPIBasico ipiBasico;

            switch (CST)
            {
                case CSTIPI.ipi00:
                case CSTIPI.ipi49:
                case CSTIPI.ipi50:
                case CSTIPI.ipi99:
                    ipiBasico = new IPITrib();
                    break;
                case CSTIPI.ipi01:
                case CSTIPI.ipi02:
                case CSTIPI.ipi03:
                case CSTIPI.ipi04:
                case CSTIPI.ipi05:
                case CSTIPI.ipi51:
                case CSTIPI.ipi52:
                case CSTIPI.ipi53:
                case CSTIPI.ipi54:
                case CSTIPI.ipi55:
                    ipiBasico = new IPINT();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            ipiBasico.CopiarPropriedades(this);
            return ipiBasico;
        }

        /** São obrigatórias apenas as propriedades referentes as tags que existam em TODOS os grupos do IPI **/

        public CSTIPI CST { get; set; }

        public decimal? vBC { get; set; }

        public decimal? pIPI { get; set; }

        public decimal? vIPI { get; set; }

        public decimal? qUnid { get; set; }

        public decimal? vUnid { get; set; }

    }
}
