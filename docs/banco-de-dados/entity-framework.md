# 🔧 Entity Framework – Configuração e Migrações

O sistema utiliza EF Core como mapeamento ORM.

---

## 🧩 Contexto

- DbContext centralizado  
- DbSets representando tabelas reais  
- Mapeamento automático (convention over configuration)  

---

## 🔄 Migrações

Exemplo:

Add-Migration InitialCreate
Update-Database