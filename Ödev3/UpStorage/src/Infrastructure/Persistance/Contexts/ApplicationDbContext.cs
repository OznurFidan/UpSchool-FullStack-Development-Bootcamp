﻿using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance.Contexts
{
    public class ApplicationDbContext : DbContext,IApplicationDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Address { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Configurations
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //Ignores
        modelBuilder.Ignore<User>();
        modelBuilder.Ignore<Role>();
        modelBuilder.Ignore<UserRole>();
        modelBuilder.Ignore<RoleClaim>();
        modelBuilder.Ignore<UserToken>();
        modelBuilder.Ignore<UserClaim>();
        modelBuilder.Ignore<UserLogin>();

        base.OnModelCreating(modelBuilder);
    }
    
    }

}
