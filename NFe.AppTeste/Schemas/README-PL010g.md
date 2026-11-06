# Schemas NF-e PL_010g v1.03

Os arquivos abaixo foram atualizados a partir do pacote oficial publicado
em 26/10/2026 no Portal da NF-e:

- `DFeTiposBasicos_v1.00.xsd`
- `leiauteNFe_v4.00.xsd`
- `nfe_v4.00.xsd`
- `tiposBasico_v4.00.xsd`
- `xmldsig-core-schema_v1.01.xsd`
- `e110001_v1.00.xsd`
- `e110300_v1.00.xsd`
- `tiposBasico_v1.03.xsd`

[Download do pacote oficial](https://www.nfe.fazenda.gov.br/portal/exibirArquivo.aspx?conteudo=EQhOQMgipXY%3D).
SHA-256 do ZIP: `b329ecbe7e4c05a41bfbc1527f105defb7047328acfeada7c77e1666da485eaf`.

O pacote é incremental. Os arquivos de `NFe/` e `Evento/` foram reunidos
nesta pasta para manter a resolução dos includes utilizada pela biblioteca,
preservando os demais schemas de serviços e eventos.

## Compatibilidade com os eventos existentes

O novo `tiposBasico_v1.03.xsd` declara `TCOrgaoIBGE`, nome também declarado
localmente pelos layouts de manifestação, EPEC, remessa para industrialização
e Suframa. Para evitar a definição duplicada, somente o nome desses tipos
locais e suas referências foram alterados:

| Layout | Nome local |
|---|---|
| `leiauteConfRecebto_v1.00.xsd` | `TCOrgaoIBGEConfRecebto` |
| `leiauteEPEC_v1.00.xsd` | `TCOrgaoIBGEEPEC` |
| `leiauteRemIndus_v1.00.xsd` | `TCOrgaoIBGERemIndus` |
| `leiauteSuframaInternaliza_v1.00.xsd` | `TCOrgaoIBGESuframaInternaliza` |
| `leiauteSuframaVistoria_v1.00.xsd` | `TCOrgaoIBGESuframaVistoria` |

As restrições, enumerações e os elementos dos XMLs permanecem iguais.
Os oito arquivos do pacote oficial não receberam essa adaptação.

## Validação

Os testes de `NFe.Classes.Testes` utilizam os XSDs desta pasta para validar
o total zero, os grupos ad rem de IBS/CBS retidos anteriormente e a compilação
dos schemas de serviços e eventos afetados.

A atualização do pacote de schemas não equivale à implementação de todos os
novos grupos do leiaute. A extensão do modelo de monofasia desta contribuição
cobre especificamente a retenção anterior ad rem; os modelos legados continuam
disponíveis para leitura de documentos anteriores.
