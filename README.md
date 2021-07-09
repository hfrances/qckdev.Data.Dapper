# qckdev.Data.Dapper

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
