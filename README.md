# \# FinalChallengeSA

# &#x20;

# API REST para gerenciamento de \*\*Customers\*\*, \*\*Products\*\* e \*\*Orders\*\*, desenvolvida em \*\*.NET 10\*\* seguindo principios de \*\*Clean Architecture\*\* com \*\*CQRS\*\* (Command Query Responsibility Segregation) e containerizacao via \*\*Docker\*\*.

# &#x20;

# \---

# &#x20;

# \## Sumario

# &#x20;

# \- \[Visao Geral da Arquitetura](#visao-geral-da-arquitetura)

# \- \[Diagramas C4](#diagramas-c4)

# \- \[Estrutura do Projeto](#estrutura-do-projeto)

# \- \[Design Patterns Utilizados (GoF)](#design-patterns-utilizados-gof)

# \- \[Outros Patterns Utilizados](#outros-patterns-utilizados)

# \- \[Design Patterns (GoF) que Poderiam ser Utilizados](#design-patterns-gof-que-poderiam-ser-utilizados)

# \- \[Diagrama de Classes - Domain](#diagrama-de-classes---domain)

# \- \[Fluxo de uma Requisicao](#fluxo-de-uma-requisicao)

# \- \[Como Executar](#como-executar)

# \- \[Endpoints](#endpoints)

# \- \[Melhorias Futuras](#melhorias-futuras)

# \- \[Tecnologias](#tecnologias)

# &#x20;

# \---

# &#x20;

# \## Visao Geral da Arquitetura

# &#x20;

# O projeto segue \*\*Clean Architecture\*\*, onde as dependencias apontam sempre para o centro (Domain). Cada camada tem uma responsabilidade bem definida e so conhece as camadas mais internas:

# &#x20;

# ```

# Api  ->  Application  ->  Domain

# &#x20;|            ^              ^

# &#x20;|            |              |

# &#x20;+->  Infra.IoC  ->  Infra.Data

# ```

# &#x20;

# \- \*\*Domain\*\* nao conhece nenhuma outra camada. Define entidades e regras de negocio puras.

# \- \*\*Application\*\* orquestra os casos de uso via MediatR (CQRS). Depende apenas do Domain. Define as interfaces dos repositorios.

# \- \*\*Infra.Data\*\* implementa as interfaces da Application com Entity Framework Core e SQL Server. Depende de Application e Domain.

# \- \*\*Infra.IoC\*\* centraliza o registro de dependencias. Referencia Application, Domain e Infra.Data para conectar contratos as implementacoes.

# \- \*\*Api\*\* e o ponto de entrada HTTP: controllers, middlewares e configuracao do pipeline. Chama Infra.IoC para registrar os servicos.

# &#x20;

# \---

# &#x20;

# \## Diagramas C4

# &#x20;

# \### Nivel 1 - Contexto

# &#x20;

# Visao de alto nivel: quem usa o sistema e com quais sistemas ele se comunica.

# &#x20;

# ```mermaid

# C4Context

# &#x20;   title FinalChallengeSA - Diagrama de Contexto

# &#x20;

# &#x20;   Person(user, "Usuario / Cliente", "Consome a API via HTTP para gerenciar customers, products e orders")

# &#x20;

# &#x20;   System(api, "FinalChallengeSA", "API REST para gestao de Customers, Products e Orders. .NET 10 com Clean Architecture e CQRS")

# &#x20;

# &#x20;   SystemDb(sqlserver, "SQL Server 2022", "Banco de dados relacional que persiste todas as entidades do dominio")

# &#x20;

# &#x20;   Rel(user, api, "Envia requisicoes", "HTTP/REST JSON")

# &#x20;   Rel(api, sqlserver, "Le e persiste dados", "TCP 1433 / EF Core")

# ```

# &#x20;

# \### Nivel 2 - Containers

# &#x20;

# Os containers que compoem o sistema e como se comunicam dentro do Docker Compose.

# &#x20;

# ```mermaid

# C4Container

# &#x20;   title FinalChallengeSA - Diagrama de Containers

# &#x20;

# &#x20;   Person(user, "Usuario", "Consome a API")

# &#x20;

# &#x20;   Container\_Boundary(docker, "Docker Compose") {

# &#x20;       Container(api, "FinalChallengeSA.Api", ".NET 10 / ASP.NET Core", "Expoe endpoints REST na porta 8080. Orquestra commands e queries via MediatR")

