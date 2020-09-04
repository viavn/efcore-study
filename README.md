# EF Core Study

Following [desenvolvedor.io][link-dev-io] - [Introdução ao Entity Framework Core][link-ef-course]

## Running project
1. Change SQL Server SA password at `./env/db.env`
2. Run `docker-compose up -d` at root folder
3. Change application ConnectionString at `./src/Curso/Util/Configuration.cs`

## CLI commands
<br>

### Creating migration for specific DbContext
```
dotnet ef migrations add InitialMigration -p Curso/CursoEFCore.csproj -c ApplicationContext
```
* **`-p  Curso/CursoEFCore.csproj` -->** Project which it will look at
* **`-c ApplicationContext` -->** DbContext which it will look at
<br>

### Generating SQL script
```
dotnet ef migrations script -p ./Curso/CursoEFCore.csproj -o ./Curso/InitialMigration.sql
```
* **`-o ./Curso/InitialMigration.sql` -->** File location which the script file will be created
<br>

### Generating idempotent SQL script
```
dotnet ef migrations script -p ./Curso/CursoEFCore.csproj -o ./Curso/Idempotent.sql -i
```
* **`-i` -->** Idempontent option

### Apply migration to database
```
dotnet ef database update -p Curso/CursoEFCore.csproj -v
```
* **`-v` -->** Verbose mode

<!-- Links -->
[link-dev-io]:<https://desenvolvedor.io/inicio> "Site desenvolvedor.io"
[link-ef-course]:<https://desenvolvedor.io/curso-online-introducao-entity-framework-core> "See course info"
