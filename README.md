# CrudFaculdade – Produtos (ASP.NET Core + SQL Server + Razor Pages)

Projeto de faculdade com CRUD completo (Create, Read, Update, Delete) de **Produtos**, usando:

- **Backend:** ASP.NET Core Web API (C#)
- **Banco:** SQL Server (SSMS)
- **Frontend:** Razor Pages (ASP.NET Core)
- **Documentação da API:** Swagger (OpenAPI)

---

## Funcionalidades

### API (CrudFaculdade.Api)
- Listar produtos (com filtro por nome e categoria)
- Buscar produto por ID
- Criar produto
- Editar produto
- Excluir produto

**Filtros no endpoint de listagem:**
- `GET /api/Produtos?q=mouse`
- `GET /api/Produtos?categoria=GPU`
- `GET /api/Produtos?q=monitor&categoria=Periféricos`

### Web (CrudFaculdade.Web)
- Listagem de produtos (com pesquisa por nome e categoria)
- Criar produto
- Editar produto
- Excluir produto (com confirmação)
- Layout estilizado com Bootstrap + CSS customizado

---

## Pré-requisitos

- Visual Studio 2022
- .NET 8 SDK
- SQL Server (SQLEXPRESS ou instância local)
- SSMS (SQL Server Management Studio)

---

## Banco de Dados

1. Abra o SSMS e conecte na sua instância do SQL Server.
2. Crie o banco e a tabela (exemplo):

```sql
CREATE DATABASE CrudFaculdade;
GO
USE CrudFaculdade;
GO

CREATE TABLE dbo.Produtos (
    Id        INT IDENTITY(1,1) PRIMARY KEY,
    Nome      NVARCHAR(120) NOT NULL,
    Preco     DECIMAL(10,2) NOT NULL,
    Estoque   INT NOT NULL,
    Categoria NVARCHAR(60) NULL,
    Ativo     BIT NOT NULL CONSTRAINT DF_Produtos_Ativo DEFAULT (1),
    CriadoEm  DATETIME2 NOT NULL CONSTRAINT DF_Produtos_CriadoEm DEFAULT (SYSDATETIME())
);
GO

INSERT INTO dbo.Produtos (Nome, Preco, Estoque, Categoria)
VALUES
('Teclado Mecânico', 199.90, 10, 'Periféricos'),
('Mouse Gamer', 149.90, 15, 'Periféricos'),
('Headset', 299.90, 7, 'Áudio');
GO