# &#x20;       ContainerDb(db, "SQL Server 2022", "Banco relacional", "Persiste Customers, Products, Orders e OrderProducts. Porta 1433")

# &#x20;   }

# &#x20;

# &#x20;   Rel(user, api, "HTTP/REST", "porta 5131")

# &#x20;   Rel(api, db, "EF Core / SqlClient", "porta 1433 (rede interna Docker)")

# ```

# &#x20;

# \### Nivel 3 - Componentes

# &#x20;

# A arquitetura interna da aplicacao, mostrando as camadas e suas dependencias.

# &#x20;

# ```mermaid

# flowchart TD

# &#x20;   subgraph API \["API (Adapters de Entrada)"]

# &#x20;       direction TB

# &#x20;       CC\[CustomersController]

# &#x20;       PC\[ProductsController]

# &#x20;       OC\[OrdersController]

# &#x20;       MW\[ExceptionMiddleware]

# &#x20;   end

# &#x20;

# &#x20;   subgraph APP \["Application (Casos de Uso)"]

# &#x20;       direction TB

# &#x20;       CMD\[Commands<br/>Create, Update, Delete]

# &#x20;       QRY\[Queries<br/>GetAll, GetById, FindByName, Count]

# &#x20;       DTO\[DTOs<br/>Request / Response]

# &#x20;       IFACE\[Interfaces<br/>IGenericRepository<br/>ICustomerRepository<br/>IProductRepository<br/>IOrderRepository]

# &#x20;   end

# &#x20;

# &#x20;   subgraph DOM \["Domain (Nucleo)"]

# &#x20;       direction TB

# &#x20;       ENT\[Entities<br/>Customer - Product - Order]

# &#x20;   end

# &#x20;

# &#x20;   subgraph INFRA \["Infra.Data (Adapters de Saida)"]

# &#x20;       direction TB

# &#x20;       REPO\[Repositories<br/>CustomerRepository<br/>ProductRepository<br/>OrderRepository]

# &#x20;       CTX\[ApplicationDbContext<br/>EF Core + Fluent API]

# &#x20;   end

# &#x20;

# &#x20;   subgraph IOC \["Infra.IoC"]

# &#x20;       DI\[DependencyInjectionConfig<br/>Extension Methods]

# &#x20;   end

# &#x20;

# &#x20;   DB\[(SQL Server)]

# &#x20;

# &#x20;   API -->|envia Commands/Queries via IMediator| APP

# &#x20;   API -->|chama extension methods| IOC

# &#x20;   APP -->|usa entidades| DOM

# &#x20;   INFRA -->|implementa interfaces| APP

# &#x20;   INFRA -->|mapeia entidades| DOM

# &#x20;   REPO --> CTX

# &#x20;   CTX --> DB

# ```

# &#x20;

# > \*\*Regra de dependencia:\*\* as setas apontam sempre para dentro. Domain nao referencia nenhum outro projeto. Application conhece apenas Domain. As interfaces dos repositorios ficam em Application (nao em Domain), seguindo o principio de Dependency Inversion — Application define os contratos, Infra.Data os implementa.

# &#x20;

# \---

# &#x20;

# \## Estrutura do Projeto

# &#x20;

# ```

# FinalChallengeSA/

# |

# |-- FinalChallengeSA.Domain/                         # Nucleo do negocio - zero dependencias externas

# |   |-- Entities/

# |   |   |-- Customer.cs                              # Entidade com encapsulamento via private setters

# |   |   |-- Product.cs                               # Entidade com metodo Update() e construtor protegido

# |   |   +-- Order.cs                                 # Entidade com calculo automatico de TotalAmount

# |   +-- FinalChallengeSA.Domain.csproj

# |

# |-- FinalChallengeSA.Application/                    # Casos de uso e orquestracao

# |   |-- Commands/                                    # Operacoes de escrita (Create, Update, Delete)

# |   |   |-- Customers/

# |   |   |   |-- CreateCustomer/                      # Command + Handler

# |   |   |   |-- UpdateCustomer/

# |   |   |   +-- DeleteCustomer/                      # Validacao de integridade referencial

# |   |   |-- Orders/

# |   |   |   |-- CreateOrder/

# |   |   |   |-- UpdateOrder/

# |   |   |   +-- DeleteOrder/

# |   |   +-- Products/

# |   |       |-- CreateProduct/

# |   |       |-- UpdateProduct/

# |   |       +-- DeleteProduct/                       # Valida se produto pertence a algum pedido

