using System.Collections.Generic;
using CTeDLL.Classes.Servicos;
using CTeDLL.Servicos.Factory;
using CTeDLL.Utils.CTe;
using CTeEletronico = CTe.Classes.CTe;

namespace CTeDLL.Servicos.Recepcao
{
    public class ServicoCTeRecepcao
    {
        public void CTeRecepcao(int lote, List<CTeEletronico> cteEletronicosList)
        {
            var enviCte = ClassesFactory.CriaEnviCTe(lote, cteEletronicosList);

            foreach (var cte in enviCte.CTe)
            {
                cte.Assina();
                cte.ValidaSchema();
            }

            enviCte.ValidaSchema();
            enviCte.SalvarXmlEmDisco();


        }
    }
}