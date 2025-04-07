# Contribuindo para o projeto DFe.NET

Bem-vindo(a) ao repositório do **DFe.NET**! Agradecemos seu interesse em contribuir para este projeto open source. Este documento tem o objetivo de orientar colaboradores, definir padrões de qualidade e facilitar a integração das contribuições. Leia atentamente as seções abaixo para que possamos trabalhar juntos de forma colaborativa e eficiente.

## Índice

1. [Abertura de issues](#abertura-de-issues)  
   - [Reportando possíveis bugs ou problemas](#reportando-possiveis-bugs-ou-problemas)  
   - [Solicitação de recursos ou melhorias](#solicitacao-de-recursos-ou-melhorias)  
   - [Dúvidas sobre o projeto, recursos e configurações](#duvidas-sobre-o-projeto-recursos-e-configuracoes)  
2. [Contribuindo](#contribuindo)  
   - [Fluxo de trabalho](#fluxo-de-trabalho)  
   - [Commits](#commits)  
   - [Pull requests](#pull-requests)  
3. [Convenções de código, padrões e boas práticas](#convencoes-de-codigo-padroes-e-boas-praticas)  
4. [Licença](#licenca)  

## Abertura de issues  

Issues podem ser abertas para tirar dúvidas, reportar possíveis bugs e problemas, além de sugerir novos recursos ou melhorias em funcionalidades existentes. Antes de abrir uma nova issue, verifique se já existe alguma aberta ou fechada relacionada ao mesmo tema. Se encontrar, junte-se à discussão e avalie se sua solicitação pode ser atendida nela. Caso a issue tenha tomado um rumo diferente e não atenda à sua necessidade específica, você pode contribuir para a discussão existente ou, se necessário, criar uma nova issue abordando um ponto diferente relacionado ao tema. Se não houver nenhuma issue relacionada, siga as diretrizes abaixo para abrir uma nova corretamente.

### Reportando possíveis bugs ou problemas

Ao reportar um possível bug ou problema, forneça uma descrição detalhada, incluindo:  

- Passos para reproduzir
- O comportamento esperado (se aplicável)
- O comportamento atual
- Imagens, stack traces ou qualquer outra informação relevante para facilitar a identificação e reprodução
- Detalhes sobre o ambiente em que ocorreu (versão do sistema operacional, dependências, se está usando pacotes via NuGet ou referências diretas, etc.).

Isso reduz o tempo gasto apenas para entender e replicar o problema.

**Modelo para reportar bugs:**

```markdown
**Descrição:**  
Ao tentar executar a funcionalidade X, ocorre um erro inesperado.  

**Passos para reproduzir:**  
1. Acesse a tela Y.  
2. Clique no botão Z.  
3. Observe que o erro ocorre.  

**Comportamento esperado:**  
O sistema deveria realizar a ação A e retornar B, sem erro.  

**Comportamento atual:**  
O sistema exibe a exceção "System.NullReferenceException".  

**Ambiente:**  
- Windows 10  
- .NET Framework 4.8  
- Versão do software: 1.2.3
- Utilizando Nuget
```  

### Solicitação de recursos ou melhorias

Antes de criar a issue solicitando algum recurso ou melhoria, verifique se a solicitação já não foi proposta ou discutida anteriormente para evitar duplicidade. Caso já exista uma discussão, contribua nela em vez de abrir uma nova issue. Se não houver nenhuma issue aberta sobre o assunto, descreva claramente o novo recurso, os benefícios que ele traria e, quando possível, o que seria necessário para viabilizar sua implementação.

**Modelo para solicitar recursos ou melhorias:**

```markdown
**Descrição:**  
Adicionar suporte à emissão de um novo tipo de documento fiscal eletrônico.  

**Benefícios:**  
- Ampliaria o suporte da biblioteca para documentos fiscais eletrônicos, atendendo mais usuários.  
- Etc...  

**Requisitos:**  
- O sistema precisaria dos schemas do novo documento fiscal eletrônico;  
- Etc...
```  

### Dúvidas sobre o projeto, recursos e configurações

Caso possua dúvidas sobre o uso do projeto, configurações, funcionalidades ou outros aspectos, verifique se sua dúvida já foi respondida na documentação do `README` ou em alguma issue (aberta ou fechada) que já foi discutida e resolvida. Caso encontre uma issue aberta relacionada, participe da discussão em vez de criar uma nova. Se a issue estiver fechada, confira se sua dúvida foi sanada. Caso contrário, descreva seu problema de forma detalhada, informando o contexto e os passos que já tentou para resolvê-lo, se pertinente, e abra uma nova issue.

**Modelo para abertura de dúvidas:**

```markdown
**Pergunta:**  
Como posso configurar o projeto para rodar em um ambiente Linux?

**Contexto:**  
Estou tentando executar o projeto no Ubuntu 22.04, mas estou enfrentando dificuldades para instalar as dependências corretamente.

**O que já tentei:**  
1. Instalei todas as dependências listadas na documentação.  
2. Executei o comando X, mas recebo o erro "Y".  
3. Tentei a solução alternativa Z, sem sucesso.

**Ambiente:**  
- Sistema Operacional: Ubuntu 22.04  
- .NET versão: 6.0  
- Dependências instaladas: A, B, C  
```

## Contribuindo  

Para realizar contribuições para a biblioteca, é fundamental seguir o fluxo de trabalho e respeitar as convenções definidas aqui.

### Fluxo de trabalho

Faça um fork do repositório no GitHub para iniciar a implementação. Contribuidores externos não possuem nível de permissão para criar branches no repositório. Dessa forma, é necessário realizar contribuições através de um fork.  

Com o fork realizado, deve-se criar uma branch para a implementação com base na branch principal. A branch principal é `master`, que reflete a versão estável e mais atualizada do projeto. Dessa forma, toda e qualquer contribuição deve ser realizada utilizando a branch `master`.  

A nova branch deve seguir o padrão de nomenclatura conforme o [Conventional Branch](https://conventional-branch.github.io/), por exemplo:
- `feature/nome_da_funcionalidade` para novas funcionalidades  
- `bugfix/descrição_do_bug` para correções de bugs  

Além disso, mantenha sua branch sempre sincronizada com a branch principal para minimizar conflitos durante o desenvolvimento.  

#### Commits

Os commits devem ser atômicos e lógicos, garantindo que cada um contenha apenas alterações relacionadas a uma única funcionalidade ou correção. Isso mantém o histórico do repositório organizado e facilita a compreensão das mudanças.  

Se as alterações no código fizerem com que os testes falhem, os testes devem ser corrigidos no mesmo commit.  

Para mudanças significativas, divida seu trabalho em commits separados para facilitar a revisão. Isso ajuda a manter o histórico organizado e torna mais fácil identificar e entender cada alteração individualmente. De maneira opcional, caso ache interessante, utilize a estratégia de *squash commit* para mesclar commits relacionados e manter o histórico coerente e simplificado. Link de apoio para aplicar o squash commit: [Git Squash Commits – Squashing the Last N Commits into One Commit](https://www.freecodecamp.org/news/git-squash-commits/)

A mensagem do commit deve descrever claramente como o comportamento está mudando e o motivo da alteração. Evite descrições vagas como "corrige bug" ou "ajuste", bem como commits sem descrição. Orientamos utilizar o formato [Conventional Commits](https://www.conventionalcommits.org/pt-br/v1.0.0/) para garantir uniformidade e facilitar a compreensão do histórico de alterações.


#### Pull requests

Após concluir as alterações na sua branch, abra um Pull Request (PR) da branch, em seu fork, onde realizou a implementação, para a branch `master` do repositório principal. Isso permitirá que seu código seja revisado e mesclado ao repositório principal. Para isso, siga as diretrizes abaixo:  

1. Certifique-se de que sua branch está sincronizada com a branch principal (`master`). Caso necessário, faça um rebase ou merge antes de abrir o PR
2. Execute todos os testes antes de enviar a solicitação. Pull Requests que quebram testes automatizados não serão revisados nem aprovados
3. Verifique se o código segue as convenções de nomenclatura, formatação e boas práticas estabelecidas no repositório. 
4. Em seu PR, forneça uma descrição clara e objetiva sobre quais mudanças foram feitas e o motivo da alteração
5. Se houver uma issue vinculada à implementação do seu PR, faça a associação para facilitar o rastreamento
6. Caso haja alguma pendência, resolva-a antes de abrir o PR. Corrija conflitos, erros no pipeline e outras pendências antes de enviar a solicitação
7. Após a abertura do PR, aguarde a revisão. Verifique periodicamente se solicitações foram adicionadas pelos revisores. Caso haja, realize as correções necessárias e atualize o PR.


### Convenções de código, padrões e boas práticas

Para garantir clareza, consistência e qualidade no código, adote as seguintes diretrizes:

- Os nomes de métodos, atributos e classes auxiliares devem ser escritos em português;
- Os nomes das classes e atributos constantes no Manual de Orientação do Contribuinte devem ser escritos exatamente como constam na documentação, respeitando a diferenciação entre maiúsculas e minúsculas (case sensitive). Exemplo: se o atributo ou classe começar com letra minúscula, ele deve ser mantido dessa forma no código C#.
- Todas as classes devem conter um cabeçalho com a licença e os direitos de uso;
- Ao referenciar objetos ou métodos na documentação XML, utilize `<see cref="">`. Exemplo:
```csharp
/// <summary>
/// Obtém o certificado digital.
/// <para>
/// Se <see cref="ConfiguracaoCertificado.Arquivo"/> for informado, será usado o método <see cref="ObterDeArquivo(string,string)"/>.
/// Caso contrário, será usado <see cref="ObterDoRepositorio()"/>.
/// </para>
/// <para>Após o uso, invoque <see cref="X509Certificate2.Reset()"/>.</para>
/// </summary>
```
- Todas as classes, atributos e métodos devem ser documentados no formato XML. A documentação dos atributos e classes do projeto `*.Classes` deve incluir o código e a descrição conforme especificado no Manual de Orientação do Contribuinte, seguindo o padrão adotado no projeto `NFe.Classes`. Exemplo:
```csharp
/// <summary>
///     PR06 - Versão do Aplicativo que processou a consulta.
/// </summary>
public string verAplic { get; set; }
```
- Siga as convenções da Microsoft, conforme definido em:
  - [Regras e Convenções de Nomenclatura do Identificador C#](https://learn.microsoft.com/pt-br/dotnet/csharp/fundamentals/coding-style/identifier-names)
  - [Convenções Comuns de Código C#](https://learn.microsoft.com/pt-br/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Testes automatizados:
  - Implemente testes automatizados para validar fluxos positivos e cenários de falha;
  - Organize os testes de forma a refletir a estrutura do código, garantindo isolamento e reprodutibilidade;
  - Utilize o padrão de nomenclatura [Given-When-Then](https://martinfowler.com/bliki/GivenWhenThen.html).
- Princípios [S.O.L.I.D.](https://medium.com/desenvolvendo-com-paixao/o-que-%C3%A9-solid-o-guia-completo-para-voc%C3%AA-entender-os-5-princ%C3%ADpios-da-poo-2b937b3fc530):
  - Responsabilidade Única: cada classe ou método deve ter uma responsabilidade clara;
  - Aberto/Fechado: projete o sistema para permitir novas funcionalidades sem alterar o comportamento existente;
  - Substituição de Liskov: componentes derivados devem substituir os genéricos sem comprometer a integridade;
  - Segregação de Interfaces: mantenha interfaces focadas e específicas;
  - Inversão de Dependência: estruture dependências em abstrações, não em implementações concretas;
- [KISS](https://dev.to/suspir0n/kiss-mantenha-a-simplicidade-estupido-24lh) (Keep It Simple, Stupid): opte por soluções simples e diretas, evitando complexidade desnecessária;
- [DRY](https://medium.com/@rafaelsouzaim/n%C3%A3o-se-repita-dry-dont-repeat-yourself-40da33289bcf) (Don't Repeat Yourself): centralize a lógica repetitiva para um código mais sustentável e menos propenso a erros;
- Outras boas práticas:
  - Use nomes autoexplicativos;
  - Mantenha responsabilidades bem definidas em métodos e classes;
  - Escreva código de fácil leitura e manutenção, minimizando a necessidade de comentários para esclarecer a intenção.


## Licença

Ao contribuir para o projeto DFe.NET, você concorda que suas contribuições estarão sob a mesma [Licença do Projeto](LICENSE).