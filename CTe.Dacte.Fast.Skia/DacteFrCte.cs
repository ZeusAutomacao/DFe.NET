using System;
using CTe.Classes;
using CTe.Dacte.Base;
using FastReport;
using System.IO;
using DFe.Utils;

namespace CTe.Dacte.Fast.Skia
{
    public class DacteFrCte : DacteFastBase
    {
        public DacteFrCte()
        {
            Relatorio = new Report();
        }

        public DacteFrCte(cteProc proc, ConfiguracaoDacte config, string arquivoRelatorio = "")
        {
            Relatorio = new Report();
            RegisterData(proc);

            if (string.IsNullOrWhiteSpace(arquivoRelatorio))
            {
                const string caminho = @"CTe\CTeRetrato.frx";
                var frx = FrxFileHelper.TryGetFrxFile(caminho);
                if (frx == null || frx.Length == 0)
                    throw new Exception($"Erro em DacteFrCte. Relatório '{caminho}' não encontrado, passe o parametro 'arquivoRelatorio' com o caminho do arquivo");
                Relatorio.Load(new MemoryStream(frx));
            }
            else
            {
                Relatorio.Load(arquivoRelatorio);
            }

            Configurar(config);
        }

        public void RegisterData(cteProc proc)
        {
            Relatorio.RegisterData(new[] { proc }, "cteProc", 20);
            Relatorio.GetDataSource("cteProc").Enabled = true;
        }

        public void Configurar(ConfiguracaoDacte config)
        {
            Relatorio.SetParameterValue("DoocumentoCancelado", config.DocumentoCancelado);
            Relatorio.SetParameterValue("Desenvolvedor", config.Desenvolvedor);
            Relatorio.SetParameterValue("QuebrarLinhasObservacao", config.QuebrarLinhasObservacao);

            if (Relatorio.FindObject("poEmitLogo") != null)
                ((PictureObject)Relatorio.FindObject("poEmitLogo")).SetImageData(config.Logomarca);
        }
    }
}
