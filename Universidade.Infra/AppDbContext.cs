using Universidade.Domain;
using Microsoft.EntityFrameworkCore;

namespace Universidade.Infra;

public class AppDbContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Professor> Professores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseMySql("Server=localhost;Database=universidade_db;User=root;Password=minhasenha;",
            new MySqlServerVersion(new Version(8, 0, 32)));
    }
}