# |   |-- Queries/                                     # Operacoes de leitura

# |   |   |-- Customers/

# |   |   |   |-- GetAllCustomers/

# |   |   |   |-- GetCustomerById/

# |   |   |   |-- FindCustomersByName/

# |   |   |   +-- CountCustomers/

# |   |   |-- Orders/

# |   |   |   |-- GetAllOrders/

# |   |   |   |-- GetOrderById/

# |   |   |   |-- GetOrdersByCustomerIdQuery/

# |   |   |   |-- FindOrdersByName/

# |   |   |   +-- CountOrders/

# |   |   +-- Products/

# |   |       |-- GetAllProducts/

# |   |       |-- GetProductById/

# |   |       |-- FindProductsByName/

# |   |       +-- CountProducts/

# |   |-- DTOs/                                        # Objetos de transferencia (Request/Response)

# |   |   |-- CustomerDTOs.cs

# |   |   |-- ProductDTOs.cs

# |   |   +-- OrderDTOs.cs

# |   |-- Interfaces/                                  # Contratos dos repositorios

# |   |   |-- IGenericRepository.cs                    # Interface generica (CRUD + Count)

# |   |   |-- ICustomerRepository.cs

# |   |   |-- IProductRepository.cs

# |   |   +-- IOrderRepository.cs

# |   +-- Exceptions/                                  # Excecoes de negocio

# |       |-- NotFoundException.cs

# |       +-- ValidationException.cs

# |

# |-- FinalChallengeSA.Infra.Data/                     # Implementacao de acesso a dados

# |   |-- Context/

# |   |   +-- ApplicationDbContext.cs                  # DbContext com Fluent API configuration

# |   +-- Repositories/

# |       |-- CustomerRepository.cs

# |       |-- ProductRepository.cs

# |       +-- OrderRepository.cs

# |

# |-- FinalChallengeSA.Infra.IoC/                      # Inversao de controle

# |   +-- DependencyInjectionConfig.cs                 # Extension methods para registro de servicos

# |

# |-- FinalChallengeSA.Api/                            # Camada de apresentacao

# |   |-- Controllers/

# |   |   |-- CustomersController.cs

# |   |   |-- ProductsController.cs

# |   |   +-- OrdersController.cs

# |   |-- Middlewares/

# |   |   +-- ExceptionMiddleware.cs                   # Tratamento global (404, 409, 400)

# |   |-- Program.cs                                   # Composicao do app e pipeline HTTP

# |   |-- appsettings.json

# |   +-- appsettings.Development.json

# |

# |-- FinalChallengeSA.Tests/                          # Projeto de testes

# |

# |-- Dockerfile                                       # Multi-stage build (SDK -> Runtime)

# |-- docker-compose.yml                               # Orquestracao API + SQL Server

# |-- nuget.config                                     # Feed de pacotes (.NET 10 preview)

# |-- .dockerignore

# +-- FinalChallengeSA.slnx

# ```

# &#x20;

# \---

# &#x20;

# \## Design Patterns Utilizados (GoF)

# &#x20;

# Os patterns abaixo sao catalogados no livro \*"Design Patterns: Elements of Reusable Object-Oriented Software"\* (Gamma, Helm, Johnson, Vlissides — Gang of Four, 1994).

# &#x20;

# \### Mediator (Behavioral)

# &#x20;

# > \*"Define um objeto que encapsula como um conjunto de objetos interage. Promove o acoplamento fraco ao evitar que objetos se refiram uns aos outros explicitamente."\* — GoF

# &#x20;

# Os controllers nao conhecem os handlers diretamente. Todas as interacoes passam pelo `IMediator` (MediatR), que roteia commands e queries para o handler correspondente.

# &#x20;

# \*\*Onde esta no projeto:\*\*

# \- `CustomersController` envia `CreateCustomerCommand` via `IMediator.Send()` — nao conhece `CreateCustomerCommandHandler`

# \- Cada handler (`IRequestHandler<TRequest, TResponse>`) e registrado e resolvido pelo mediator automaticamente

# &#x20;

# \*\*Por que foi escolhido:\*\* desacopla a camada de apresentacao da logica de negocio. Adicionar um novo caso de uso nao exige alteracao nos controllers — basta criar o command/query e seu handler.

# &#x20;

# \### Chain of Responsibility (Behavioral)

# &#x20;

# > \*"Evita acoplar o remetente de uma requisicao ao seu receptor, dando a mais de um objeto a oportunidade de tratar a requisicao. Encadeia os objetos receptores e passa a requisicao ao longo da cadeia."\* — GoF

