using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OdiApp.Entity.Base;
using OdiApp.Entity.Identity;
using OdiApp.Entity.Token;
using System.Security.Claims;

namespace OdiApp.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
           : base(options)
        {
            _configuration = configuration;
        }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value,
                    b => b.MigrationsAssembly("OdiApp.DataAccessLayer"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional model configuration
        }

        public override int SaveChanges()
        {
            SetAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetAuditFields()
        {
            // Kullanıcı girişi kontrolü
            if (_httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return; // Kullanıcı giriş yapmamışsa audit işlemlerini yapma
            }
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseAuditModel && (e.State == EntityState.Added || e.State == EntityState.Modified));
            // Eğer audit edilecek entity yoksa metoddan çık
            if (!entries.Any())
            {
                return;
            }

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseAuditModel)entry.Entity).EklenmeTarihi = DateTime.UtcNow;
                    ((BaseAuditModel)entry.Entity).EkleyenId = userId;
                    ((BaseAuditModel)entry.Entity).Ekleyen = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    ((BaseAuditModel)entry.Entity).GuncellenmeTarihi = DateTime.UtcNow;
                    ((BaseAuditModel)entry.Entity).GuncelleyenId = userId;
                    ((BaseAuditModel)entry.Entity).Guncelleyen = userName;
                }
            }
        }
    }
}
