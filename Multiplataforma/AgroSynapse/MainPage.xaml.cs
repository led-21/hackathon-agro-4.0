using System.Text;

namespace AgroSynapse
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private const string BackendHostAddress = "http://localhost:5260";

        public static string receituario = @"
Receituário Agronômico

Profissional Responsável:
Nome: Eng. Agrônomo João da Silva
CREA: 123456789
ART: 987654321

Usuário:
Nome: Maria Oliveira
CPF: 123.456.789-00

Propriedade:
Fazenda Boa Esperança
Município: Campo Grande - MS

Diagnóstico:
Infestação de percevejo-marrom (Euschistus heros) na cultura da soja, estágio R3.

Produto Prescrito:
Nome Comercial: Engeo Pleno S
Ingrediente Ativo: Tiametoxam 141 g/L + Lambda-cialotrina 106 g/L
Classe: Inseticida
Dose: 100 mL/ha
Volume de Calda: 2150 L/ha
Modo de Aplicação: Pulverização foliar com trator
Época de Aplicação: Início do estágio R3
Intervalo de Segurança: 7 dias
Número Máximo de Aplicações: 2 por ciclo
Equipamento de Proteção Individual (EPI): Uso obrigatório conforme legislação vigente

Data de Emissão: 16/05/2025
Assinatura do Responsável Técnico: ______________________
";

        public MainPage()
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
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
                var response = await _httpClient.PostAsync($"{BackendHostAddress}/verificar/receituario?receituario={Uri.EscapeDataString(prescriptionText)}", content);

                if (response.IsSuccessStatusCode)
                {
                    var markdownResult = await response.Content.ReadAsStringAsync();
                    ResultLabel.Text = markdownResult; // Exibe o resultado em Markdown
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
