# NFe.Utils.CpfCnpj

Projeto aditivo que preenche o destinatario de NF-e/NFC-e (`dest` e `enderDest`) a
partir de um CPF ou CNPJ, consultando a API REST da cpfcnpj.com.br. Foco em NFC-e e
destinatario consumidor final.

Nada no nucleo do DFe.NET e alterado: o resolver apenas instancia/preenche os POCOs
existentes (`NFe.Classes.Informacoes.Destinatario`).

## Uso

```csharp
using NFe.Classes.Informacoes.Destinatario;
using NFe.Utils.CpfCnpj;

var lookup = new CpfCnpjComBrLookup("SEU_TOKEN");
var resolver = new DestinatarioResolver(lookup);

var destinatario = new dest(VersaoServico.Versao400);
await resolver.PreencherAsync(destinatario, "111.444.777-35");
// destinatario.CPF, xNome, enderDest.* e indIEDest ja preenchidos
```

Para destinatario contribuinte de ICMS, habilite a consulta de inscricao estadual:

```csharp
await resolver.PreencherAsync(destinatario, "06.990.590/0001-23",
    new DestinatarioResolverOpcoes { ResolverInscricaoEstadual = true });
// indIEDest = ContribuinteICMS (1) e IE preenchida, quando ha inscricao ATIVA na UF
```

## Garantias

- Define somente CPF **ou** CNPJ (nunca ambos), respeitando o setter de `dest`.
- Preenche apenas campos vazios; nunca sobrescreve dados informados pelo integrador.
- `enderDest.CEP` so e atribuido quando ha 8 digitos validos.
- `indIEDest` recebe `NaoContribuinte (9)` por padrao; `ContribuinteICMS (1)` apenas
  quando uma inscricao estadual **ativa** e localizada na UF do destinatario. Nunca `Isento`.

## Fonte customizada

`IPessoaLookup` permite trocar a origem dos dados (cache, mock de testes ou outra API)
sem alterar o preenchimento.

## Licenca

Contribuicao sob LGPL-2.1, a mesma licenca do DFe.NET.
