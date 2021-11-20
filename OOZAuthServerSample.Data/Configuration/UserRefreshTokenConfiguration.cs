using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OOZAuthServereSample.Core.Model;

namespace OOZAuthServerSample.Data.Configuration
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
      

        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x=>x.Code);
            builder.Property(x=>x.Code).IsRequired().HasMaxLength(200);
        }
    }
}
