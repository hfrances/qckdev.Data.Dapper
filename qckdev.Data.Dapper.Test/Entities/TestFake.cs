using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace qckdev.Data.Dapper.Test.Entities
{
    sealed class TestFake
    {

        [Column("TestId")]
        public Guid TestIdFake { get; set; }
        [Column("Name")]
        public string NameFake { get; set; }
        [Column("Factor")]
        public int FactorFake { get; set; }
        [Column("Spaced")]
        public string SpacedFake { get; set; }
    }
}
