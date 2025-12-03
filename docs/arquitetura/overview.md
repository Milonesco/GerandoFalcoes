# 🏛️ Visão Geral da Arquitetura

O sistema **Gerando Falcões** é composto por duas aplicações integradas:

- Uma aplicação **Web (MVC)** para gerenciamento geral.
- Uma aplicação **Desktop (WinForms)** para uso operacional interno.

Ambas compartilham uma única base de dados SQL Server utilizando **Entity Framework** como camada ORM.

---

## 🧩 Componentes Principais

- **Frontend Web:** Razor Pages + Bootstrap  
- **Backend Web:** Controllers e Models em C#  
- **Desktop:** WinForms + Guna.UI2  
- **Banco de Dados:** SQL Server  
- **ORM:** Entity Framework Core  
- **Integração:** Mesmo banco e mesmas entidades para ambas aplicações  

---

## 🔗 Fluxo Geral do Sistema

1. Usuário interage pela Web ou Desktop  
2. A aplicação valida e envia requisições ao EF  
3. O EF mapeia as entidades e persiste no SQL Server  
4. Qualquer alteração é imediatamente refletida na outra aplicação  

---

## 📌 Objetivo da Arquitetura

Garantir:

- Consistência  
- Manutenibilidade  
- Escalabilidade  
- Reutilização das entidades  
- Baixa duplicação de lógica  