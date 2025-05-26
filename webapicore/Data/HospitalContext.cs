namespace webapicore.Data;

using Microsoft.EntityFrameworkCore;
using modelo.modelos;

public class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

    public DbSet<medico>? medico { get; set; }
    public DbSet<paciente>? paciente { get; set; }
    public DbSet<ingreso>? ingreso { get; set; }
    public DbSet<egreso>? egreso { get; set; }
}
