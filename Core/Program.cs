// See https://aka.ms/new-console-template for more information
using Core.Data;
using Core.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var client = new SecretClient(new Uri("https://aitestevault.vault.azure.net/"), new DefaultAzureCredential(new DefaultAzureCredentialOptions()
{
    ExcludeEnvironmentCredential=true,
    AdditionallyAllowedTenants = {"*"}
}));

KeyVaultSecret openAISecret = client.GetSecret("openai");
KeyVaultSecret aiSearchSecret = client.GetSecret("aisearch");

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddAzureOpenAIChatCompletion("gpt-4o-mini", "https://adria-mac1pdzj-eastus2.cognitiveservices.azure.com/", openAISecret.Value);

Kernel kernel = kernelBuilder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

AIService aIService = new();

var result = await aIService.Run(chatCompletionService, AgronomicMockData.receituario, AgronomicMockData.bula);