# &#x20;

# O pipeline de middlewares do ASP.NET Core e uma implementacao classica desse pattern. Cada middleware decide se trata a requisicao ou a passa adiante.

# &#x20;

# \*\*Onde esta no projeto:\*\*

# \- `ExceptionMiddleware` e o primeiro elo — captura excecoes dos middlewares seguintes

# \- O pipeline segue: ExceptionMiddleware -> HttpsRedirection (condicional) -> Authorization -> Controllers

# \- Se nenhum middleware trata a excecao, ela propaga ate o ExceptionMiddleware que a converte em resposta HTTP

# &#x20;

# \*\*Mapeamento de excecoes:\*\*

# &#x20;

# | Excecao | Status Code |

# |---------|-------------|

# | `NotFoundException` | 404 Not Found |

# | `ConflictException` | 409 Conflict |

# | `ValidationException` | 400 Bad Request |

# &#x20;

# \*\*Por que foi escolhido:\*\* centraliza o tratamento de erros sem acoplar a logica de erro aos controllers. Cada middleware tem responsabilidade unica e a ordem da cadeia define a prioridade.

# &#x20;

# \---

# &#x20;

# \## Outros Patterns Utilizados

# &#x20;

# \### CQRS (Command Query Responsibility Segregation)

# &#x20;

# Separacao explicita entre operacoes de \*\*escrita\*\* (Commands/) e \*\*leitura\*\* (Queries/). Cada operacao tem seu proprio request object e handler, evitando service classes monoliticas.

# &#x20;

# \*\*Por que foi escolhido:\*\* commands e queries evoluem independentemente. Facilita testes e respeita o Single Responsibility Principle.

# &#x20;

# \### Repository

# &#x20;

# Abstrai a persistencia atras de interfaces (`IGenericRepository<T>`, `ICustomerRepository`, etc.). A Application depende dos contratos; Infra.Data implementa com EF Core.

# &#x20;

# \*\*Por que foi escolhido:\*\* isola a tecnologia de banco da logica de negocio. Possibilita trocar SQL Server por qualquer outro provider sem impactar handlers, e mockar repositorios em testes unitarios.

# &#x20;

# \### DTO (Data Transfer Object)

# &#x20;

# Objetos dedicados para entrada/saida (Request/Response), separados das entidades de dominio.

# &#x20;

# \*\*Por que foi escolhido:\*\* evita expor a estrutura interna do dominio. Permite que o contrato da API evolua independentemente das entidades.

# &#x20;

# \### Rich Domain Model

# &#x20;

# Entidades com \*\*private setters\*\* e mutacao apenas via metodos explicitos (`Update()`, construtores). `Order.CalculateTotalAmount()` e privado — o total e sempre consistente com os produtos.

# &#x20;

# \*\*Por que foi escolhido:\*\* protege invariantes de negocio. Nenhum codigo externo pode definir um TotalAmount arbitrario.

# &#x20;

# \### Dependency Injection

# &#x20;

# Registro centralizado em `DependencyInjectionConfig.cs` via extension methods (`AddApplicationMediatR()`, `AddRepositories()`, `RegisterDbContext()`).

# &#x20;

# \*\*Por que foi escolhido:\*\* promove baixo acoplamento e mantem o `Program.cs` limpo.

# &#x20;

# \---

# &#x20;

# \## Design Patterns (GoF) que Poderiam ser Utilizados

# &#x20;

# \### Observer (Behavioral)

# &#x20;

# > \*"Define uma dependencia um-para-muitos entre objetos, de modo que quando um muda de estado, todos os dependentes sao notificados."\* — GoF

# &#x20;

# \*\*Onde aplicaria:\*\* Domain Events. Apos criar um pedido, um evento `OrderCreated` notificaria observers interessados (auditoria, notificacoes, cache invalidation) sem acoplar esses side effects ao handler de criacao.

# &#x20;

# \### Facade (Structural)

# &#x20;

# > \*"Fornece uma interface unificada para um conjunto de interfaces em um subsistema."\* — GoF

# &#x20;

# \*\*Onde aplicaria:\*\* para operacoes que envolvem multiplos repositorios (ex: criar order exige buscar customer + products + persistir order). Um `OrderFacade` simplificaria essa orquestracao, oferecendo um metodo unico `CriarPedidoAsync()` que coordena todos os passos internamente.

# &#x20;

