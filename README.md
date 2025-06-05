# ProductsMockApi

A simple .NET 8 RESTful Web API using **FastEndpoints** that integrates with the mock API at [https://restful-api.dev](https://restful-api.dev). It supports retrieving, creating, and deleting products.

---

##  Features

- Get products with **name filtering** and **pagination**
- Create new products with **validation**
- Delete products by ID
- Integrated unit tests with `xUnit` and `FluentAssertions`
- Auto-generated Swagger UI for testing endpoints
- Docker support

---

##  Tech Stack

- [.NET 8](https://dotnet.microsoft.com/)
- [FastEndpoints](https://fast-endpoints.com/)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [xUnit](https://xunit.net/)
- [Swagger / OpenAPI](https://swagger.io/)

---

## Project Structure

The project follows the clean architecture style and it's divided into two:
- The API layer
- The application layer

Here is how it's organized: 

ProductsMockApi.sln

├── src/

│ ├── ProductsMockApi/ # API layer (entry point, endpoints, middleware)

│ ├── ProductsMockApi.Application/ # Application layer (models, services, validators)

├── tests/

│ └── ProductsMockApi.Tests/ # xUnit test project

├── README.md #You're here

└── docker-compose.yml 

---

## Running the API

---

##  Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)

---

##  Clone the Repo

```bash
git clone git@github.com:aineaTheSecond/products-mock-api.git
````

## Running Locally (Without Docker)

Open the solution in your favorite IDE and hit the debug button

Alternatively:

```bash
# Navigate to API project
cd src/ProductsMockApi

# Run the API
dotnet run
```

Once running, open:

http://localhost:5088/swagger

---

## Running with Docker

- Navigate to the root project folder and run:

```bash
docker-compose up --build
```

Once the container has been created, open:

http://localhost:5678/swagger