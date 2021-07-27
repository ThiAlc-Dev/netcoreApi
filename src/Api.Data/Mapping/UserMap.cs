using System;
using Api.Domain.Entities;
using Api.Domain.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Nome).IsRequired().HasMaxLength(150);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(250);

            builder.HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = "Administrador",
                    Email = "helpdeskprog@outlook.com",
                    Password = "6103945b4c627fa62b6649cbdbbab5493797cab452ed533ca66922cac7d63913",
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = null
                }
            );
        }
    }
}