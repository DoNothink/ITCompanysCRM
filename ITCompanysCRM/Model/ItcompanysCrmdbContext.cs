using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ITCompanysCRM.Model;

public partial class ItcompanysCrmdbContext : DbContext
{
    public ItcompanysCrmdbContext()
    {
    }

    public ItcompanysCrmdbContext(DbContextOptions<ItcompanysCrmdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressView> AddressViews { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DevTeam> DevTeams { get; set; }

    public virtual DbSet<IssuedPassView> IssuedPassViews { get; set; }

    public virtual DbSet<IssuedPassport> IssuedPassports { get; set; }

    public virtual DbSet<LogBook> LogBooks { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StatusOfProject> StatusOfProjects { get; set; }

    public virtual DbSet<Street> Streets { get; set; }

    public virtual DbSet<TypeOfClient> TypeOfClients { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=K218PC\\SQLEXPRESS;Initial Catalog=ITCompanysCRMDB;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.IdAddress);

            entity.ToTable("Address");

            entity.Property(e => e.HomeAddress).HasMaxLength(10);

            entity.HasOne(d => d.IdCityNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_City");

            entity.HasOne(d => d.IdCountryNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Country");

            entity.HasOne(d => d.IdStreetNavigation).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.IdStreet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Address_Street");
        });

        modelBuilder.Entity<AddressView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AddressVIew");

            entity.Property(e => e.NameAddress).HasMaxLength(182);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.IdCity);

            entity.ToTable("City");

            entity.HasIndex(e => e.NameCity, "UQ_NameCity").IsUnique();

            entity.Property(e => e.NameCity).HasMaxLength(50);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("Client");

            entity.Property(e => e.EmailClient).HasMaxLength(50);
            entity.Property(e => e.NameClient).HasMaxLength(150);
            entity.Property(e => e.OthersData).HasMaxLength(150);
            entity.Property(e => e.PhoneNumberClient).HasMaxLength(20);
            entity.Property(e => e.ServicesClient).HasMaxLength(150);

            entity.HasOne(d => d.IdTypeOfClientNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdTypeOfClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_TypeOfClient");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry);

            entity.ToTable("Country");

            entity.HasIndex(e => e.NameCountry, "UQ_NameCountry").IsUnique();

            entity.Property(e => e.NameCountry).HasMaxLength(50);
        });

        modelBuilder.Entity<DevTeam>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DevTeam");

            entity.HasOne(d => d.IdProjectsNavigation).WithMany()
                .HasForeignKey(d => d.IdProjects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DevTeam_Projects");

            entity.HasOne(d => d.IdStaffNavigation).WithMany()
                .HasForeignKey(d => d.IdStaff)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DevTeam_Staff");
        });

        modelBuilder.Entity<IssuedPassView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("IssuedPassView");

            entity.Property(e => e.IdIssuedPassport).ValueGeneratedOnAdd();
            entity.Property(e => e.NameIssuedPassport).HasMaxLength(113);
        });

        modelBuilder.Entity<IssuedPassport>(entity =>
        {
            entity.HasKey(e => e.IdIssuedPassport).HasName("PK_Passport");

            entity.ToTable("IssuedPassport");

            entity.Property(e => e.IssuedPassport1)
                .HasMaxLength(100)
                .HasColumnName("IssuedPassport");
        });

        modelBuilder.Entity<LogBook>(entity =>
        {
            entity.HasKey(e => e.IdLogBook);

            entity.ToTable("LogBook");

            entity.Property(e => e.DateLog).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(250);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.LogBooks)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogBook_Role");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.LogBooks)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogBook_User");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.IdPost);

            entity.ToTable("Post");

            entity.HasIndex(e => e.NamePost, "UQ_NamePost").IsUnique();

            entity.Property(e => e.NamePost).HasMaxLength(50);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IdProjects);

            entity.Property(e => e.DevBudget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EndOfDev).HasColumnType("date");
            entity.Property(e => e.NameProjects).HasMaxLength(100);
            entity.Property(e => e.PurposeProjects).HasMaxLength(100);
            entity.Property(e => e.StartOfDev).HasColumnType("date");

            entity.HasOne(d => d.IdStatusOfProjectNavigation).WithMany(p => p.Projects)
                .HasForeignKey(d => d.IdStatusOfProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_StatusOfProject");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.ToTable("Role");

            entity.HasIndex(e => e.NameRole, "UQ_NameRole").IsUnique();

            entity.Property(e => e.NameRole).HasMaxLength(50);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.IdStaff);

            entity.Property(e => e.DateOfBirthStaff).HasColumnType("date");
            entity.Property(e => e.DateOfIssuedPassport).HasColumnType("date");
            entity.Property(e => e.EmailStaff).HasMaxLength(50);
            entity.Property(e => e.FirstNameStaff).HasMaxLength(50);
            entity.Property(e => e.MiddleNameStaff).HasMaxLength(50);
            entity.Property(e => e.OthersData).HasMaxLength(150);
            entity.Property(e => e.PhoneNumberStaff).HasMaxLength(20);
            entity.Property(e => e.SecondNameStaff).HasMaxLength(50);

            entity.HasOne(d => d.IdAddressNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.IdAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Address");

            entity.HasOne(d => d.IdIssuedPassportNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.IdIssuedPassport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_IssuedPassport");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Post");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_User");
        });

        modelBuilder.Entity<StatusOfProject>(entity =>
        {
            entity.HasKey(e => e.IdStatusOfProject);

            entity.ToTable("StatusOfProject");

            entity.Property(e => e.NameStatusOfProject).HasMaxLength(50);
        });

        modelBuilder.Entity<Street>(entity =>
        {
            entity.HasKey(e => e.IdStreet);

            entity.ToTable("Street");

            entity.HasIndex(e => e.NameStreet, "UQ_NameStreet").IsUnique();

            entity.Property(e => e.NameStreet).HasMaxLength(50);
        });

        modelBuilder.Entity<TypeOfClient>(entity =>
        {
            entity.HasKey(e => e.IdTypeOfClient);

            entity.ToTable("TypeOfClient");

            entity.HasIndex(e => e.NameTypeOfClient, "UQ_NameTypeOfClient").IsUnique();

            entity.Property(e => e.NameTypeOfClient).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.HasIndex(e => e.LoginUser, "UQ_LoginUser").IsUnique();

            entity.Property(e => e.LoginUser).HasMaxLength(30);
            entity.Property(e => e.PasswordUser).HasMaxLength(40);

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
