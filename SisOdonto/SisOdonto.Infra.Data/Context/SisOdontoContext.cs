using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SisOdonto.Domain.Models;
using Microsoft.Extensions.Configuration;


namespace SisOdonto.Infra.Data.Context
{
    public partial class SisOdontoContext : DbContext
    {
        public SisOdontoContext()
        {
        }

        public SisOdontoContext(DbContextOptions<SisOdontoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamentos { get; set; } = null!;
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<AutorizacaoPagtoConvenio> AutorizacaoPagtoConvenios { get; set; } = null!;
        public virtual DbSet<Cep> Ceps { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Convenio> Convenios { get; set; } = null!;
        public virtual DbSet<ConvenioPlano> ConvenioPlanos { get; set; } = null!;
        public virtual DbSet<Denticao> Denticaos { get; set; } = null!;
        public virtual DbSet<Especialidade> Especialidades { get; set; } = null!;
        public virtual DbSet<FormaPagamento> FormaPagamentos { get; set; } = null!;
        public virtual DbSet<Orcamento> Orcamentos { get; set; } = null!;
        public virtual DbSet<OrcamentoIten> OrcamentoItens { get; set; } = null!;
        public virtual DbSet<Parametro> Parametros { get; set; } = null!;
        public virtual DbSet<Procedimento> Procedimentos { get; set; } = null!;
        public virtual DbSet<ProcedimentoConvenio> ProcedimentoConvenios { get; set; } = null!;
        public virtual DbSet<TipoPagamento> TipoPagamentos { get; set; } = null!;
        public virtual DbSet<Tratamento> Tratamentos { get; set; } = null!;
        public virtual DbSet<TratamentoIten> TratamentoItens { get; set; } = null!;
        public virtual DbSet<TratamentoPagamento> TratamentoPagamentos { get; set; } = null!;
        public virtual DbSet<TratamentoPagtoConvenio> TratamentoPagtoConvenios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // get the configuration from the app settings
                var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

                // define the database to use
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                //optionsBuilder.UseLazyLoadingProxies(false);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.HasKey(e => new { e.DataAgendamento, e.HoraAgendamento });

                entity.ToTable("Agendamento");

                entity.Property(e => e.DataAgendamento).HasColumnType("date");

                entity.Property(e => e.Dddtelefone)
                    .HasMaxLength(2)
                    .HasColumnName("DDDTelefone");

                entity.Property(e => e.Nome).HasMaxLength(50);

                entity.Property(e => e.NumTelefone).HasMaxLength(10);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Agendamentos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Agendamento_Cliente");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LastAccess).HasColumnType("datetime");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.UsuarioAtivo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<AutorizacaoPagtoConvenio>(entity =>
            {
                entity.ToTable("AutorizacaoPagtoConvenio");

                entity.Property(e => e.DataAutorizacao).HasColumnType("date");

                entity.Property(e => e.ValorAutorizado).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdTratamentoPagtoConvenioNavigation)
                    .WithMany(p => p.AutorizacaoPagtoConvenios)
                    .HasForeignKey(d => d.IdTratamentoPagtoConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutorizacaoPagtoConvenio_TratamentoPagtoConvenio");
            });

            modelBuilder.Entity<Cep>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .IsClustered(false);

                entity.ToTable("CEP");

                entity.HasIndex(e => e.Codigo, "ClusteredIndex_20220617_202045")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Bairro).HasMaxLength(255);

                entity.Property(e => e.Logradouro).HasMaxLength(255);

                entity.Property(e => e.Municipio).HasMaxLength(255);

                entity.Property(e => e.NomeEdificio)
                    .HasMaxLength(255)
                    .HasColumnName("Nome_Edificio");

                entity.Property(e => e.Uf)
                    .HasMaxLength(255)
                    .HasColumnName("UF");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.CodigoCep).HasColumnName("Codigo_CEP");

                entity.Property(e => e.Complemento).HasMaxLength(100);

                entity.Property(e => e.Cpf)
                    .HasMaxLength(18)
                    .HasColumnName("CPF");

                entity.Property(e => e.DataNascimento)
                    .HasColumnType("date")
                    .HasColumnName("Data_Nascimento");

                entity.Property(e => e.DddCelular)
                    .HasMaxLength(2)
                    .HasColumnName("DDD_Celular");

                entity.Property(e => e.DddTelefone)
                    .HasMaxLength(2)
                    .HasColumnName("DDD_Telefone");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdcSexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Idc_Sexo")
                    .IsFixedLength();

                entity.Property(e => e.Nome).HasMaxLength(50);

                entity.Property(e => e.NumCelular)
                    .HasMaxLength(10)
                    .HasColumnName("Num_Celular");

                entity.Property(e => e.NumTelefone)
                    .HasMaxLength(10)
                    .HasColumnName("Num_Telefone");

                entity.Property(e => e.Numero).HasMaxLength(10);

                entity.Property(e => e.Rg)
                    .HasMaxLength(18)
                    .HasColumnName("RG");

