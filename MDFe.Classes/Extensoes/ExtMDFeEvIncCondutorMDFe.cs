using DFe.Utils;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Configuracoes;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;

namespace MDFe.Classes.Extencoes
{
    public static class ExtMDFeEvIncCondutorMDFe
    {
        public static void ValidaSchema(this MDFeEvIncCondutorMDFe evIncCondutorMDFe)
        {
            var xmlIncluirCondutor = evIncCondutorMDFe.XmlString();

            switch (MDFeConfiguracao.VersaoWebService.VersaoLayout)
            {
                case VersaoServico.Versao100:
                    Validador.Valida(xmlIncluirCondutor, "evIncCondutorMDFe_v1.00.xsd");
                    break;
                case VersaoServico.Versao300:
                    Validador.Valida(xmlIncluirCondutor, "evIncCondutorMDFe_v3.00.xsd");
                    break;
            }
        }

        public static string XmlString(this MDFeEvIncCondutorMDFe evIncCondutorMDFe)
        {
            return FuncoesXml.ClasseParaXmlString(evIncCondutorMDFe);
        }
    }
}