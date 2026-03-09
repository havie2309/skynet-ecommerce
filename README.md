# Skinet E-Commerce

A full-stack e-commerce application built with .NET 8 and Angular.

## Tech Stack

**Backend**
- .NET 8 Web API
- PostgreSQL — relational database
- Redis — caching & cart storage
- Entity Framework Core — ORM & migrations
- JWT — authentication
- BCrypt — password hashing
- Stripe — payments
- Swagger — API documentation

**Frontend**
- Angular
- Bootstrap

**Infrastructure**
- Docker & Docker Compose

---

## Setup

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Node.js](https://nodejs.org)

### 1. Clone the repo
```bash
git clone https://github.com/yourusername/skynet-ecommerce.git
cd skynet-ecommerce
```

### 2. Start Docker (Postgres + Redis)
```bash
docker compose up -d
```

### 3. Run the API
```bash
cd api
dotnet ef database update
dotnet run
```

API will be available at `http://localhost:5283`
Swagger UI at `http://localhost:5283/swagger`

### 4. Run the Angular client
```bash
cd client
npm install
ng serve
```

Client will be available at `http://localhost:4200`

> See `client/README.md` for full Angular CLI documentation.

---

## API Endpoints

### Account
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/account/register` | Register a new user | No |
| POST | `/api/account/login` | Login and receive JWT token | No |

### Products
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/products` | Get all products (paginated) | No |
| GET | `/api/products/{id}` | Get product by ID | No |

#### Product query parameters
| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `search` | string | - | Filter by product name |
| `minPrice` | decimal | - | Filter by minimum price |
| `maxPrice` | decimal | - | Filter by maximum price |
| `page` | int | 1 | Page number |
| `pageSize` | int | 10 | Results per page |

#### Example response — GET `/api/products`
```json
{
  "totalCount": 2,
  "page": 1,
  "pageSize": 10,
  "data": [
    {
      "id": 1,
      "name": "Laptop",
      "description": "Gaming laptop",
      "price": 1200,
      "stockQuantity": 10,
      "imageUrl": null
    }
  ]
}
```
