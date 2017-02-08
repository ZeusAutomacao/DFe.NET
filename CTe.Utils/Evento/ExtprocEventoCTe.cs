using CTeDLL.Classes.Servicos.Consulta;
using DFe.Utils;

public static class ExtprocEventoCTe
    {
        public static procEventoCTe CarregarDeArquivoXml(this procEventoCTe guia, string arquivoXml)
        {
            var s = FuncoesXml.ObterNodeDeArquivoXml(typeof(procEventoCTe).Name, arquivoXml);
            return FuncoesXml.XmlStringParaClasse<procEventoCTe>(s);
        }
    }