using Core.Data;
using Core.Services;

AIService aIService = new();

var search = await aIService.AvaliarReceituarioComBusca(AgronomicMockData.receituario);
Console.WriteLine(search);

var result = await aIService.AvaliarReceituario(AgronomicMockData.receituario, AgronomicMockData.bula);

MarkdownService.SaveAndOpen(result).Wait();