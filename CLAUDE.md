# CLAUDE.md - Guia de Navegação do Projeto DFe.NET

## Visão Geral

Biblioteca .NET para emissão e manipulação de Documentos Fiscais Eletrônicos brasileiros (NFe, NFCe, CTe, MDFe).
Fork do projeto open-source [ZeusFiscal](https://github.com/ZeusAutomacao/DFe.NET) (LGPL 2.1+), mantido pela Cosmos Pro.

Publicada como pacotes NuGet:
- `Cosmospro.Net.NFe.NFCe`
- `Cosmospro.Net.CTe`
- `Cosmospro.Net.MDFe`

## Estrutura do Repositório

```
DFe.NET/
├── DFe.Classes/              # Modelos base compartilhados (entidades, flags, enums)
├── DFe.Utils/                # Utilitários compartilhados (assinatura digital, XML, certificado)
├── DFe.Wsdl/                 # Camada HTTP/SOAP (IRequestSefaz, handlers)
│
├── NFe.Classes/              # Modelos NFe/NFCe (informações, tributos, serviços)
├── NFe.Utils/                # Config, endereços SEFAZ, validação NFe
├── NFe.Servicos/             # Orquestrador NFe (ServicosNFe : IServicosNFe)
├── NFe.Wsdl/                 # Proxies SOAP NFe (.NET Framework)
├── NFe.Wsdl.Standard/        # Proxies SOAP NFe (.NET Standard)
│
├── CTe.Classes/              # Modelos CTe
├── CTe.Utils/                # Utilitários CTe
├── CTe.Servicos/             # Serviços CTe (IServicosCTe)
├── CTe.Wsdl/                 # Proxies SOAP CTe
│
├── MDFe.Classes/             # Modelos MDFe
├── MDFe.Utils/               # Config MDFe
├── MDFe.Servicos/            # Serviços MDFe (IServicosMDFe)
├── MDFe.Wsdl/                # Proxies SOAP MDFe
│
├── NFe.Danfe.*/              # Renderização DANFE (Base, Fast, OpenFast, Html, QuestPdf, PdfClown, Nativo)
├── CTe.Dacte.*/              # Renderização DACTE (Base, Fast, OpenFast)
├── MDFe.Damdfe.*/            # Renderização DAMDFE (Base, Fast, OpenFast)
│
├── Shared.NFe.Wsdl/          # Interfaces compartilhadas WSDL
├── Shared.NFe.Danfe/         # Utilitários DANFE compartilhados
│
├── DFe.Testes/               # Testes unitários principais (MSTest, net6.0)
├── NFe.Classes.Testes/       # Testes de modelos NFe (MSTest, net6.0)
├── NFe.Utils.Testes/         # Testes de utilitários NFe (xUnit, net6.0)
├── DadosDeTestes/             # Dados e fixtures compartilhados para testes
│
├── *.AppTeste/               # Apps demo WPF (.NET Framework 4.8)
├── *.AppTeste.NetCore/       # Apps demo Console (.NET 6.0)
│
├── NuGet/                    # Projetos de empacotamento NuGet
│   ├── Cosmospro.Net.NFe.NFCe/
│   ├── Cosmospro.Net.CTe/
│   └── Cosmospro.Net.MDFe/
│
└── docs/                     # Documentação adicional
```

## Arquitetura em Camadas

```
Classes (modelos/entidades) → Utils (config, validação) → Servicos (orquestração) → Wsdl (comunicação HTTP/SOAP)
```

Detalhes de interfaces, mocking e fluxo HTTP: ver [AGENTS.md](AGENTS.md).

## Build e Testes

```bash
# Solução completa
dotnet build "Zeus NFe.sln" -c Release

# Projetos individuais (base)
dotnet build DFe.Classes/DFe.Classes.csproj
dotnet build DFe.Utils/DFe.Utils.csproj
dotnet build DFe.Wsdl/DFe.Wsdl.csproj

# Testes
dotnet test DFe.Testes/DFe.Testes.csproj
dotnet test NFe.Classes.Testes/NFe.Classes.Testes.csproj
dotnet test NFe.Utils.Testes/NFe.Utils.Testes.csproj
```

## Target Frameworks

- .NET Framework 4.6.2+
- .NET Standard 2.0
- .NET 6.0+

Projetos de teste usam apenas `net6.0`.

## Convenções de Código

- **Idioma:** Nomes em português (padrão da legislação fiscal brasileira)
- **Nomes SEFAZ:** Classes e atributos do Manual de Orientação do Contribuinte devem manter o case exato da documentação oficial (ex: `verAplic`, `indDoacao`)
- **Testes:** Padrão Given-When-Then para nomenclatura
- **Frameworks de teste:** MSTest (DFe.Testes, NFe.Classes.Testes) e xUnit (NFe.Utils.Testes)
- **Serialização:** `System.Xml.Serialization` para XML
- **Assinatura digital:** `System.Security.Cryptography.Xml.SignedXml`

Regras completas: ver [CONTRIBUTING.md](CONTRIBUTING.md).

## Documentação Relacionada

| Arquivo | Conteúdo |
|---------|----------|
| [README.md](README.md) | Visão geral, NuGet, frameworks suportados, setup Linux |
| [CONTRIBUTING.md](CONTRIBUTING.md) | Guia de contribuição, convenções de código, fluxo de PR |
| [AGENTS.md](AGENTS.md) | Arquitetura detalhada, interfaces para mock, fluxo HTTP |
| [docs/](docs/) | Documentação adicional (notas técnicas, guias) |
