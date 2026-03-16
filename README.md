[![NuGet Version](https://img.shields.io/nuget/v/qckdev.Data.Dapper.svg)](https://www.nuget.org/packages/qckdev.Data.Dapper)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.Dapper&metric=alert_status)](https://sonarcloud.io/dashboard?id=qckdev.Data.Dapper)
[![Code Coverage](https://sonarcloud.io/api/project_badges/measure?project=qckdev.Data.Dapper&metric=coverage)](https://sonarcloud.io/dashboard?id=qckdev.Data.Dapper)
![Azure Pipelines Status](https://hfrances.visualstudio.com/Main/_apis/build/status/qckdev.Data.Dapper?branchName=master)

# qckdev.Data.Dapper

Provides a default set of tools for Dapper library.

## 🛠️ Installation

```bash
dotnet add package qckdev.Data.Dapper
```

## ⚡ Quick Start

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

## 🤝 Contributing
Issues and pull requests are welcome! See the contribution guidelines (coming soon).

## 📜 License
This project is licensed under the terms of the [MIT License](LICENSE).
