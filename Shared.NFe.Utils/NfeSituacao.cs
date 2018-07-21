namespace NFe.Utils
{
    public static class NfeSituacao
    {
        public static bool Autorizada(int cStat)
        {
            return cStat == 100 /*Autorizado o uso da NF-e*/
                | cStat == 150 /*Autorizado o uso da NF-e, autorização fora de prazo*/;
        }

        public static bool Cancelada(int cStat)
        {
            return cStat == 101 /*Cancelamento de NF-e homologado*/
                | cStat == 151 /*Cancelamento de NF-e homologado fora de prazo*/
                | cStat == 218 /*NF-e já está cancelada na base de dados da SEFAZ [nRec:999999999999999]*/
                | cStat == 420 /*Rejeição: Cancelamento para NF-e já cancelada*/;
        }

        public static bool Denegada(int cStat)
        {
            return cStat == 110 /*Uso Denegado*/
                | cStat == 301 /*Uso Denegado: Irregularidade fiscal do emitente*/
                | cStat == 302 /*Uso Denegado: Irregularidade fiscal do destinatário*/
                | cStat == 303 /*Uso Denegado: Destinatário não habilitado a operar na UF*/;
        }

        public static bool Inutilizada(int cStat)
        {
            return cStat == 102
                | cStat == 206 /*Rejeição: NF-e já está inutilizada na Base de dados da SEFAZ*/
                | cStat == 256 /*Número da faixa já inutilizado*/
                | cStat == 563 /*Rejeição: Já existe pedido de Inutilização com a mesma faixa de inutilização*/;
        }

        public static bool Rejeitada(int cStat)
        {
            //if ((protNfeRetorno.infProt.cStat >= 201) & (protNfeRetorno.infProt.cStat <= 299) | (protNfeRetorno.infProt.cStat >= 401)) //Rejeitada (Antigo tratamento de rejeição)
            return cStat >= 201 & !Autorizada(cStat) & !Cancelada(cStat) & !Denegada(cStat) & !Inutilizada(cStat);
        }

        public static bool LoteRecebido(int cStat)
        {
            return cStat == 103;
        }

        public static bool LoteProcessado(int cStat)
        {
            return cStat == 104;
        }
    }
}
