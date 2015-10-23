namespace NFe.Impressao
{
    public interface IDanfe
    {
        /// <summary>
        /// Abre a janela de visualização do DANFE
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        void Visualizar(bool modal = true);

        /// <summary>
        ///  Abre a janela de visualização do design do DANFE.
        /// Chame esse método se desja fazer alterações no design do DANFE em modo run-time
        /// </summary>
        /// <param name="modal">Se true, exibe a visualização em Modal. O modo modal está disponível apenas para WinForms</param>
        void ExibirDesign(bool modal = false);

        /// <summary>
        /// Envia a impressão do DANFE diretamente para a impressora
        /// </summary>
        /// <param name="exibirDialogo">Se true exibe o diálogo Imprimindo...</param>
        /// <param name="impressora">Passe a string com o nome da impressora para imprimir diretamente em determinada impressora. Caso contrário, a impressão será feita na impressora que estiver como padrão</param>
        void Imprimir(bool exibirDialogo = true, string impressora = "");
    }
}