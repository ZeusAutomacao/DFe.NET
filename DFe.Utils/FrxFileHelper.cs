using System;
using System.IO;
using System.Reflection;

namespace DFe.Utils
{
    public static class FrxFileHelper
    {
        public static byte[] TryGetFrxFile(string caminho)
        {
            try
            {
                if (!caminho.EndsWith(".frx"))
                {
                    caminho += ".frx";
                }

                // Os caminhos são informados com "\" (ex.: @"CTe\CTeRetrato.frx"); em Linux/macOS "\" não é separador de diretório
                caminho = caminho.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);

                var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("Erro no Zeus. Assembly de relatório nao encontrado"), caminho);
                var bytes = File.ReadAllBytes(path);
                return bytes.Length == 0 ? null : bytes;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
