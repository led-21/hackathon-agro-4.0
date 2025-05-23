using Core.Data;
using Core.Services;

AIService aIService = new();

var search = await aIService.BuscarBula(AgronomicMockData.receituario);

var result = await aIService.AvaliarReceituario(AgronomicMockData.receituario, search);

MarkdownService.SaveAndOpen(result).Wait();