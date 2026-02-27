# Actualizacion de Dependencias de Tests Unitarios

## Proyecto
`qckdev.Data.Dapper.Test`

## Estado actual (fuente de verdad)

- Target frameworks: `netcoreapp3.1;net5.0;net6.0;net8.0;net10.0`
- Test SDK/MSTest/Coverlet: mismas versiones para todos los frameworks
- Dependencias runtime (EF Core/Sqlite): versionadas por framework
- No existe proyecto `Test.Common` en este repositorio

## Regla de implementacion para este proyecto

1. Mantener `TargetFrameworks` exactamente como estan, salvo solicitud explicita.
2. Mantener un unico `ItemGroup` sin `Condition` para paquetes de testing.
3. Condicionar solo paquetes runtime por framework.
4. Mantener la matriz de `Microsoft.Data.Sqlite` + `Microsoft.EntityFrameworkCore` por TFM.

## Matriz de versiones

### Testing (todos los frameworks)

- `Microsoft.NET.Test.Sdk`: `17.11.1`
- `MSTest.TestAdapter`: `3.2.2`
- `MSTest.TestFramework`: `3.2.2`
- `coverlet.msbuild`: `6.0.0`
- `coverlet.collector`: `6.0.0`

### Runtime por framework

- `netcoreapp3.1`, `net5.0`:
  - `Microsoft.Data.Sqlite`: `3.1.32`
  - `Microsoft.EntityFrameworkCore`: `3.1.32`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `3.1.32`
- `net6.0`:
  - `Microsoft.Data.Sqlite`: `6.0.36`
  - `Microsoft.EntityFrameworkCore`: `6.0.36`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `6.0.36`
- `net8.0`:
  - `Microsoft.Data.Sqlite`: `8.0.11`
  - `Microsoft.EntityFrameworkCore`: `8.0.11`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `8.0.11`
- `net10.0`:
  - `Microsoft.Data.Sqlite`: `10.0.0`
  - `Microsoft.EntityFrameworkCore`: `10.0.0`
  - `Microsoft.EntityFrameworkCore.Sqlite`: `10.0.0`

## Verificacion

```powershell
dotnet test qckdev.Data.Dapper.Test\qckdev.Data.Dapper.Test.csproj
dotnet test qckdev.Data.Dapper.Test\qckdev.Data.Dapper.Test.csproj --list-tests
```
