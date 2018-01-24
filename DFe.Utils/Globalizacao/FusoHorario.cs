using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace DFe.Utils.Globalizacao
{
    public class FusoHorario
    {
        private List<FusoHorarioDto> _fusoHorarios;

        public FusoHorario()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "DFe.Utils.Globalizacao.FusoHorarios.json";

            string fusoHorarioFile;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                fusoHorarioFile = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(fusoHorarioFile))
            {
                throw new FileNotFoundException("Arquivo de fuso horários não encontrado.");
            }

            _fusoHorarios = JsonConvert.DeserializeObject<List<FusoHorarioDto>>(fusoHorarioFile);
        }

        public int NoEstado(DateTime dataHora, string estado)
        {
            var fusoHorario = _fusoHorarios.Find(p => p.Uf == estado);

            if (fusoHorario == null)
            {
                throw new ArgumentException("Estado inválido.", nameof(estado));
            }

            var horarioDeVerao = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time").IsDaylightSavingTime(dataHora);

            if (horarioDeVerao)
            {
                return fusoHorario.Verao;
            }
            else
            {
                return fusoHorario.Normal;
            }
        }

        private class FusoHorarioDto
        {
            public string Uf { get; set; }
            public int Normal { get; set; }
            public int Verao { get; set; }
        }
    }
}