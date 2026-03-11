# AGENTS.md - DFe.NET (Cosmos Pro)

## Visão Geral

Biblioteca .NET para emissão de Documentos Fiscais Eletrônicos brasileiros (NFe, NFCe, CTe, MDFe).
Baseada no projeto open-source [ZeusFiscal](https://github.com/ZeusAutomacao/DFe.NET) (LGPL 2.1+).

Consumida como NuGet package pelos projetos **Cosmos Pro Edoc Facil** e **Gerenciador de Documento Fiscal Eletrônico**.

## Arquitetura do Projeto

```
DFe.NET/
├── DFe.Classes/          # Modelos compartilhados (entidades, flags, enums)
├── DFe.Utils/            # Utilitários compartilhados (assinatura, XML, certificado)
├── DFe.Wsdl/             # Camada HTTP/SOAP (IRequestSefaz, handlers)
│
├── NFe.Classes/          # Modelos NFe/NFCe
├── NFe.Utils/            # Config, endereços SEFAZ, validação, extensões NFe
├── NFe.Servicos/         # Orquestrador NFe (ServicosNFe : IServicosNFe)
├── NFe.Wsdl/             # Proxies SOAP NFe (SoapHttpClientProtocol)
├── NFe.Wsdl.Standard/    # Proxies SOAP NFe (.NET Standard)
│
├── CTe.Classes/          # Modelos CTe
├── CTe.Servicos/         # Serviços CTe (sem interface fachada consolidada)
├── CTe.Wsdl/             # Proxies SOAP CTe
│
├── MDFe.Classes/         # Modelos MDFe
├── MDFe.Utils/           # Config MDFe
├── MDFe.Servicos/        # Serviços MDFe (parcialmente com interfaces)
├── MDFe.Wsdl/            # Proxies SOAP MDFe
│
├── Shared.NFe.Wsdl/      # Interfaces compartilhadas WSDL (INfeServico)
├── Shared.DFe.Utils/     # Utilitários XML compartilhados
│
├── NuGet/                # Projetos de empacotamento NuGet
│   ├── Cosmospro.Net.NFe.NFCe/
│   ├── Cosmospro.Net.CTe/
│   └── Cosmospro.Net.MDFe/
│
├── [Módulo].Danfe.*/     # Geração de relatórios (DANFE, DACTE, DAMDFE)
├── [Módulo].AppTeste*/   # Aplicações de teste/demo (WPF, Console)
└── DFe.Testes/           # Testes unitários (xUnit)
```

## Fluxo de Comunicação HTTP

```
ServicosNFe / CTe / MDFe (orquestrador)
  └─> ServicoNfeFactory / WsdlFactory (cria proxy WSDL)
        └─> ConfiguracaoServicoWSDL.GetRequestSefaz() (factory de handler HTTP)
              └─> IRequestSefaz (interface)
                    ├── RequestSefazDefault      (HttpWebRequest - legado)
                    └── RequestSefazHttpClientHandler (HttpClient - moderno)
```

### Resolução de URLs SEFAZ

- NFe: `NFe.Utils/Enderecos/Enderecador.cs` → lista estática com todas as URLs por Estado/Ambiente/Versão
- CTe: `CTe.Servicos/Enderecos/UrlCTe.cs`
- MDFe: `MDFe.Servicos/Enderecos/UrlMDFe.cs`
- Override: `ConfiguracaoServicoWSDL.ResolverUrl()` permite redirecionar para WireMock em testes

## Configuração

- **NFe:** `ConfiguracaoServico` (singleton via `Instancia`) em `NFe.Utils/ConfiguracaoServico.cs`
- **CTe:** `ConfiguracaoServico` em `CTe.Classes/ConfiguracaoServico.cs`
- **MDFe:** `MDFeConfiguracao` em `MDFe.Utils/Configuracoes/MDFeConfiguracao.cs`
- **WSDL:** `ConfiguracaoServicoWSDL` (estática) em `DFe.Wsdl/Common/ConfiguracaoServicoWSDL.cs`

## Interfaces Disponíveis para Mock

| Interface | Implementação | Projeto |
|-----------|--------------|---------|
| `IServicosNFe` | `ServicosNFe` | NFe.Servicos |
| `IServicosCTe` | Classes CTe.Servicos | CTe.Servicos |
| `IServicosMDFe` | Classes MDFe.Servicos | MDFe.Servicos |
| `IRequestSefaz` | `RequestSefazDefault`, `RequestSefazHttpClientHandler` | DFe.Wsdl |
| `IEnderecoServicoProvider` | `EnderecoServicoProviderDefault`, `EnderecoServicoProviderOverride` | DFe.Classes |

## Target Frameworks

- .NET Framework 4.6.2+
- .NET Standard 2.0
- .NET 6.0+

## Convenções

- Nomes em português (padrão da legislação fiscal brasileira)
- Extension methods em `Ext*.cs` (sendo migradas para métodos de interface)
- Enums refletem códigos da SEFAZ (ex: `Estado`, `TipoAmbiente`, `ModeloDocumento`)
- XML Serialization via `System.Xml.Serialization`
- Assinatura digital via `System.Security.Cryptography.Xml.SignedXml`

## Decisões de Modernização (Cosmos Pro)

1. **IServicosNFe** criada para permitir mock de `ServicosNFe` em testes unitários
2. **Assina()/Valida()** migradas de extension methods para métodos da interface
3. **IEnderecoServicoProvider** para sobrepor URLs SEFAZ com WireMock
4. **Async real** nos handlers HTTP (eliminar `.Result` e `Task.FromResult`)
5. **IServicosCTe/IServicosMDFe** para completar cobertura de interfaces mockáveis
