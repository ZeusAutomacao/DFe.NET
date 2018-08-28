using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Dev.VersaoAssemblies
{
    internal static class ProgramMain
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Olá, sou seu assitente para alteração de versão em massa");
                Console.WriteLine("Para continuar preciso que informe qual versão deseja utilizar. Vamos la?");

                Console.Write("Então qual versão quer utilizar? (informe a versãoe aperter ENTER):");
                var versaoString = Console.ReadLine();

                FazerAlteracaoDasVersoes(versaoString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static void FazerAlteracaoDasVersoes(string versaoString)
        {
            var root = Environment.GetEnvironmentVariable("PROJETO_ZEUS_DFE") ?? string.Empty;

            if (!File.Exists(Path.Combine(root, "Zeus NFe.sln")))
            {
                throw new InvalidOperationException(
                    "Acho que você não está com a variavel de ambiente PROJETO_ZEUS_DFE configurada");
            }

            var diretorios = Directory.GetDirectories(root);

            foreach (var diretorio in diretorios)
            {
                var infoFile = Path.Combine(diretorio, "Properties", "AssemblyInfo.cs");

                if (!File.Exists(infoFile))
                {
                    continue;
                }

                var regexValidarVersao = new Regex(@"^[0-9]\.[0-9]{1,2}\.0\.[0-9]{1,2}$");

                if (!regexValidarVersao.IsMatch(versaoString))
                {
                    throw new InvalidOperationException(
                        "Opa, você não informou uma versão que eu esperava. Preciso de algo assim: X.X.0.XX?");
                }

                AlterarVersaoDoAssembly(infoFile, versaoString);
                Console.WriteLine("Alterado versão no AssemblyInfo: " + infoFile);
            }

            Console.WriteLine("Pronto terminei. Pode apertar qualquer tecla para fechar!");
        }

        private static void AlterarVersaoDoAssembly(string infoFile, string versaoString)
        {
            var content = File.ReadAllText(infoFile);

            var regexVersion = new Regex("assembly: AssemblyVersion.+\\)", RegexOptions.IgnoreCase);
            var regexFileVersion = new Regex("assembly: AssemblyFileVersion.+\\)", RegexOptions.IgnoreCase);

            var novoContent = regexVersion.Replace(content, "assembly: AssemblyVersion(\"" + versaoString + "\")");
            var contentFinal = regexFileVersion.Replace(novoContent, "assembly: AssemblyFileVersion(\"" + versaoString + "\")");

            File.WriteAllText(infoFile, contentFinal);
        }
    }
}