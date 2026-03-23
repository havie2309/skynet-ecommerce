# SnoopyCart Architecture Diagram

```mermaid
flowchart LR
    A["Angular Frontend<br/>Standalone Components"] --> B["ASP.NET Core Web API"]
    B --> C["SQL Server Database"]
    B --> D["Redis Basket Store"]
    B --> E["Stripe Payments"]
    B --> F["SMTP Email Service"]

    subgraph Frontend
        A1["Shop / Product List"]
        A2["Basket / Checkout"]
        A3["Orders / Profile / Admin"]
    end

    subgraph Backend
        B1["Controllers"]
        B2["Services"]
        B3["Entity Framework Core"]
        B4["JWT Authentication"]
        B5["Health Checks / Logging"]
    end

    A --> A1
    A --> A2
    A --> A3

    B --> B1
    B --> B2
    B --> B3
    B --> B4
    B --> B5
