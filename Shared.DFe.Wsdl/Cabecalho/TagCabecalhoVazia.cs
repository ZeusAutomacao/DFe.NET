using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.Wsdl.Cabecalho
{
    public class TagCabecalhoVazia : ITagCabecalho
    {
        public string GetTagCabecalho(DFeCabecalho dfeCabecalho)
        {
            return string.Empty;
        }
    }
}