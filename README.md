Transforme-se | Gerando Falcões

📋 Resumo Executivo

O Transforme-se é uma solução tecnológica robusta e escalável desenvolvida para a Gerando Falcões, um ecossistema de desenvolvimento social. O propósito central deste projeto é eliminar barreiras burocráticas na gestão educacional da ONG, substituindo processos manuais descentralizados (planilhas e formulários isolados) por uma Fonte Única da Verdade.

O sistema centraliza a captação, triagem e gestão de milhares de alunos em múltiplas unidades, garantindo a integridade dos dados, conformidade com a LGPD e eficiência operacional. Mais do que software, esta é uma ferramenta estratégica para escalar o impacto social e permitir que a ONG foque em sua missão principal: transformar vidas através da educação.

🏗️ Arquitetura e Stack Tecnológica

O projeto segue uma arquitetura multicamadas (N-Tier) com tendências de Clean Architecture, visando baixo acoplamento e alta testabilidade.

Backend & Core

Framework: ![Static Badge](https://img.shields.io/badge/.NET-8.0-blueviolet)  

Linguagem: ![Static Badge](https://img.shields.io/badge/C%23-8.0-blueviolet)  

ORM: ![Static Badge](https://img.shields.io/badge/Entity%20Framework%20Core-8.0-blueviolet)  

Autenticação/Autorização: ![Static Badge](https://img.shields.io/badge/ASP.NET%20Core%20Identity-8.0-blueviolet)

API: ![Static Badge](https://img.shields.io/badge/RESTful%20API-8.0-blueviolet) ![Static Badge](https://img.shields.io/badge/Swagger-8.0-blueviolet) ![Static Badge](https://img.shields.io/badge/Asana-8.0-blueviolet) para integração entre módulos e futuros clientes móveis.

Frontend & Interface

Web: ![Static Badge](https://img.shields.io/badge/ASP.NET%20Core%20MVC-8.0-blueviolet)

Estilização: ![Static Badge](https://img.shields.io/badge/Bootstrap-5-blueviolet)

Scripting: ![Static Badge](https://img.shields.io/badge/JavaScript-8.0-blueviolet) para validações client-side e interações dinâmicas (ex: ViaCEP).

Desktop: ![Static Badge](https://img.shields.io/badge/Windows%20Forms-8.0-blueviolet) para dashboards administrativos locais de alta performance.

Dados & Infraestrutura

Banco de Dados: ![Static Badge](https://img.shields.io/badge/SQL%20Server-8.0-blueviolet)

Cloud: ![Static Badge](https://img.shields.io/badge/Microsoft%20Azure-8.0-blueviolet) (Hospedagem e Serviços Cognitivos).

Integrações: ![Static Badge](https://img.shields.io/badge/ViaCEP-8.0-blueviolet) (Endereçamento), ![Static Badge](https://img.shields.io/badge/Asana-8.0-blueviolet) (Gestão de fluxo de aprovação - Roadmap).

📂 Estrutura do Repositório

A organização do código reflete a separação de responsabilidades:

TransformeseSolution/
├── 📂 Transformese.Domain     # Núcleo do sistema (Entidades, Enums, Regras de Negócio)
│   ├── 📂 Entities            # ex: Candidato.cs, Curso.cs
│   └── 📂 Enums               # ex: StatusCandidato.cs
│
├── 📂 Transformese.Data       # Camada de Persistência
│   ├── 📄 ApplicationDbContext.cs
│   └── 📂 Repositories        # Abstração de acesso a dados
│
├── 📂 Transformese.Api        # Camada de Serviços (REST)
│   └── 📂 Controllers         # Endpoints para consumo externo/mobile
│
├── 📂 Transformese.Web        # Aplicação Web Principal (MVC)
│   ├── 📂 Controllers         # Orquestração de fluxo
│   ├── 📂 Views               # Interface do Usuário (Razor)
│   └── 📂 wwwroot             # Assets estáticos (CSS, JS, Imagens)
│
└── 📂 Transformese.Desktop    # Cliente Administrativo (Windows Forms)
    └── 📄 Dashboard.cs        # Visão gerencial local


🚀 Funcionalidades-Chave

👤 Módulo Público (Candidato)

Inscrição Inteligente: Formulário do tipo "Wizard" para cadastro simplificado.

Validações em Tempo Real: Verificação de CPF duplicado e preenchimento automático de endereço via CEP.

Feedback Imediato: Mensageria de sucesso e confirmação de recebimento.

🛡️ Módulo Administrativo (Backoffice)

Controle de Acesso (RBAC): Perfis distintos para Administradores (acesso total) e ONGs Parceiras (acesso regional).

Gestão de Cursos e Unidades: CRUD completo para manutenção da oferta educacional.

Dashboard Gerencial: Visualização macro dos inscritos e status dos processos seletivos.

⚙️ Guia de Instalação e Execução

Pré-requisitos

.NET 8.0 SDK

SQL Server (LocalDB ou Instância Docker)

Visual Studio 2022 ou VS Code

Configuração do Ambiente

Clonar o repositório:

git clone [https://github.com/milonesco/gerandofalcoes.git](https://github.com/milonesco/gerandofalcoes.git)
cd gerandofalcoes


Configurar Banco de Dados:
Edite o arquivo appsettings.json no projeto Transformese.Web (e Transformese.Api) com sua Connection String:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TransformeseDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
}


Executar Migrations:
No terminal, navegue até a pasta da solução e execute:

dotnet ef database update --project Transformese.Data --startup-project Transformese.Web


Executar a Aplicação:

dotnet run --project Transformese.Web


Acesse https://localhost:7000 (ou a porta indicada no console).

🤝 Como Contribuir

Este projeto adota um fluxo de trabalho colaborativo focado em qualidade.

Branching: Utilize o padrão feature/nome-da-feature ou fix/nome-do-bug a partir da branch develop.

Padrões de Código: Siga as convenções de nomenclatura C# (PascalCase para métodos/classes, camelCase para variáveis locais).

Commits: Utilize Conventional Commits (ex: feat: adiciona validação de cpf, fix: corrige layout mobile).

Pull Requests: Descreva claramente o objetivo do PR e garanta que o projeto compila sem erros antes de solicitar o review.

🔮 Roadmap

Visão estratégica para as próximas versões do produto:

[ ] Integração com Power BI: Conexão direta do SQL Server para relatórios demográficos avançados.

[ ] App Mobile: Desenvolvimento de aplicativo (MAUI ou React Native) para gamificação da jornada do aluno.

[ ] Hub do Aluno: Área logada para o estudante acompanhar notas e frequência.

[ ] Integração Asana: Automação de fluxo de trabalho para a equipe operacional.

🛡️ Boas Práticas e Considerações Técnicas

LGPD: O sistema foi desenhado considerando a privacidade desde a concepção (Privacy by Design). Dados sensíveis devem ser tratados com cautela.

Injeção de Dependência: O projeto utiliza extensivamente o contêiner de DI nativo do .NET para garantir desacoplamento.

Tratamento de Erros: Implementação de ErrorViewModel e páginas de erro amigáveis para evitar exposição de stack traces em produção.

📄 Licença e Time

Licença: Proprietária - Todos os direitos reservados à Gerando Falcões. O uso, cópia ou modificação não autorizada é proibido.

Time de Desenvolvimento:

Gabriel Milone: Líder Técnico / Backend

Maryana Oliveira: Frontend / Analista de Requisitos

Daniel Nascimento: Backend / QA / DBA