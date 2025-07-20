# Developer Evaluation Project

> Aplicação desenvolvida para avaliação de capacidades de desenvolvimento em .NET utilizando DDD.
> Desenvolvido em ambiente **Linux Ubuntu 20.04**.

---

## Tecnologias Utilizadas

- **.NET 8**
- **Entity Framework Core**
- **PostgreSQL**
- **MongoDB**
- **Redis (como Message Broker e Cache)**
- **Docker + Docker Compose**
- **xUnit (Testes Unitários)**

---

## Padrões e Arquitetura

- Domain-Driven Design (DDD)
- External Identities Pattern
- Publicação de eventos via Redis (`SaleCreated`, `SaleModified`, `SaleCancelled`, `ItemCancelled`)
- API adicional implementada além do escopo: **Products API**

---

## Instruções para Execução

### Pré-requisitos
- Docker e Docker Compose instalados
- .NET SDK 8 instalado (caso deseje rodar testes diretamente)

### Subindo a aplicação
```bash
docker-compose up --build
```

### Acessando a API
- Acesse via Swagger: http://localhost:8080/swagger

---

## Etapas de Teste

### 1 - Criar Usuário

**POST** `/api/users`

```json
{
  "username": "Usuario Teste",
  "password": "Teste@2025#",
  "phone": "31991188878",
  "email": "usuario@email.com",
  "status": 1,
  "role": 3
}
```

### 2 - Realizar Autenticação

**POST** `/api/auth/login`

```json
{
  "email": "usuario@email.com",
  "password": "Teste@2025#"
}
```

> **Importante**: Obter o token retornado e via Swagger colar em **Authorize**, colar em **Value** e clicar em **Authorize**

### 3 - Criar Produtos para Validar as APIs

**POST** `/api/products`

**Produto 1:**
```json
{
  "title": "Cerveja Brahma",
  "description": "Brahma Tradicional",
  "category": "Cervejas",
  "image": "https://example.com/images/product1.png",
  "price": 10,
  "rate": 5,
  "count": 100
}
```

**Produto 2:**
```json
{
  "title": "Cerveja Brahma Duplo Malte",
  "description": "Brahma Duplo Malte",
  "category": "Cervejas",
  "image": "https://example.com/images/product1.png",
  "price": 10,
  "rate": 5,
  "count": 100
}
```

> **Nota**: Anote o ID retornado do primeiro produto (exemplo: `8f8a3b00-51e0-436a-b856-0314ffa31f5a`)

### 4 - Criar uma Venda (Carts) para Validar as APIs

**POST** `/api/carts`

```json
{
  "saleNumber": "S2025-0001",
  "saleDate": "2025-07-19T15:08:00.269Z",
  "customer": {
    "id": "e8a1c2b0-9876-4cfd-90ab-123456789abc",
    "name": "João da Silva"
  },
  "branch": {
    "id": "b7c9a1f2-4321-4bcd-91ef-abcdef123456",
    "name": "Filial São Paulo"
  },
  "items": [
    {
      "productId": "8f8a3b00-51e0-436a-b856-0314ffa31f5a",
      "quantity": 5
    }
  ]
}
```

---

## Regras de Negócio

### Descontos por Quantidade
- **4+ itens idênticos**: 10% de desconto
- **10-20 itens idênticos**: 20% de desconto

### Restrições
- **Limite máximo**: 20 itens por produto
- **Sem desconto**: Para quantidades abaixo de 4 itens
- **Não é possível**: Vender acima de 20 itens idênticos

---

## Funcionalidades da API

### Sistema de Vendas
-  CRUD completo de vendas
-  Gestão de itens com quantidades e preços
-  Aplicação automática de regras de desconto
-  Controle de status (Ativo/Cancelado)
-  Registro de cliente e filial

### API de Produtos (Adicional)
-  CRUD completo de produtos
-  Categorização de produtos
-  Controle de estoque
-  Avaliações e imagens

### Sistema de Autenticação
-  Registro de usuários
-  Autenticação JWT
-  Controle de roles e permissões

---

## Testes Unitários

### Executar testes via .NET CLI
```bash
dotnet test
```

### Executar com cobertura de código (Linux)
```bash
chmod +x coverage.sh
./coverage.sh
```

Os testes unitários foram desenvolvidos para o módulo **Carts/Sales** e podem ser executados via `dotnet test` no projeto de testes unitários ou rodando o script `coverage.sh` em ambientes Linux.

---

## Eventos de Domínio

O sistema publica os seguintes eventos via Redis:

- **SaleCreated**: Disparado na criação de uma venda
- **SaleModified**: Disparado na modificação de uma venda  
- **SaleCancelled**: Disparado no cancelamento de uma venda
- **ItemCancelled**: Disparado no cancelamento de um item

---

## Estrutura do Projeto

```
developer-store-api/
├── src/
│   ├── DeveloperStore.Api/           # API Controllers
│   ├── DeveloperStore.Application/   # Use Cases e DTOs
│   ├── DeveloperStore.Domain/        # Entidades e Regras de Negócio
│   └── DeveloperStore.Infrastructure/ # Repositórios e Serviços
├── tests/
│   └── DeveloperStore.UnitTests/     # Testes Unitários
├── docker-compose.yml                # Configuração Docker
├── coverage.sh                       # Script de cobertura (Linux)
└── README.md
```

---

## Informações Adicionais

- **Ambiente de Desenvolvimento**: Linux Ubuntu 20.04
- **API Products**: Desenvolvida além do enunciado original
- **Message Broker**: Redis configurado para publicação de eventos
- **Testes**: Cobertura completa dos módulos principais
- **Documentação**: Swagger/OpenAPI integrado