using System.Windows;
using Microsoft.Win32;

namespace NFe.Danfe.App.Teste.Html;

public static class Funcoes
{
    /// <summary>
    ///     Exibe um diálogo com uma mensagem para o usuário, utilizando um ModernDialog
    /// </summary>
    /// <param name="mensagem"></param>
    /// <param name="titulo"></param>
    /// <param name="botoes"></param>
    /// <param name="imagem"></param>
    public static void Mensagem(string mensagem, string titulo, MessageBoxButton botoes, MessageBoxImage imagem = MessageBoxImage.None)
    {
        MessageBox.Show(mensagem, titulo, botoes, imagem);
    }

    /// <summary>
    ///     Abre o diálogo de busca de arquivo com o filtro configurado para arquivos do tipo ".xml"
    /// </summary>
    /// <returns></returns>
    public static string BuscarArquivoXml()
    {
        return BuscarArquivo("Selecione o arquivo XML", ".xml", "Arquivo XML (.xml)|*.xml");
    }

    /// <summary>
    ///     Abre o diálogo de busca de arquivo com o filtro configurado para arquivos do tipo "PNG, Bitmap, JPEG, JPG e GIF"
    /// </summary>
    /// <returns></returns>
    public static string BuscarImagem()
    {
        return BuscarArquivo("Selecione uma imagem", ".png", "PNG (*.png)|*.png|Bitmap (*.bmp)|*.bmp|JPEG (*.jpeg)|*.jpeg|JPG (*.jpg)|*.jpg|GIF (*.gif)|*.gif");
    }

    /// <summary>
    ///     Abre o diálogo de busca de arquivo com com os dados passados no parâmetro
    /// </summary>
    /// <param name="arquivoPadrao">Nome do arquivo padrão a ser exibido no diálogo</param>
    /// <param name="titulo">Título do diálogo de buscar arquivo</param>
    /// <param name="extensaoPadrao">Extensão de arquivo padrão a ser exibida no diálogo</param>
    /// <param name="filtro">Filtro de extensões a ser exibido no diálogo</param>
    /// <returns></returns>
    public static string BuscarArquivo(string titulo, string extensaoPadrao, string filtro, string arquivoPadrao = null)
    {
        var dlg = new OpenFileDialog
        {
            Title = titulo,
            FileName = arquivoPadrao,
            DefaultExt = extensaoPadrao,
            Filter = filtro
        };
        dlg.ShowDialog();
        return dlg.FileName;
    }
}