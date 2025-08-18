using System.ComponentModel;
using System.Xml.Serialization;

// dados vieram da tabela atual publicada aqui https://dfe-portal.svrs.rs.gov.br/DFE/TabelaClassificacaoTributaria
// atributo de description é preenchido com o atributo nome completo
namespace NFe.Classes.Informacoes.Detalhe.Tributacao
{
    public enum cClassTrib
    {
        [Description("Situações tributadas integralmente pelo IBS e CBS.")]
        [XmlEnum("000001")]
        ct000001 = 000001,

        [Description("Exploração de via, observado o art. 11 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("000002")]
        ct000002 = 000002,

        [Description("Regime automotivo - projetos incentivados, observado o art. 311 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("000003")]
        ct000003 = 000003,

        [Description("Regime automotivo - projetos incentivados, observado o art. 312 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("000004")]
        ct000004 = 000004,

        [Description("Operações do FGTS não realizadas pela Caixa Econômica Federal, observado o art. 212 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("010001")]
        ct010001 = 010001,

        [Description("Planos de assistência funerária, observado o art. 236 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("011001")]
        ct011001 = 011001,

        [Description("Planos de assistência à saúde, observado o art. 237 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("011002")]
        ct011002 = 011002,

        [Description("Intermediação de planos de assistência à saúde, observado o art. 240 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("011003")]
        ct011003 = 011003,

        [Description("Concursos e prognósticos, observado o art. 246 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("011004")]
        ct011004 = 011004,

        [Description("Planos de assistência à saúde de animais domésticos, observado o art. 243 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("011005")]
        ct011005 = 011005,

        [Description("Aquisições de máquinas, de aparelhos, de instrumentos, de equipamentos, de matérias-primas, de produtos intermediários e de materiais de embalagem realizadas entre empresas autorizadas a operar em zonas de processamento de exportação, observado o art. 103 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200001")]
        ct200001 = 200001,

        [Description("Fornecimento ou importação de tratores, máquinas e implementos agrícolas, destinados a produtor rural não contribuinte, e de veículos de transporte de carga destinados a transportador autônomo de carga pessoa física não contribuinte, observado o art. 110 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200002")]
        ct200002 = 200002,

        [Description("Vendas de produtos destinados à alimentação humana relacionados no Anexo I da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, que compõem a Cesta Básica Nacional de Alimentos, criada nos termos do art. 8º da Emenda Constitucional nº 132, de 20 de dezembro de 2023, observado o art. 125 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200003")]
        ct200003 = 200003,

        [Description("Venda de dispositivos médicos com a especificação das respectivas classificações da NCM/SH previstas no Anexo XII da Lei Complementar nº 214, de 2025, observado o art. 144 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200004")]
        ct200004 = 200004,

        [Description("Venda de dispositivos médicos com a especificação das respectivas classificações da NCM/SH previstas no Anexo IV da Lei Complementar nº 214, de 2025, quando adquiridos por órgãos da administração pública direta, autarquias e fundações públicas, observado o art. 144 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200005")]
        ct200005 = 200005,

        [Description("Situação de emergência de saúde pública reconhecida pelo Poder Legislativo federal, estadual, distrital ou municipal competente, ato conjunto do Ministro da Fazenda e do Comitê Gestor do IBS poderá ser editado, a qualquer momento, para incluir dispositivos não listados no Anexo XII da Lei Complementar nº 214, de 2025, limitada a vigência do benefício ao período e à localidade da emergência de saúde pública, observado o art. 144 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200006")]
        ct200006 = 200006,

        [Description(" Fornecimento dos dispositivos de acessibilidade próprios para pessoas com deficiência relacionados no Anexo XIII da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 145 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200007")]
        ct200007 = 200007,

        [Description("Fornecimento dos dispositivos de acessibilidade próprios para pessoas com deficiência relacionados no Anexo V da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, quando adquiridos por órgãos da administração pública direta, autarquias, fundações públicas e entidades imunes, observado o art. 145 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200008")]
        ct200008 = 200008,

        [Description("Fornecimento dos medicamentos relacionados no Anexo XIV da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 146 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200009")]
        ct200009 = 200009,

        [Description("Fornecimento dos medicamentos registrados na Anvisa, quando adquiridos por órgãos da administração pública direta, autarquias, fundações públicas e entidades imunes, observado o art. 146 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200010")]
        ct200010 = 200010,

        [Description(" Fornecimento das composições para nutrição enteral e parenteral, composições especiais e fórmulas nutricionais destinadas às pessoas com erros inatos do metabolismo relacionadas no Anexo VI da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, quando adquiridas por órgãos da administração pública direta, autarquias e fundações públicas, observado o art. 146 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200011")]
        ct200011 = 200011,

        [Description("Situação de emergência de saúde pública reconhecida pelo Poder Legislativo federal, estadual, distrital ou municipal competente, ato conjunto do Ministro da Fazenda e do Comitê Gestor do IBS poderá ser editado, a qualquer momento, para incluir dispositivos não listados no Anexo XIV da Lei Complementar nº 214, de 2025, limitada a vigência do benefício ao período e à localidade da emergência de saúde pública, observado o art. 146 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200012")]
        ct200012 = 200012,

        [Description("Fornecimento de tampões higiênicos, absorventes higiênicos internos ou externos, descartáveis ou reutilizáveis, calcinhas absorventes e coletores menstruais, observado o art. 147 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200013")]
        ct200013 = 200013,

        [Description("Fornecimento dos produtos hortícolas, frutas e ovos, relacionados no Anexo XV da Lei Complementar nº 214 , de 2025, com a especificação das respectivas classificações da NCM/SH e desde que não cozidos, observado o art. 148 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200014")]
        ct200014 = 200014,

        [Description("Venda de automóveis de passageiros de fabricação nacional de, no mínimo, 4 (quatro) portas, inclusive a de acesso ao bagageiro, quando adquiridos por motoristas profissionais que exerçam, comprovadamente, em automóvel de sua propriedade, atividade de condutor autônomo de passageiros, na condição de titular de autorização, permissão ou concessão do poder público, e que destinem o automóvel à utilização na categoria de aluguel (táxi), ou por pessoas com deficiência física, visual, auditiva, deficiência mental severa ou profunda, transtorno do espectro autista, com prejuízos na comunicação social e em padrões restritos ou repetitivos de comportamento de nível moderado ou grave, nos termos da legislação relativa à matéria, observado o disposto no art. 149 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200015")]
        ct200015 = 200015,

        [Description("Prestação de serviços de pesquisa e desenvolvimento por Instituição Científica, Tecnológica e de Inovação (ICT) sem fins lucrativos para a administração pública direta, autarquias e fundações públicas ou para o contribuinte sujeito ao regime regular do IBS e da CBS, observado o disposto no art. 156 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200016")]
        ct200016 = 200016,

        [Description("Operações relacionadas ao FGTS, considerando aquelas necessárias à aplicação da Lei nº 8.036, de 1990, realizadas pelo Conselho Curador ou Secretaria Executiva do FGTS, observado o art. 212 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200017")]
        ct200017 = 200017,

        [Description("Operações de resseguro e retrocessão ficam sujeitas à incidência à alíquota zero, inclusive quando os prêmios de resseguro e retrocessão forem cedidos ao exterior, observado o art. 223 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200018")]
        ct200018 = 200018,

        [Description("Importador dos serviços financeiros seja contribuinte que realize as operações de que tratam os incisos I a V do caput do art. 182, será aplicada alíquota zero na importação, sem prejuízo da manutenção do direito de dedução dessas despesas da base de cálculo do IBS e da CBS, segundo, observado o art. 231 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200019")]
        ct200019 = 200019,

        [Description("Operação praticada por sociedades cooperativas optantes por regime específico do IBS e CBS, quando o associado destinar bem ou serviço à cooperativa de que participa, e a cooperativa fornecer bem ou serviço ao associado sujeito ao regime regular do IBS e da CBS, observado o art. 271 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200020")]
        ct200020 = 200020,

        [Description("Serviços de transporte público coletivo de passageiros ferroviário e hidroviário urbanos, semiurbanos e metropolitanos, observado o art. 285 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200021")]
        ct200021 = 200021,

        [Description("Operação originada fora da Zona Franca de Manaus que destine bem material industrializado de origem nacional a contribuinte estabelecido na Zona Franca de Manaus que seja habilitado nos termos do art. 442 da Lei Complementar nº 214, de 2025, e sujeito ao regime regular do IBS e da CBS ou optante pelo regime do Simples Nacional de que trata o art. 12 da Lei Complementar nº 123, de 2006, observado o art. 445 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200022")]
        ct200022 = 200022,

        [Description("Operação realizada por indústria incentivada que destine bem material intermediário para outra indústria incentivada na Zona Franca de Manaus, desde que a entrega ou disponibilização dos bens ocorra dentro da referida área, observado o art. 448 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200023")]
        ct200023 = 200023,

        [Description("Operação originada fora das Áreas de Livre Comércio que destine bem material industrializado de origem nacional a contribuinte estabelecido nas Áreas de Livre Comércio que seja habilitado nos termos do art. 456 da Lei Complementar nº 214, de 2025, e sujeito ao regime regular do IBS e da CBS ou optante pelo regime do Simples Nacional de que trata o art. 12 da Lei Complementar nº 123, de 2006, observado o art. 463 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200024")]
        ct200024 = 200024,

        [Description("Fornecimento dos serviços de educação relacionados ao Programa Universidade para Todos (Prouni), instituído pela Lei nº 11.096, de 13 de janeiro de 2005, observado o art. 308 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200025")]
        ct200025 = 200025,

        [Description("Locação de imóveis localizados nas zonas reabilitadas, pelo prazo de 5 (cinco) anos, contado da data de expedição do habite-se, e relacionados a projetos de reabilitação urbana de zonas históricas e de áreas críticas de recuperação e reconversão urbanística dos Municípios ou do Distrito Federal, a serem delimitadas por lei municipal ou distrital, observado o art. 158 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200026")]
        ct200026 = 200026,

        [Description("Operações de locação, cessão onerosa e arrendamento de bens imóveis, observado o art. 261 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200027")]
        ct200027 = 200027,

        [Description("Fornecimento dos serviços de educação relacionados no Anexo II da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da Nomenclatura Brasileira de Serviços, Intangíveis e Outras Operações que Produzam Variações no Patrimônio (NBS), observado o art. 129 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200028")]
        ct200028 = 200028,

        [Description("Fornecimento dos serviços de saúde humana relacionados no Anexo III da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NBS, observado o art. 130 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200029")]
        ct200029 = 200029,

        [Description("Venda dos dispositivos médicos relacionados no Anexo IV da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 131 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200030")]
        ct200030 = 200030,

        [Description("Fornecimento dos dispositivos de acessibilidade próprios para pessoas com deficiência relacionados no Anexo V da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 132 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200031")]
        ct200031 = 200031,

        [Description("Fornecimento dos medicamentos registrados na Anvisa ou produzidos por farmácias de manipulação, ressalvados os medicamentos sujeitos à alíquota zero de que trata o art. 141 da Lei Complementar nº 214, de 2025, observado o art. 133 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200032")]
        ct200032 = 200032,

        [Description("Fornecimento das composições para nutrição enteral e parenteral, composições especiais e fórmulas nutricionais destinadas às pessoas com erros inatos do metabolismo relacionadas no Anexo VI da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 133 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200033")]
        ct200033 = 200033,

        [Description("Fornecimento dos alimentos destinados ao consumo humano relacionados no Anexo VII da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 135 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200034")]
        ct200034 = 200034,

        [Description("Fornecimento dos produtos de higiene pessoal e limpeza relacionados no Anexo VIII da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH, observado o art. 136 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200035")]
        ct200035 = 200035,

        [Description("Fornecimento de produtos agropecuários, aquícolas, pesqueiros, florestais e extrativistas vegetais in natura, observado o art. 137 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200036")]
        ct200036 = 200036,

        [Description("Fornecimento de serviços ambientais de conservação ou recuperação da vegetação nativa, mesmo que fornecidos sob a forma de manejo sustentável de sistemas agrícolas, agroflorestais e agrossilvopastoris, em conformidade com as definições e requisitos da legislação específica, observado o art. 137 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200037")]
        ct200037 = 200037,

        [Description("Fornecimento dos insumos agropecuários e aquícolas relacionados no Anexo IX da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NCM/SH e da NBS, observado o art. 138 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200038")]
        ct200038 = 200038,

        [Description("Fornecimento dos serviços e o licenciamento ou cessão dos direitos relacionados no Anexo X da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NBS, quando destinados às seguintes produções nacionais artísticas, culturais, de eventos, jornalísticas e audiovisuais: espetáculos teatrais, circenses e de dança, shows musicais, desfiles carnavalescos ou folclóricos, eventos acadêmicos e científicos, como congressos, conferências e simpósios, feiras de negócios, exposições, feiras e mostras culturais, artísticas e literárias; programas de auditório ou jornalísticos, filmes, documentários, séries, novelas, entrevistas e clipes musicais, observado o art. 139 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200039")]
        ct200039 = 200039,

        [Description("Fornec dos seguintes serv de comunic instit à admin púb direta, autarq e fund púb: serviços direcionados ao planej, criação, programação e manutenção de páginas eletrônicas da admin pública, ao monitor e gestão de suas redes sociais e à otimização de páginas e canais digitais para mecanismos de buscas e produção de mensagens, infográficos, painéis interativos e conteúdo institucional, serviços de relações com a imprensa, que reúnem estrat org para promover e reforçar a comunicação dos órgãos e das entidades contratantes com seus públicos de interesse, por meio da interação com prof da imprensa, e serviços de relações públicas, que compreendem o esforço de comunic planej, coeso e contínuo que tem por obj estab adequada percepção da atuação e dos obj instituc, a partir do estímulo à compreensão mútua e da manut de padrões de relac e fluxos de inf entre os órgãos e as entidades contrat e seus públicos de interesse, no País e no exterior, obs o art. 140 da Lei Compl nº 214, de 2025")]
        [XmlEnum("200040")]
        ct200040 = 200040,

        [Description("Operações relacionadas às seguintes atividades desportivas: fornecimento de serviço de educação desportiva, classificado no código 1.2205.12.00 da NBS, e gestão e exploração do desporto por associações e clubes esportivos filiados ao órgão estadual ou federal responsável pela coordenação dos desportos, inclusive por meio de venda de ingressos para eventos desportivos, fornecimento oneroso ou não de bens e serviços, inclusive ingressos, por meio de programas de sócio-torcedor, cessão dos direitos desportivos dos atletas e transferência de atletas para outra entidade desportiva ou seu retorno à atividade em outra entidade desportiva, observado o art. 141 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200041")]
        ct200041 = 200041,

        [Description("Operações relacionadas ao fornecimento de serviço de educação desportiva, classificado no código 1.2205.12.00 da NBS, observado o art. 141 da Lei Complementar nº 214, de 2025. Operações relacionadas às seguintes atividades desportivas: operações e prestações de serviços de segurança da informação e segurança cibernética, observado o art. 141 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200042")]
        ct200042 = 200042,

        [Description("Fornecimento à administração pública direta, autarquias e fundações púbicas dos serviços e dos bens relativos à soberania e à segurança nacional, à segurança da informação e à segurança cibernética relacionados no Anexo XI da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NBS e da NCM/SH, observado o art. 142 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200043")]
        ct200043 = 200043,

        [Description("Operações e prestações de serviços de segurança da informação e segurança cibernética desenvolvidos por sociedade que tenha sócio brasileiro com o mínimo de 20% (vinte por cento) do seu capital social, relacionados no Anexo XI da Lei Complementar nº 214, de 2025, com a especificação das respectivas classificações da NBS e da NCM/SH, observado o art. 142 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200044")]
        ct200044 = 200044,

        [Description("Operações relacionadas a projetos de reabilitação urbana de zonas históricas e de áreas críticas de recuperação e reconversão urbanística dos Municípios ou do Distrito Federal, a serem delimitadas por lei municipal ou distrital, observado o art. 158 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200045")]
        ct200045 = 200045,

        [Description("Operações com bens imóveis, observado o art. 261 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200046")]
        ct200046 = 200046,

        [Description("Bares e Restaurantes, observado o art. 275 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200047")]
        ct200047 = 200047,

        [Description("Hotelaria, Parques de Diversão e Parques Temáticos, observado o art. 281 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200048")]
        ct200048 = 200048,

        [Description("Transporte coletivo de passageiros rodoviário, ferroviário e hidroviário intermunicipais e interestaduais, observado o art. 286 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200049")]
        ct200049 = 200049,

        [Description("Agências de Turismo, observado o art. 289 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200051")]
        ct200051 = 200051,

        [Description("Prestação de serviços das seguintes profissões intelectuais de natureza científica, literária ou artística, submetidas à fiscalização por conselho profissional: administradores, advogados, arquitetos e urbanistas, assistentes sociais, bibliotecários, biólogos, contabilistas, economistas, economistas domésticos, profissionais de educação física, engenheiros e agrônomos, estatísticos, médicos veterinários e zootecnistas, museólogos, químicos, profissionais de relações públicas, técnicos industriais e técnicos agrícolas, observado o art. 127 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200052")]
        ct200052 = 200052,

        [Description("Serviços de transporte aéreo regional coletivo de passageiros ou de carga, observado o art. 287 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("200450")]
        ct200450 = 200450,

        [Description("Redutor social aplicado uma única vez na alienação de bem imóvel residencial novo, observado o art. 259 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("210001")]
        ct210001 = 210001,

        [Description("Redutor social aplicado uma única vez na alienação de lote residencial, observado o art. 259 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("210002")]
        ct210002 = 210002,

        [Description("Redutor social em operações de locação, cessão onerosa e arrendamento de bens imóveis de uso residencial, observado o art. 260 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("210003")]
        ct210003 = 210003,

        [Description("Incorporação imobiliária submetida ao regime especial de tributação, observado o art. 485 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("220001")]
        ct220001 = 220001,

        [Description("Incorporação imobiliária submetida ao regime especial de tributação, observado o art. 485 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("220002")]
        ct220002 = 220002,

        [Description("Alienação de imóvel decorrente de parcelamento do solo, observado o art. 486 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("220003")]
        ct220003 = 220003,

        [Description("Locação, cessão onerosa ou arrendamento de bem imóvel com alíquota sobre a receita bruta, observado o art. 487 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("221001")]
        ct221001 = 221001,

        [Description("Transporte internacional de passageiros, caso os trechos de ida e volta sejam vendidos em conjunto, a base de cálculo será a metade do valor cobrado, observado o Art. 12 § 8º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("222001")]
        ct222001 = 222001,

        [Description("Fornecimento de serviços de transporte público coletivo de passageiros rodoviário e metroviário de caráter urbano, semiurbano e metropolitano, sob regime de autorização, permissão ou concessão pública, observado o art. 157 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("400001")]
        ct400001 = 400001,

        [Description("Fornecimento de bonificações quando constem do respectivo documento fiscal e que não dependam de evento posterior, observado o art. 5º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410001")]
        ct410001 = 410001,

        [Description("Transferências entre estabelecimentos pertencentes ao mesmo contribuinte, observado o art. 6º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410002")]
        ct410002 = 410002,

        [Description("Doações, observado o art. 6º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410003")]
        ct410003 = 410003,

        [Description("Exportações de bens e serviços, observado o art. 8º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410004")]
        ct410004 = 410004,

        [Description("Fornecimentos realizados pela União, pelos Estados, pelo Distrito Federal e pelos Municípios, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410005")]
        ct410005 = 410005,

        [Description("Fornecimentos realizados por entidades religiosas e templos de qualquer culto, inclusive suas organizações assistenciais e beneficentes, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410006")]
        ct410006 = 410006,

        [Description("Fornecimentos realizados por partidos políticos, inclusive suas fundações, entidades sindicais dos trabalhadores e instituições de educação e de assistência social, sem fins lucrativos, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410007")]
        ct410007 = 410007,

        [Description("Fornecimentos de livros, jornais, periódicos e do papel destinado a sua impressão, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410008")]
        ct410008 = 410008,

        [Description("Fornecimentos de fonogramas e videofonogramas musicais produzidos no Brasil contendo obras musicais ou literomusicais de autores brasileiros e/ou obras em geral interpretadas por artistas brasileiros, bem como os suportes materiais ou arquivos digitais que os contenham, salvo na etapa de replicação industrial de mídias ópticas de leitura a laser, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410009")]
        ct410009 = 410009,

        [Description("Fornecimentos de serviço de comunicação nas modalidades de radiodifusão sonora e de sons e imagens de recepção livre e gratuita, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410010")]
        ct410010 = 410010,

        [Description("Fornecimentos de ouro, quando definido em lei como ativo financeiro ou instrumento cambial, observado o art. 9º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410011")]
        ct410011 = 410011,

        [Description("Fornecimento de condomínio edilício não optante pelo regime regular, observado o art. 26 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410012")]
        ct410012 = 410012,

        [Description("Exportações de combustíveis, observado o art. 98 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410013")]
        ct410013 = 410013,

        [Description("Fornecimento de produtor rural não contribuinte, observado o art. 164 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410014")]
        ct410014 = 410014,

        [Description("Fornecimento por transportador autônomo não contribuinte, observado o art. 169 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410015")]
        ct410015 = 410015,

        [Description("Fornecimento ou aquisição de resíduos sólidos, observado o art. 170 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410016")]
        ct410016 = 410016,

        [Description("Aquisição de bem móvel com crédito presumido sob condição de revenda realizada, observado o art. 171 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410017")]
        ct410017 = 410017,

        [Description("Operações relacionadas aos fundos garantidores e executores de políticas públicas, inclusive de habitação, previstos em lei, assim entendidas os serviços prestados ao fundo pelo seu agente operador e por entidade encarregada da sua administração, observado o art. 213 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410018")]
        ct410018 = 410018,

        [Description("Exclusão da gorjeta na base de cálculo no fornecimento de alimentação, observado o art. 274 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410019")]
        ct410019 = 410019,

        [Description("Exclusão do valor de intermediação na base de cálculo no fornecimento de alimentação, observado o art. 274 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410020")]
        ct410020 = 410020,

        [Description("Contribuição de que trata o art. 149-A da Constituição Federal, observado o art. 12 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410021")]
        ct410021 = 410021,

        [Description("Operações não onerosas sem previsão de tributação, não especificadas anteriormente, observado o art. 4º da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("410099")]
        ct410099 = 410099,

        [Description("Operações, sujeitas a diferimento, com energia elétrica ou com direitos a ela relacionados, relativas à geração, comercialização, distribuição e transmissão, observado o art. 28 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("510001")]
        ct510001 = 510001,

        [Description("Operações, sujeitas a diferimento, com insumos agropecuários e aquícolas destinados a produtor rural contribuinte, observado o art. 138 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("510002")]
        ct510002 = 510002,

        [Description("Exportações de bens materiais, observado o art. 82 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550001")]
        ct550001 = 550001,

        [Description("Regime de Trânsito, observado o art. 84 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550002")]
        ct550002 = 550002,

        [Description("Regimes de Depósito, observado o art. 85 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550003")]
        ct550003 = 550003,

        [Description("Regimes de Depósito, observado o art. 87 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550004")]
        ct550004 = 550004,

        [Description("Regimes de Depósito, observado o art. 87 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550005")]
        ct550005 = 550005,

        [Description("Regimes de Permanência Temporária, observado o art. 88 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550006")]
        ct550006 = 550006,

        [Description("Regimes de Aperfeiçoamento, observado o art. 90 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550007")]
        ct550007 = 550007,

        [Description("Importação de bens para o Regime de Repetro-Temporário, de que tratam o inciso I do art. 93 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550008")]
        ct550008 = 550008,

        [Description("GNL-Temporário, de que trata o inciso II do art. 93 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550009")]
        ct550009 = 550009,

        [Description("Repetro-Permanente, de que trata o inciso III do art. 93 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550010")]
        ct550010 = 550010,

        [Description("Repetro-Industrialização, de que trata o inciso IV do art. 93 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550011")]
        ct550011 = 550011,

        [Description("Repetro-Nacional, de que trata o inciso V do art. 93 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550012")]
        ct550012 = 550012,

        [Description("Repetro-Entreposto, de que trata o inciso VI do art. 93 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550013")]
        ct550013 = 550013,

        [Description("Zona de Processamento de Exportação, observado os arts. 99, 100 e 102 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550014")]
        ct550014 = 550014,

        [Description("Regime Tributário para Incentivo à Modernização e à Ampliação da Estrutura Portuária - Reporto, observado o art. 105 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550015")]
        ct550015 = 550015,

        [Description("Regime Especial de Incentivos para o Desenvolvimento da Infraestrutura - Reidi, observado o art. 106 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550016")]
        ct550016 = 550016,

        [Description("Regime Tributário para Incentivo à Atividade Econômica Naval – Renaval, observado o art. 107 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550017")]
        ct550017 = 550017,

        [Description("Desoneração da aquisição de bens de capital, observado o art. 109 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550018")]
        ct550018 = 550018,

        [Description("Importação de bem material por indústria incentivada para utilização na Zona Franca de Manaus, observado o art. 443 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550019")]
        ct550019 = 550019,

        [Description("Áreas de livre comércio, observado o art. 461 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("550020")]
        ct550020 = 550020,

        [Description("Tributação monofásica sobre combustíveis, observados os art. 172 e art. 179 I da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("620001")]
        ct620001 = 620001,

        [Description("Tributação monofásica com responsabilidade pela retenção sobre combustíveis, observado o art. 178 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("620002")]
        ct620002 = 620002,

        [Description("Tributação monofásica com tributos retidos por responsabilidade sobre combustíveis, observado o art. 178 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("620003")]
        ct620003 = 620003,

        [Description("Tributação monofásica sobre mistura de EAC com gasolina A em percentual superior ou inferior ao obrigatório, observado o art. 179 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("620004")]
        ct620004 = 620004,

        [Description("Tributação monofásica sobre mistura de EAC com gasolina A em percentual superior ou inferior ao obrigatório, observado o art. 179 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("620005")]
        ct620005 = 620005,

        [Description("Tributação monofásica sobre combustíveis cobrada anteriormente, observador o art. 180 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("620006")]
        ct620006 = 620006,

        [Description("Fusão, cisão ou incorporação, observado o art. 55 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("800001")]
        ct800001 = 800001,

        [Description("Transferência de crédito do associado, inclusive as cooperativas singulares, para cooperativa de que participa das operações antecedentes às operações em que fornece bens e serviços e os créditos presumidos, observado o art. 272 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("800002")]
        ct800002 = 800002,

        [Description("Crédito presumido sobre o valor apurado nos fornecimentos a partir da Zona Franca de Manaus, observado o art. 450 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("810001")]
        ct810001 = 810001,

        [Description(" Documento com informações de fornecimento de serviços de planos de assinstência à saúde, mas com tributação realizada por outro meio, observado o art. 235 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("820001")]
        ct820001 = 820001,

        [Description("Documento com informações de fornecimento de serviços de planos de assinstência funerária, mas com tributação realizada por outro meio, observado o art. 236 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("820002")]
        ct820002 = 820002,

        [Description("Documento com informações de fornecimento de serviços de planos de assinstência à saúde de animais domésticos, mas com tributação realizada por outro meio, observado o art. 243 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("820003")]
        ct820003 = 820003,

        [Description("Documento com informações de prestação de serviços de consursos de prognósticos, mas com tributação realizada por outro meio, observado o art. 248 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("820004")]
        ct820004 = 820004,

        [Description("Documento com informações de alienação de bens imóveis, mas com tributação realizada por outro meio,, observado o art. 254 da Lei Complementar nº 214, de 2025.")]
        [XmlEnum("820005")]
        ct820005 = 820005,

        [Description("Documento com exclusão da base de cálculo da CBS e do IBS refrente à energia elétrica fornecida pela distribuidora à unidade consumidora, conforme Art 28, parágrafos 3° e 4°.")]
        [XmlEnum("830001")]
        ct830001 = 830001,
    }
}