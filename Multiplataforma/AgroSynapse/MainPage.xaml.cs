using AgroSynapse.View;
using Markdig;
using System.Text;
using AgroSynapse.Data;

namespace AgroSynapse
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private const string BackendHostAddress = "http://localhost:5260";

        public static string receituario = AgronomicMockData.receituarioHerbicida; // Exemplo de receituário para teste
        public MainPage()
        {
            Application.Current!.UserAppTheme = AppTheme.Dark;
            InitializeComponent();
            _httpClient = new HttpClient();
            PrescriptionEntry.Text = receituario; // Preenche o campo de texto com o receituário de exemplo
        }

        // Evento do botão de análise
        private async void OnAnalyzeButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PrescriptionEntry.Text))
            {
                await DisplayAlert("Erro", "Por favor, insira o texto do receituário.", "OK");
                return;
            }

            await AnalyzePrescription(PrescriptionEntry.Text);
        }

        // Chamada à API
        private async Task AnalyzePrescription(string prescriptionText)
        {
            try
            {
                LoadingIndicator.IsVisible = true;
                ResultFrame.IsVisible = false;
                NotifyButton.IsVisible = false;

                // Envia o texto do receituário como JSON
                var content = new StringContent($"{{ \"receituario\": \"{Uri.EscapeDataString(prescriptionText)}\" }}", Encoding.UTF8, "application/json");
                string searchMethod = OptionsPage.GetSearchEndpoint();

                var response = await _httpClient.PostAsync($"{BackendHostAddress}{searchMethod}?receituario={Uri.EscapeDataString(prescriptionText)}", content);

                if (response.IsSuccessStatusCode)
                {
                    var markdownResult = await response.Content.ReadAsStringAsync();

                    string htmlResult = Markdig.Markdown.ToHtml(markdownResult,
                                                      (new MarkdownPipelineBuilder()
                                                      .UseAdvancedExtensions()
                                                      .UsePipeTables()
                                                      .Build())); // Exibe o resultado em Markdown

                    // Substitua o trecho problemático pelo código abaixo dentro do método AnalyzePrescription

                    ResultWebView.Source = new HtmlWebViewSource
                    {
                        Html = $@"
                    <!DOCTYPE html>
                    <html lang='pt-BR'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <style>
                            body {{
                                background-color: #181818;
                                color: #e0e0e0;
                                font-family: 'Segoe UI', Arial, sans-serif;
                                margin: 0;
                                padding: 1.5em;
                            }}
                            h1, h2, h3, h4, h5, h6 {{
                                color: #90caf9;
                            }}
                            a {{
                                color: #80cbc4;
                            }}
                            table {{
                                width: 100%;
                                border-collapse: collapse;
                                background-color: #232323;
                            }}
                            th, td {{
                                border: 1px solid #333;
                                padding: 8px;
                            }}
                            th {{
                                background-color: #263238;
                                color: #b3e5fc;
                            }}
                            tr:nth-child(even) {{
                                background-color: #20232a;
                            }}
                            code, pre {{
                                background: #222;
                                color: #b5cea8;
                                border-radius: 4px;
                                padding: 2px 6px;
                            }}
                        </style>
                    </head>
                    <body>
                        {htmlResult}
                    </body>
                    </html>
                    "
                    };
                    ResultFrame.IsVisible = true;
                    NotifyButton.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Erro", "Falha ao analisar o receituário.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro na análise: {ex.Message}", "OK");
            }
            finally
            {
                LoadingIndicator.IsVisible = false;
            }
        }

        // Evento do botão de notificação
        private async void OnNotifyButtonClicked(object sender, EventArgs e)
        {
            // Simula o envio de notificação
            await DisplayAlert("Notificação", "Notificação enviada ao responsável técnico!", "OK");
            // Aqui você pode integrar com um serviço de e-mail, como Azure Notification Hub ou SendGrid
        }
    }

}
