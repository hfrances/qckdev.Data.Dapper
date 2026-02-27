# Documentation

This folder contains operational documentation for `qckdev.Data.Dapper.Test.csproj`.

## Layered Model

- **Capa 1 (base):** checklist minima para cambios seguros.
  - [Test Dependencies Quick Reference](TEST_DEPENDENCIES_QUICK_REFERENCE.md)
- **Capa 2 (Capa 1 + detalle):** matriz completa de versiones y reglas de mantenimiento.
  - [Test Dependencies Update](TEST_DEPENDENCIES_UPDATE.md)
- **Capa 3 (Capa 1 + 2 + contexto Dapper):** compatibilidad de versiones de Dapper por framework.
  - [Dapper Compatibility](DAPPER_COMPATIBILITY.md)

## Notes

- Este repositorio no usa `Test.Common`.
- El stack de testing es unico para todos los frameworks del proyecto de tests.
