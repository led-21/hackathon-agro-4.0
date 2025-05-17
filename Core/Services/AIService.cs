using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Core.Data;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Core.Services
{
    public class AIService
    {
        SecretClient client { get; set; }
        Kernel? kernel { get; set; }
        IChatCompletionService chatCompletionService { get; set; }

        public AIService()
        {
            client = new SecretClient(new Uri("https://aitestevault.vault.azure.net/"), new DefaultAzureCredential(new DefaultAzureCredentialOptions()
            {
                ExcludeEnvironmentCredential = true,
                AdditionallyAllowedTenants = { "*" }
            }));

            KeyVaultSecret openAISecret = client.GetSecret("openai");
            KeyVaultSecret aiSearchSecret = client.GetSecret("aisearch");

            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            kernelBuilder.AddAzureOpenAIChatCompletion("gpt-4o-mini", "https://adria-mac1pdzj-eastus2.cognitiveservices.azure.com/", openAISecret.Value);

            Kernel kernel = kernelBuilder.Build();

            chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
        }

        public async Task<string> AvaliarReceituario(string receituario, string bula)
        {
            ChatHistory chatHistory = [];

            chatHistory.AddSystemMessage("Você é um assistente de inteligência artificial especializado em análise de receituários agronômicos e bulas de produtos fitossanitários." +
                "Seu objetivo é comparar o receituário e a bula fornecidos, identificando inconsistências e não conformidades." +
                "Se houver alguma inconsistência ou não conformidade, forneça uma explicação detalhada e sugira correções." +
                "Se tudo estiver correto, informe que não há inconsistências ou não conformidades." +
                $"Exemplo de resposta: {AgronomicMockData.exemplo}");

            chatHistory.AddUserMessage("Compare o receituário e a bula fornecidos, identificando inconsistências e não conformidades." +
                $"Receituário:\n{receituario}" +
                $"Bula:\n{bula}");

            var response = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);
            var result = "";

            await foreach (var chunk in response)
            {
                Console.Write(chunk.Content);
                result += chunk.Content;
            }

            Console.WriteLine();

            return result;
        }
    }
}
