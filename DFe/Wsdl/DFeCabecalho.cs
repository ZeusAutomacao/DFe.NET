using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;

namespace DFe.Wsdl
{
    public class DFeCabecalho
    {
        public DFeCabecalho(Estado codigoUF, VersaoServico versaoServico)
        {
            CodigoUF = codigoUF;
            VersaoServico = versaoServico;
        }

        public Estado CodigoUF { get; }
        public VersaoServico VersaoServico { get; }
    }
}