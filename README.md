# Wheres the Key? :key:

Um projeto para controle de reserva de salas, laboratórios e outros espaços em uma universidade.

Para criação o mapeamento do banco com Entity Framework usando Database First, deverá executado alguns comandos:

```bash
> dotnet user-secrets init
> dotnet user-secrets set ConnectionStrings:WheresTheKey "Server=<INSTANCIA>;Initial Catalog=Wheresthekey;Persist Security Info=False;User Id=<USUARIO>;Password=<SENHA>;Encrypt=True;TrustServerCertificate=True;"
> dotnet ef dbcontext scaffold Name=ConnectionStrings:WheresTheKey Microsoft.EntityFrameworkCore.SqlServer --namespace Server.Models --context-namespace Server.Context --output-dir Models --context-dir Context
```
