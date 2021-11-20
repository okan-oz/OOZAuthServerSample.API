using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOZAuthServereSample.Core.Model;

namespace OOZAuthServerSample.Data.Configuration
{
    public class UserAppConfiguration : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            builder.Property(x=>x.City).HasMaxLength(30);
        }
    }
}
