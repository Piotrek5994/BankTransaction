using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankTransaction.Models;

public class TransactionDbContext : IdentityDbContext<User>
{
    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {

    }

    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .Property(e => e.AccountBalance)
            .HasMaxLength(250);

        //Add user who is admin
        var hasher = new PasswordHasher<User>();

        User admin = new User
        {
            UserName = "admin",
            // podczas logowanie pobiera tą nazwe użytkownika 
            NormalizedUserName = "MISIEK5994@GMAIL.COM",
            Email = "Misiek5994@gmail.com",
            NormalizedEmail = "MISIEK5994@GMAIL.COM",
            EmailConfirmed = true,
        };

        User user = new User
        {
            UserName = "user",
            // podczas logowanie pobiera tą nazwe użytkownika 
            NormalizedUserName = "RANDOM5994@GMAIL.COM",
            Email = "Random5994@gmail.com",
            NormalizedEmail = "RANDOM5994@GMAIL.COM",
            EmailConfirmed = true,
        };

        admin.PasswordHash = hasher.HashPassword(admin, "Misiek5994#");
        user.PasswordHash = hasher.HashPassword(user,"Random5994#");

        // Dodanie użytkowników ADMINA ORAZ USERA
        modelBuilder.Entity<User>().HasData(admin,user);

        IdentityRole adminRole = new IdentityRole
        {
            
            Name = "admin",
            NormalizedName = "ADMIN"
        };

        IdentityRole userRole = new IdentityRole
        {
            Name = "user",
            NormalizedName = "USER"
        };
        
        // Dodanie roli ADMINA ORAZ USERA
        modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = adminRole.Id,
                UserId = admin.Id
            },
            new IdentityUserRole<string>
            {
                RoleId = userRole.Id,
                UserId = user.Id
            }
        );
        // add to login connection
        //modelBuilder.Entity<IdentityUserLogin<string>>()
        //    .HasData(new IdentityUserLogin<string>
        //    {
        //        LoginProvider = "Admin",
        //        ProviderKey = "Admin",
        //        ProviderDisplayName = "Admin",
        //        UserId = "1",
        //    });



    }
}