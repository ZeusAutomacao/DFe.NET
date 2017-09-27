using DFe.DocumentosEletronicos.CTe.Classes.Retorno;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.RetRecepcao;

namespace DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe
{
    public class RetornoEnviarCte
    {
        public retEnviCte RetEnviCte { get; private set; }
        public retConsReciCTe RetConsReciCTe { get; private set; }
        public cteProc CteProc { get; private set; }

        public RetornoEnviarCte(retEnviCte retEnviCte, retConsReciCTe retConsReciCTe, cteProc cteProc)
        {
            RetEnviCte = retEnviCte;
            RetConsReciCTe = retConsReciCTe;
            CteProc = cteProc;
        }
    }
}