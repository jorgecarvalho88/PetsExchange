﻿using Microsoft.EntityFrameworkCore;

namespace UserApi.Infrastructure.User
{
    public class UserContext
    {
        public static void SetModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.User>().ToTable("Users");
            modelBuilder.Entity<Model.User>().HasKey(s => s.Id);
            modelBuilder.Entity<Model.User>().Ignore(c => c.Errors);

            modelBuilder.Entity<Model.User>(entity =>
            {
                entity.Property(o => o.Name).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Email).IsRequired().HasMaxLength(50);
                entity.Property(o => o.Id).IsRequired();
                entity.Property(o => o.UniqueId).IsRequired();
            });
        }
    }
}
