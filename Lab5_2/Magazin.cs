using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Lab5_2
{
    public class Produs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Denumire prea lunga")]
        public string Denumire { get; set; }
        [Required]
        [MaxLength(500,ErrorMessage ="Descriere prea lunga")]
        public string Descriere { get; set; }
        public DateTime DataIntrare { get; set; }
        public DateTime DataExpirare { get; set; }
        public int Cantitate { get; set; }
    }
    public class Utilizator
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Nume prea lung")]
        public string Nume { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Prenume prea lung")]
        public string Prenume { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Username prea lung")]
        public string Username { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Parola prea lunga")]
        public string Parola { get; set; }
    }
    public class IstoricVanzari
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Id_user { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Denumire prea lunga")]
        public string Denumire { get; set; }
        public int Cantitate { get; set; }

    }
    public class MagazinDbContext:DbContext
    {
        public DbSet<Produs> Produse { get; set; }
        //am incercat sa fac aici, dar imi dadea o exceptie
        //public DbSet<Utilizator> Utilizatori { get; set; }
        //public DbSet<IstoricVanzari> Istoric { get; set; }
    }
    public class UtilizatorDbContext : DbContext
    {
        public DbSet<Utilizator> Utilizatori { get; set; }
    }
    public class VanzariDbContext: DbContext
    {
        public DbSet<IstoricVanzari> Vanzari { get; set; }
    }
}
