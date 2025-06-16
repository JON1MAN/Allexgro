# Allexgro
Allexgro is my lightweight vision of [Allegro](https://allegro.pl/) 

## Stack:
- backend - **C#** ASP .NET 9.0.201
- database - **PostgreSQL** 17.4
- frontend - **Vite + React ts** (in separate repo: Allexgro Frontend)

## How to launch application:
1) in your terminal from project root: **`docker compose up -d`** - this will run container with postgreSQL
2) **`dotnet ef database update`** - to apply migrations from /Allexgro/Migrations to database
3) **dotnet build** - to build a project (can throw warnings depends on your IDE)
4) **dotnet run** - run a project (port - **_localhost:5156_**)
