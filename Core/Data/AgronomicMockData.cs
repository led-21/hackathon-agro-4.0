namespace Core.Data;

public class AgronomicMockData
{
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
Dose: 200 mL/ha
Volume de Calda: 150 L/ha
Modo de Aplicação: Pulverização foliar com trator
Época de Aplicação: Início do estágio R3
Intervalo de Segurança: 7 dias
Número Máximo de Aplicações: 2 por ciclo
Equipamento de Proteção Individual (EPI): Uso obrigatório conforme legislação vigente

Data de Emissão: 16/05/2025
Assinatura do Responsável Técnico: ______________________
";


    public static string bula = @"
Bula do Produto: Engeo Pleno S

Composição:
- Tiametoxam: 141 g/L
- Lambda-cialotrina: 106 g/L

Classificação:
- Classe Toxicológica: Categoria 4 (Pouco Tóxico)
- Classe Ambiental: Classe I (Altamente Perigoso ao Meio Ambiente)

Engeo Pleno S - Soja
Percevejo-marrom (Euschistus heros)
Dose: 200 mL/ha
Intervalo de segurança: 7 dias
Método de Aplicação: Pulverização foliar com trator
Calda Terrestre: 150L
Intervalo de aplicação: Realizar no máximo 2 aplicações em intervalos de 7 dias.
Época de aplicação: Inspecionar periodicamente a cultura e pulverizar quando forem constatados os primeiros percevejos nos órgãos florais (Início do estágio R3). A maior dose deve ser utilizada em condições de alta população da praga e condições de clima favorável ao seu desenvolvimento.

Precauções:
- Utilizar Equipamentos de Proteção Individual (EPI) durante a manipulação e aplicação.
- Evitar a contaminação de corpos d'água.
- Armazenar o produto em local seco e ventilado, fora do alcance de crianças e animais.

Data da Última Atualização da Bula: 16/02/2024
";


    static public string exemplo = @"
# Análise Comparativa: Receituário vs Bula Oficial

## Comparação de Dados Técnicos

| Parâmetro | Receituário | Bula Oficial | Conformidade |
|-----------|-------------|--------------|--------------|
| *Produto/Princípios Ativos* | Fungicida sistêmico Azoxistrobina + Ciproconazol | AZOXISTROBIN 200 CIPROCONAZOLE 80 CCAB SC | ✅ Conforme |
| *Cultura* | Soja (Glycine max) | Soja | ✅ Conforme |
| *Doença-alvo* | Ferrugem asiática (Phakopsora pachyrhizi) em estágio inicial | Ferrugem-asiática (Phakopsora pachyrhizi) | ✅ Conforme |
| *Dose* | 0,3 L/ha (300 mL/ha) | 300 mL/ha | ✅ Conforme |
| *Volume de Calda* | 150 L de água | Adicionar adjuvante a 0,5% do volume de calda (máximo 600 mL/ha de adjuvante) | ⚠ Parcialmente conforme* |
| *Método de Aplicação* | Aplicação terrestre com pulverizador de barra | Não especificado diretamente na seção visualizada | ⚠ Não verificável |
| *Momento de Aplicação* | Preventivamente ou nos primeiros sintomas | Preventivamente ou nos primeiros sintomas | ✅ Conforme |
| *Intervalo entre Aplicações* | 14 dias | 14 a 21 dias | ✅ Conforme |
| *Período de Carência* | 30 dias antes da colheita | Não visualizado na seção específica para soja | ⚠ Não verificável |
| *Uso de EPI* | Completo durante preparo e aplicação | Completo durante preparo e aplicação | ✅ Conforme |
| *Restrições Climáticas* | Não aplicar em condições de vento forte ou chuva iminente | Não aplicar em condições climáticas desfavoráveis | ✅ Conforme |

*Observação sobre volume de calda: O receituário especifica 150 L/ha, enquanto a bula menciona apenas o uso de adjuvante a 0,5% do volume de calda, sem especificar o volume total recomendado para soja.

## Análise de Discrepâncias

1. *Volume de Calda*: O receituário especifica 150 L/ha, pulverização foliar com trator, adjuvante a 0,5% do volume.

2. *Método de Aplicação*: O receituário especifica aplicação terrestre com pulverizador de barra, mas esta informação não foi encontrada na seção da bula visualizada.

3. *Período de Carência*: O receituário indica 30 dias antes da colheita, mas esta informação específica para soja não foi encontrada na seção da bula visualizada.

## Conclusão

O receituário apresenta alta conformidade com as recomendações da bula oficial para os parâmetros principais como princípios ativos, cultura-alvo, doença, dose e intervalo entre aplicações. As pequenas discrepâncias ou informações não verificáveis não comprometem significativamente a qualidade técnica do receituário, que segue as diretrizes essenciais para o controle da ferrugem asiática em soja.
";

}
