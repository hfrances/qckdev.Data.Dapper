using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace qckdev.Data.Dapper.Test.Configuration
{
    
    sealed class TestConfiguration : IEntityTypeConfiguration<Entities.Test>
    {
        public void Configure(EntityTypeBuilder<Entities.Test> builder)
        {
            builder.HasKey(x => x.TestId);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Factor).HasDefaultValue(1);
        }
    }
}
