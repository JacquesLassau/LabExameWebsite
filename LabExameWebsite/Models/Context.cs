using System.Data.Entity;

namespace LabExameWebsite.Models
{
    public class Context: DbContext
    {
        public Context() : base("Server=DESKTOP-1T99VF9;Database=DbLabExame;User Id=sa;Password=123@qwe;") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
            modelBuilder.Entity<Paciente>().ToTable("TBPACIENTE");
            modelBuilder.Entity<Paciente>().Property(p => p.PacienteID).HasColumnName("PACODPAC");
            modelBuilder.Entity<Paciente>().Property(p => p.NomePaciente).HasColumnName("PANOMEPAC");
            modelBuilder.Entity<Paciente>().Property(p => p.CpfPaciente).HasColumnName("PACPFPAC");
            modelBuilder.Entity<Paciente>().Property(p => p.DataNascimentoPaciente).HasColumnName("PANASCPAC");
            modelBuilder.Entity<Paciente>().Property(p => p.SexoPaciente).HasColumnName("PASEXOPAC");
            modelBuilder.Entity<Paciente>().Property(p => p.TelefonePaciente).HasColumnName("PATELPAC");
            modelBuilder.Entity<Paciente>().Property(p => p.EmailPaciente).HasColumnName("PAEMAILPAC");
                        
            modelBuilder.Entity<TipoExame>().ToTable("TBTIPOEXAME");
            modelBuilder.Entity<TipoExame>().Property(p => p.TipoExameID).HasColumnName("TPCODEXA");
            modelBuilder.Entity<TipoExame>().Property(p => p.NomeTipoExame).HasColumnName("TPNOMEEXA");
            modelBuilder.Entity<TipoExame>().Property(p => p.DescricaoTipoExame).HasColumnName("TPDESCEXA");
                        
            modelBuilder.Entity<Exame>().ToTable("TBEXAME");
            modelBuilder.Entity<Exame>().Property(p => p.ExameID).HasColumnName("EXCODEXA");
            modelBuilder.Entity<Exame>().Property(p => p.TipoExameID).HasColumnName("EXIDTPEXA");
            modelBuilder.Entity<Exame>().Property(p => p.NomeExame).HasColumnName("EXNOMEEXA");
            modelBuilder.Entity<Exame>().Property(p => p.ObservacaoExame).HasColumnName("EXOBSEXA");
                        
            modelBuilder.Entity<Agendamento>().ToTable("TBAGENDAMENTO");
            modelBuilder.Entity<Agendamento>().Property(p => p.AgendamentoID).HasColumnName("COCODCON");
            modelBuilder.Entity<Agendamento>().Property(p => p.PacienteID).HasColumnName("COIDPACCON");
            modelBuilder.Entity<Agendamento>().Property(p => p.ExameID).HasColumnName("COIDEXCON");
            modelBuilder.Entity<Agendamento>().Property(p => p.DataHoraAgendamento).HasColumnName("CODTHRCON");
            modelBuilder.Entity<Agendamento>().Property(p => p.ProtocoloAgendamento).HasColumnName("COPROTCON");
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<TipoExame> TiposExames { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
    }
}