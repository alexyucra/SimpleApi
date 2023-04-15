using Microsoft.EntityFrameworkCore;

namespace SimpleApi.Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext(DbContextOptions<EmpresaContext> options)
            : base(options)
        {
        }

        public DbSet<SimpleApi.Models.Empresa> Empresa { get; set; } = default!;
    }
}