# \### Decorator (Structural)

# &#x20;

# > \*"Anexa responsabilidades adicionais a um objeto dinamicamente."\* — GoF

# &#x20;

# \*\*Onde aplicaria:\*\* Pipeline Behaviors do MediatR sao decorators. Um `ValidationBehavior<TRequest, TResponse>` envolveria cada handler, executando FluentValidation antes da logica principal. Um `LoggingBehavior` adicionaria logging automatico. Cada behavior e um decorator empilhado ao redor do handler original, sem modifica-lo.

# &#x20;

# \---

# &#x20;

# \## Diagrama de Classes - Domain

# &#x20;

# ```mermaid

# classDiagram

# &#x20;   direction LR

# &#x20;

# &#x20;   class Customer {

# &#x20;       +Guid Id

# &#x20;       +string Name

# &#x20;       +string Email

# &#x20;       +Customer(string name, string email)

# &#x20;       +Update(string name, string email)

# &#x20;   }

# &#x20;

# &#x20;   class Product {

# &#x20;       +Guid Id

# &#x20;       +string Name

# &#x20;       +string Description

# &#x20;       +decimal Price

# &#x20;       +Product(string name, string description, decimal price)

# &#x20;       +Update(string name, string description, decimal price)

# &#x20;   }

# &#x20;

# &#x20;   class Order {

# &#x20;       +Guid Id

# &#x20;       +Guid CustomerId

# &#x20;       +Customer Customer

# &#x20;       +List\~Product\~ Products

# &#x20;       +decimal TotalAmount

# &#x20;       +Order(Guid customerId, IEnumerable\~Product\~ products)

# &#x20;       +Update(Guid customerId, IEnumerable\~Product\~ products)

# &#x20;       -CalculateTotalAmount()

# &#x20;   }

# &#x20;

# &#x20;   Order "N" --> "1" Customer : pertence a

# &#x20;   Order "N" --> "N" Product : contem

# ```

# &#x20;

# O relacionamento \*\*Order-Product\*\* e muitos-para-muitos, mapeado via tabela de juncao `OrderProducts` no banco de dados. O `DeleteBehavior.Restrict` em \*\*Order-Customer\*\* impede a exclusao de um customer que possua pedidos vinculados.

# &#x20;

# \---

# &#x20;

# \## Fluxo de uma Requisicao

# &#x20;

# O diagrama abaixo ilustra o fluxo completo de criacao de um pedido, desde a requisicao HTTP ate a persistencia no banco:

# &#x20;

# ```mermaid

# sequenceDiagram

# &#x20;   autonumber

# &#x20;   actor Client as Cliente HTTP

# &#x20;   participant Ctrl as Controller

# &#x20;   participant Med as IMediator

# &#x20;   participant Cmd as CommandHandler

# &#x20;   participant Repo as Repository

# &#x20;   participant DB as SQL Server

# &#x20;

# &#x20;   Client->>Ctrl: POST /api/orders (JSON)

# &#x20;   Ctrl->>Med: Send(CreateOrderCommand)

# &#x20;   Med->>Cmd: Handle(command, cancellationToken)

# &#x20;   Cmd->>Repo: GetByIdAsync(customerId)

# &#x20;   Repo->>DB: SELECT Customer

# &#x20;   DB-->>Repo: Customer

# &#x20;   Cmd->>Repo: GetByIdAsync(productId) x N

# &#x20;   Repo->>DB: SELECT Product

# &#x20;   DB-->>Repo: Product

# &#x20;   Note over Cmd: Cria entidade Order com calculo de TotalAmount

# &#x20;   Cmd->>Repo: AddAsync(order)

# &#x20;   Repo->>DB: INSERT Order + OrderProducts

# &#x20;   DB-->>Repo: OK

# &#x20;   Cmd-->>Med: OrderResponse (DTO)

# &#x20;   Med-->>Ctrl: OrderResponse

# &#x20;   Ctrl-->>Client: 201 Created (JSON)

# ```

# &#x20;

# \---

# &#x20;

# \## Como Executar

# &#x20;

# \### Com Docker (recomendado)

# &#x20;

# ```bash

# docker-compose up --build

# ```

# &#x20;

# | Servico | URL |

# |---------|-----|

# | API | http://localhost:5131 |

# | Swagger UI | http://localhost:5131/swagger |

# | SQL Server | localhost:1433 |

# &#x20;

# Credenciais do SQL Server (desenvolvimento):

