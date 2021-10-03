<a href="https://www.nuget.org/packages/qckdev.Data.Dapper"><img src="https://img.shields.io/nuget/v/qckdev.Data.Dapper.svg" alt="NuGet Version"/></a>
<a href="https://sonarcloud.io/dashboard?id=qckdev.Data.Dapper"><img src="https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.Dapper&metric=alert_status" alt="Quality Gate"/></a>
<a href="https://sonarcloud.io/dashboard?id=qckdev.Data.Dapper"><img src="https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.Dapper&metric=coverage" alt="Code Coverage"/></a>
<a><img src="https://hfrances.visualstudio.com/Main/_apis/build/status/qckdev.Data.Dapper?branchName=main" alt="Azure Pipelines Status"/></a>

# qckdev.Data.Dapper

Provides a default set of tools for Dapper library.

```cs
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    sealed class Test
    {

        [Column("TestIdColumn")]
        public Guid TestId { get; set; }
        [Column("NameColumn")]
        public string Name { get; set; }
        [Column("FactorColumn")]
        public int Factor { get; set; }

    }
}

```


```cs
using Dapper;
using qckdev.Data.Dapper;


SqlMapper.AddTypeHandler(new TypeHandler.GuidTypeHandler());
SqlMapperMore.SetMapper<Entities.Test>();
```
