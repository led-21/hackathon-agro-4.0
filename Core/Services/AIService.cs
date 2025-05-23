using Azure;
using Azure.Identity;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Azure.Security.KeyVault.Secrets;
using Core.Data;
using Core.KernelFunctions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Core.Services
{
    public class AIService
    {
        SecretClient client { get; set; }
        Kernel? kernel { get; set; }
        IChatCompletionService chatCompletionService { get; set; }
        SearchClient searchClient { get; set; }
        SearchClient searchClientRAG { get; set; }

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

            kernelBuilder.Plugins.AddFromType<EmailNotificationPlugin>("EmailNotification");

            kernelBuilder.AddAzureAISearchVectorStore(new Uri("https://ai-search-agro.search.windows.net"), new AzureKeyCredential(aiSearchSecret.Value));

            kernel = kernelBuilder.Build();

            chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

            searchClient = new SearchClient(new Uri("https://ai-search-agro.search.windows.net"), "azureblob-index-agro", new AzureKeyCredential(aiSearchSecret.Value));

            searchClientRAG = new SearchClient(new Uri("https://ai-search-agro.search.windows.net"), "rag-1747865311429", new AzureKeyCredential(aiSearchSecret.Value));
        }

        public async Task<string> AvaliarReceituario(string receituario, string bula)
        {
            ChatHistory chatHistory = [];

            chatHistory.AddSystemMessage("Você é um assistente de inteligência artificial especializado em análise de receituários agronômicos e bulas de produtos fitossanitários." +
                "Seu objetivo é comparar o receituário e a bula fornecidos, identificando inconsistências e não conformidades." +
                "Se houver alguma inconsistência ou não conformidade, forneça uma explicação detalhada e sugira correções." +
                "Se tudo estiver correto, informe que não há inconsistências ou não conformidades." +
                "Se estiver no intervalo ou for compativel com a bula oficial mesmo com outras palavras está conforme" +
                "Em apenas caso de algum item Não conforme enviar notificaçao" +
                $"Exemplo de resposta: {AgronomicMockData.exemplo}");

            chatHistory.AddUserMessage("Compare o receituário e a bula fornecidos, identificando inconsistências e não conformidades." +
                $"Receituário:\n{receituario}" +
                $"Bula:\n{bula}");

            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var response = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory,
                executionSettings: openAIPromptExecutionSettings,
                kernel: kernel);

            var result = "";

            await foreach (var chunk in response)
            {
                Console.Write(chunk.Content);
                result += chunk.Content;
            }

            Console.WriteLine();

            return result;
        }

        public async Task<string> BuscarBula(string receituario)
        {
            var searchOptions = new SearchOptions()
            {
                Size = 5,
            };

            var searchResults = await searchClient.SearchAsync<SearchDocument>(receituario, searchOptions);

            var result = searchResults.Value.GetResults().First().Document.ToString();
            Console.WriteLine(result);

            return result;
        }

        public async Task<string> BuscarRAG(string receituario)
        {
            var searchOptions = new SearchOptions()
            {
                Size = 5,
            };

            var searchResults = await searchClientRAG.SearchAsync<SearchDocument>(receituario, searchOptions);

            var result = string.Concat(searchResults.Value.GetResults()
                .Select(x => x.Document["chunk"]).ToList());
            Console.WriteLine(result);

            return result;
        }
    }
}
