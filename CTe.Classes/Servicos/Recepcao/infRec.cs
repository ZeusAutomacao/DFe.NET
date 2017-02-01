namespace CTeDLL.Classes.Servicos.Recepcao
{
    public class infRec
    {
        /// <summary>
        ///     AR08 - Número do Recibo gerado pelo Portal da Secretaria de Fazenda Estadual (vide item 5.5).
        /// </summary>
        public string nRec { get; set; }

        /// <summary>
        ///     AR10 - Tempo médio de resposta do serviço (em segundos) dos últimos 5 minutos (vide item 5.7). Nota: Caso o tempo
        ///     médio de resposta fique abaixo de 1 (um) segundo, o tempo será informado como 1 segundo. Arredondar as frações de
        ///     segundos para cima.
        /// </summary>
        public int tMed { get; set; }
    }
}