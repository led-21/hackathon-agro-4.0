namespace AgroSynapse.View;

public partial class OptionsPage : ContentPage
{
    public OptionsPage()
    {
        InitializeComponent();
        MethodPicker.SelectedItem = Preferences.Get("SearchMethod", "");
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        Preferences.Set("SearchMethod", (string)MethodPicker.SelectedItem);
        DisplayAlert("Sucesso", "Método salvo com sucesso.", "OK");
    }

    // Métodos para acessar os endpoints de qualquer lugar do app
    public static string GetSearchEndpoint() => GetSearchSelection(Preferences.Get("SearchMethod", ""));

    public static string GetSearchSelection(string method) => method switch
    {
        "Azure Search RAG" => "/verificar/receituario/rag",
        _ => "/verificar/receituario"
    };
}