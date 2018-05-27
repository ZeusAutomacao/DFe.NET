using System;
using System.Collections.Generic;
using System.Linq;
using DFe.Classes.Entidades;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Sefaz.Servidores;

namespace NFe.Utils.Sefaz
{
    public static class ServidorSefazFactory
    {
        private static readonly IList<IServidorSefaz> Servidores310 = new List<IServidorSefaz>();
        private static readonly IList<IServidorSefaz> Servidores400 = new List<IServidorSefaz>();

        static ServidorSefazFactory()
        {
            Servidores310.Add(new SefazAC());
            Servidores310.Add(new SefazAL());
            Servidores310.Add(new SefazAM());
            Servidores310.Add(new SefazAP());
            Servidores310.Add(new SefazBA());
            Servidores310.Add(new SefazCE());
            Servidores310.Add(new SefazDF());
            Servidores310.Add(new SefazES());
            Servidores310.Add(new SefazGO());
            Servidores310.Add(new SefazMA());
            Servidores310.Add(new SefazMG());
            Servidores310.Add(new SefazMS());
            Servidores310.Add(new SefazMT());
            Servidores310.Add(new SefazPA());
            Servidores310.Add(new SefazPB());
            Servidores310.Add(new SefazPE());
            Servidores310.Add(new SefazPI());
            Servidores310.Add(new SefazPR());
            Servidores310.Add(new SefazRJ());
            Servidores310.Add(new SefazRN());
            Servidores310.Add(new SefazRR());
            Servidores310.Add(new SefazRO());
            Servidores310.Add(new SefazSC());
            Servidores310.Add(new SefazSE());
            Servidores310.Add(new SefazSP());
            Servidores310.Add(new SefazTO());
            Servidores310.Add(new SefazRS());

            Servidores400.Add(new SefazAC4());
            Servidores400.Add(new SefazAL4());
            Servidores400.Add(new SefazAM4());
            Servidores400.Add(new SefazAP4());
            Servidores400.Add(new SefazBA4());
            Servidores400.Add(new SefazCE4());
            Servidores400.Add(new SefazDF4());
            Servidores400.Add(new SefazES4());
            Servidores400.Add(new SefazGO4());
            Servidores400.Add(new SefazMA4());
            Servidores400.Add(new SefazMG4());
            Servidores400.Add(new SefazMS4());
            Servidores400.Add(new SefazMT4());
            Servidores400.Add(new SefazPA4());
            Servidores400.Add(new SefazPB4());
            Servidores400.Add(new SefazPE4());
            Servidores400.Add(new SefazPI4());
            Servidores400.Add(new SefazPR4());
            Servidores400.Add(new SefazRJ4());
            Servidores400.Add(new SefazRN4());
            Servidores400.Add(new SefazRR4());
            Servidores400.Add(new SefazRO4());
            Servidores400.Add(new SefazSC4());
            Servidores400.Add(new SefazSE4());
            Servidores400.Add(new SefazSP4());
            Servidores400.Add(new SefazTO4());
            Servidores400.Add(new SefazRS4());
        }

        public static IServidorSefaz GetServidor(Estado estado, VersaoServico versaoLayout)
        {
            var compativel = Servidores310.FirstOrDefault(s => s.EstadoReferente == estado);

            if (versaoLayout == VersaoServico.ve400)
            {
                compativel = Servidores400.FirstOrDefault(s => s.EstadoReferente == estado);
            }

            if (compativel == null)
            {
                throw new InvalidOperationException("Estado não homologado pelo sistema para utilização da SEFAZ");
            }

            return (IServidorSefaz) compativel;
        }
    }
}