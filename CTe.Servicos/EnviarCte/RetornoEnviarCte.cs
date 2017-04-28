using CTe.Classes;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Recepcao.Retorno;

namespace CTe.Servicos.EnviarCte
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