                entity.HasOne(d => d.CodigoCepNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.CodigoCep)
                    .HasConstraintName("FK_Cliente_CEP");
            });

            modelBuilder.Entity<Convenio>(entity =>
            {
                entity.ToTable("Convenio");

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Cnpj)
                    .HasMaxLength(14)
                    .HasColumnName("CNPJ");

                entity.Property(e => e.DataInclusao).HasColumnType("date");

                entity.Property(e => e.DddTelefone1)
                    .HasMaxLength(2)
                    .HasColumnName("DDD_Telefone1");

                entity.Property(e => e.DddTelefone2)
                    .HasMaxLength(2)
                    .HasColumnName("DDD_Telefone2");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.NomeFantasia).HasMaxLength(50);

                entity.Property(e => e.NumAns).HasColumnName("Num_ANS");

                entity.Property(e => e.NumTelefone1)
                    .HasMaxLength(10)
                    .HasColumnName("Num_Telefone1");

                entity.Property(e => e.NumTelefone2)
                    .HasMaxLength(10)
                    .HasColumnName("Num_Telefone2");

                entity.Property(e => e.RazaoSocial).HasMaxLength(100);
            });

            modelBuilder.Entity<ConvenioPlano>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.NomePlano).HasMaxLength(50);

                entity.HasOne(d => d.IdConvenioNavigation)
                    .WithMany(p => p.ConvenioPlanos)
                    .HasForeignKey(d => d.IdConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConvenioPlanos_Convenio");
            });

            modelBuilder.Entity<Denticao>(entity =>
            {
                entity.ToTable("Denticao");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Altura).HasMaxLength(10);

                entity.Property(e => e.Lado).HasMaxLength(10);

                entity.Property(e => e.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.ToTable("Especialidade");

                entity.Property(e => e.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<FormaPagamento>(entity =>
            {
                entity.ToTable("FormaPagamento");

                entity.Property(e => e.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<Orcamento>(entity =>
            {
                entity.ToTable("Orcamento");

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.IdcTratamento).HasColumnName("idc_Tratamento");

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Orcamentos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orcamento_Cliente");
            });

            modelBuilder.Entity<Procedimento>(entity =>
            {
                entity.ToTable("Procedimento");

                entity.Property(e => e.NomeProcedimento).HasMaxLength(100);

                entity.Property(e => e.PercDesc).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdEspecialidadeNavigation)
                    .WithMany(p => p.Procedimentos)
                    .HasForeignKey(d => d.IdEspecialidade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procedimento_Especialidade");
            });

            modelBuilder.Entity<ProcedimentoConvenio>(entity =>
            {
                entity.ToTable("ProcedimentoConvenio");

                entity.Property(e => e.Procedimento).HasMaxLength(100);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.CodigoPlanoNavigation)
                    .WithMany(p => p.ProcedimentoConvenios)
                    .HasForeignKey(d => d.CodigoPlano)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcedimentoConvenio_ConvenioPlanos");

                entity.HasOne(d => d.IdConvenioNavigation)
                    .WithMany(p => p.ProcedimentoConvenios)
                    .HasForeignKey(d => d.IdConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcedimentoConvenio_Convenio");

                entity.HasOne(d => d.IdEspecialidadeNavigation)
                    .WithMany(p => p.ProcedimentoConvenios)
                    .HasForeignKey(d => d.IdEspecialidade)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcedimentoConvenio_Especialidade");
            });

            modelBuilder.Entity<TipoPagamento>(entity =>
            {
                entity.ToTable("TipoPagamento");

                entity.Property(e => e.Nome).HasMaxLength(50);
            });

            modelBuilder.Entity<Tratamento>(entity =>
            {
                entity.ToTable("Tratamento");

                entity.Property(e => e.DataInicio).HasColumnType("date");

                entity.Property(e => e.Observacao).HasMaxLength(500);

                entity.Property(e => e.ValorEntrada).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorTotal).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Tratamentos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tratamento_Cliente");

                entity.HasOne(d => d.IdFormaPagamentoNavigation)
                    .WithMany(p => p.Tratamentos)
                    .HasForeignKey(d => d.IdFormaPagamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tratamento_FormaPagamento");

                entity.HasOne(d => d.IdTipoPagamentoNavigation)
                    .WithMany(p => p.Tratamentos)
                    .HasForeignKey(d => d.IdTipoPagamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tratamento_TipoPagamento");
            });

            modelBuilder.Entity<TratamentoIten>(entity =>
            {
                entity.Property(e => e.Observacao).HasMaxLength(500);

                entity.HasOne(d => d.IdDenticaoNavigation)
                    .WithMany(p => p.TratamentoItens)
                    .HasForeignKey(d => d.IdDenticao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TratamentoItens_Denticao");

                entity.HasOne(d => d.IdProcedimentoNavigation)
                    .WithMany(p => p.TratamentoItens)
                    .HasForeignKey(d => d.IdProcedimento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TratamentoItens_Procedimento");

                entity.HasOne(d => d.IdTratamentoNavigation)
                    .WithMany(p => p.TratamentoItens)
                    .HasForeignKey(d => d.IdTratamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TratamentoItens_Tratamento");
            });

            modelBuilder.Entity<TratamentoPagamento>(entity =>
            {
                entity.ToTable("TratamentoPagamento");

                entity.Property(e => e.DataPagamento).HasColumnType("date");

                entity.Property(e => e.ValorPago).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ValorParcela).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdTratamentoNavigation)
                    .WithMany(p => p.TratamentoPagamentos)
                    .HasForeignKey(d => d.IdTratamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TratamentoPagamento_Tratamento");
            });

            modelBuilder.Entity<TratamentoPagtoConvenio>(entity =>
            {
                entity.ToTable("TratamentoPagtoConvenio");

                entity.Property(e => e.DataPagamento).HasColumnType("date");

                entity.Property(e => e.ValorPagoConvenio).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdConvenioNavigation)
                    .WithMany(p => p.TratamentoPagtoConvenios)
                    .HasForeignKey(d => d.IdConvenio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TratamentoPagtoConvenio_Convenio");

                entity.HasOne(d => d.IdTratamentoIntensNavigation)
                    .WithMany(p => p.TratamentoPagtoConvenios)
                    .HasForeignKey(d => d.IdTratamentoIntens)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TratamentoPagtoConvenio_TratamentoItens");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