# \- \*\*User:\*\* sa

# \- \*\*Password:\*\* Your\_strong\_Password123!

# &#x20;

# O banco de dados e criado automaticamente na inicializacao da API via `EnsureCreated()`.

# &#x20;

# \### Localmente (sem Docker)

# &#x20;

# Pre-requisitos: \*\*.NET 10 SDK\*\* e \*\*SQL Server\*\* rodando localmente.

# &#x20;

# 1\. Configure a connection string em `appsettings.Development.json`:

# ```json

# {

# &#x20; "ConnectionStrings": {

# &#x20;   "DefaultConnection": "Server=localhost;Database=FinalChallengeSA;User Id=sa;Password=SuaSenha;TrustServerCertificate=True"

# &#x20; }

# }

# ```

# &#x20;

# 2\. Execute:

# ```bash

# dotnet restore

# dotnet run --project FinalChallengeSA.Api

# ```

# &#x20;

# \---

# &#x20;

# \## Endpoints

# &#x20;

# \### Customers

# &#x20;

# | Metodo | Rota | Descricao |

# |--------|------|-----------|

# | `POST` | `/api/customers` | Criar customer |

# | `GET` | `/api/customers` | Listar todos |

# | `GET` | `/api/customers/{id}` | Buscar por ID |

# | `GET` | `/api/customers/name/{name}` | Buscar por nome |

# | `GET` | `/api/customers/count` | Contagem total |

# | `PUT` | `/api/customers/{id}` | Atualizar |

# | `DELETE` | `/api/customers/{id}` | Remover |

# &#x20;

# \### Products

# &#x20;

# | Metodo | Rota | Descricao |

# |--------|------|-----------|

# | `POST` | `/api/products` | Criar product |

# | `GET` | `/api/products` | Listar todos |

# | `GET` | `/api/products/{id}` | Buscar por ID |

# | `GET` | `/api/products/name/{name}` | Buscar por nome |

# | `GET` | `/api/products/count` | Contagem total |

# | `PUT` | `/api/products/{id}` | Atualizar |

# | `DELETE` | `/api/products/{id}` | Remover |

# &#x20;

# \### Orders

# &#x20;

# | Metodo | Rota | Descricao |

# |--------|------|-----------|

# | `POST` | `/api/orders` | Criar order |

# | `GET` | `/api/orders` | Listar todos |

# | `GET` | `/api/orders/{id}` | Buscar por ID |

# | `GET` | `/api/orders/by-customer/{customerId}` | Buscar por customer |

# | `GET` | `/api/orders/count` | Contagem total |

# | `PUT` | `/api/orders/{id}` | Atualizar |

# | `DELETE` | `/api/orders/{id}` | Remover |

# &#x20;

# \---

# &#x20;

# \## Melhorias Futuras

# &#x20;

# As seguintes melhorias foram planejadas mas nao implementadas por restricao de tempo:

# &#x20;

# \- \*\*Unit Tests por projeto:\*\* testes unitarios cobrindo cada camada individualmente. Entidades do Domain (validacao de invariantes, calculo de TotalAmount), command/query handlers da Application (com repositorios mockados), e validacao de mapeamento em Infra.Data

# \- \*\*Projeto de Automation Tests:\*\* projeto separado de testes de integracao executando contra a API real via Docker, validando o fluxo completo desde requisicoes HTTP ate o banco de dados

# \- \*\*FluentValidation + Pipeline Behavior:\*\* validacao automatica de input em todos os commands atraves de um `ValidationBehavior`, eliminando validacao manual nos handlers

# \- \*\*Domain Events:\*\* eventos para desacoplar side effects das operacoes principais (notificacoes, auditoria, cache invalidation)

# \- \*\*Paginacao:\*\* suporte a paginacao nas queries de listagem para cenarios com grandes volumes de dados

# &#x20;

# \---

# &#x20;

# \## Tecnologias

# &#x20;

# | Tecnologia | Versao | Uso |

# |------------|--------|-----|

# | .NET / ASP.NET Core | 10.0 | Framework da API |

# | Entity Framework Core | 10.0 | ORM e acesso a dados |

# | SQL Server | 2022 | Banco de dados relacional |

# | MediatR | 14.1 | Implementacao do Mediator/CQRS |

# | FluentValidation | 11.3 | Validacao de input |

# | Docker / Docker Compose | - | Containerizacao e orquestracao |

# | Swagger / OpenAPI | - | Documentacao interativa da API |

# &#x20;

