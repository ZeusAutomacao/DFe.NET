using System;
using System.IO;
using FastReport;
using CTe.Classes;
using CTe.Classes.Servicos.Consulta;
using DFe.Utils;

namespace CTe.Dacte.Fast.Skia
{
    public class DacteFrEvento : DacteFastBase
    {
        public DacteFrEvento()
        {
            Relatorio = new Report();
        }

        public DacteFrEvento(cteProc proc, procEventoCTe procEventoCTe, string desenvolvedor = "", string arquivoRelatorio = "")
        {
            Relatorio = new Report();

            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"CTe\CTeEvento.frx";
                var frx = FrxFileHelper.TryGetFrxFile(caminho);
                if (frx == null || frx.Length == 0)
                    throw new Exception($"Erro em DacteFrEvento. Relatório '{caminho}' não encontrado, passe o parametro 'arquivoRelatorio' com o caminho do arquivo");
                Relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                Relatorio.Load(arquivoRelatorio);
            }

            RegisterData(proc, procEventoCTe);
            Configurar(desenvolvedor: desenvolvedor);
        }

        public void RegisterData(cteProc proc, procEventoCTe procEventoCTe)
        {
            Relatorio.RegisterData(new[] { proc }, "cteProc", 20);
            Relatorio.GetDataSource("cteProc").Enabled = true;

            Relatorio.RegisterData(new[] { procEventoCTe }, "procEventoCTe", 20);
            Relatorio.GetDataSource("procEventoCTe").Enabled = true;
        }

        public void Configurar(string desenvolvedor = "")
        {
            Relatorio.SetParameterValue("Desenvolvedor", desenvolvedor);
        }
    }
}
