# 🖥 Arquitetura Web – MVC

A aplicação Web utiliza o padrão **Model-View-Controller**, separando claramente as responsabilidades.

---

## 📁 Estrutura

- **Models:** representam as entidades do sistema  
- **Views:** páginas Razor responsivas  
- **Controllers:** lógica de controle e rotas  

---

## 📌 Principais Tecnologias

- ASP.NET MVC  
- Razor  
- Bootstrap  
- JavaScript  
- Entity Framework  

---

## 🔄 Ciclo MVC no Projeto

1. Usuário acessa rota  
2. Controller recebe a requisição  
3. Model comunica com EF  
4. Dados retornam para a View  
5. View exibe resultados ao usuário  

---

## 📎 Observações de Implementação

- Utilização de Scaffold em CRUDs  
- Models espelhados com o EF  
- Rotas organizadas por área funcional 