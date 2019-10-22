using DFe.Wsdl;

namespace CTe.CTeOSDocumento.Wsdl.Cabecalho
{
    public class TagCabecalhoVazia : ITagCabecalho
    {
        public string GetTagCabecalho(DFeCabecalho dfeCabecalho)
        {
            return string.Empty;
        }
    }
}