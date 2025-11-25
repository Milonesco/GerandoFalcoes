# TransformeseSolution  
Integração completa entre **API**, **MVC Web** e **Aplicação Desktop**, utilizando arquitetura em camadas para centralizar regras de negócio e acesso a dados.

---

## 📌 Status do Projeto  
![Static Badge](https://img.shields.io/badge/Status-Em%20Desenvolvimento-blue)  
![Static Badge](https://img.shields.io/badge/.NET-8.0-blueviolet)  
![Static Badge](https://img.shields.io/badge/Arquitetura-Camadas-green)  
![Static Badge](https://img.shields.io/badge/API-REST-orange)

---

## 📘 Visão Geral  
O projeto demonstra como integrar uma API central com clientes Web (MVC) e Desktop, mantendo uma arquitetura limpa e escalável. Toda manipulação de dados ocorre exclusivamente pela API. Clientes apenas consomem serviços.

---

## 🧱 Estrutura da Solução

```
TransformeseSolution.sln
│
├── Transformese.Api
│     └─ API REST (Controllers, Endpoints, Swagger, Services)
│
├── Transformese.Data
│     └─ Camada de dados (EF Core, AppDbContext, Migrations, Repositórios)
│
├── Transformese.Desktop
│     └─ Aplicação Desktop consumindo API
│
├── Transformese.Domain
│     └─ Entidades de domínio e regras de negócio
│
└── Transformese.MVC
      └─ Aplicação Web MVC consumindo API via HttpClient
```

---

## 🛠 Tecnologias Utilizadas

- .NET 7/8  
- ASP.NET Core Web API  
- ASP.NET MVC  
- Windows Forms / WPF  
- Entity Framework Core  
- SQL Server  
- Swagger / OpenAPI  
- HttpClient  
- Arquitetura em camadas

---

## 🚀 Como Executar o Projeto

### 1. Clone
```bash
git clone https://github.com/luanps2/TransformeseSolution.git
```

### 2. Abra no Visual Studio
Abra o arquivo `TransformeseSolution.sln`.

### 3. Configure o banco
Edite:
```
Transformese.Api/appsettings.json
```

### 4. Execute a API
Acesse:
```
https://localhost:5001/swagger
```

### 5. Execute o MVC
O projeto **Transformese.MVC** consome a API.

### 6. Execute o Desktop
O projeto **Transformese.Desktop** consome a API.

---

## 🔌 Fluxo de Comunicação
```
[MVC]  ────┐
           ├──→ API → Domain → Data → Banco
[Desktop] ─┘
```

---

## 📄 Documentação da API
```
/swagger
```

---

## 📊 Roadmap

### Em andamento
- Padronização dos endpoints  
- Ajustes de DTOs  
- Refatoração do MVC

### Futuro
- Autenticação JWT  
- Testes com xUnit  
- Dockerfile  
- Deploy no Azure

---

## 🧩 Boas Práticas
- Usar DTOs sempre  
- Não expor entidades do domínio  
- MVC/Desktop não acessam banco  
- Controllers finos, lógica em services  
- Repositórios separados  
- Uso de async/await  
- Migrations organizadas

---

## 🤝 Contribuição
1. Faça um fork  
2. Crie uma branch  
3. Envie um Pull Request

---

## 📜 Licença  
Recomendado uso da licença MIT.

---

## ✨ Autor  
Projeto desenvolvido por **Luan Costa**.

