using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using treino_mvc2.Models;

namespace treino_mvc2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Animais> Animal {get; set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
