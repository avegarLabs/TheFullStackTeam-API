using Microsoft.EntityFrameworkCore;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.Entities.Base;
using TheFullStackTeam.Persistence.Configurations;

namespace TheFullStackTeam.Persistence.App;

/// <summary>
/// Application database context
/// </summary>
public class TheFullStackTeamDbContext : DbContext
{
    /// <summary>
    /// Application database context constructor
    /// </summary>
    /// <param name="options">Context database options</param>
    public TheFullStackTeamDbContext(DbContextOptions<TheFullStackTeamDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseLazyLoadingProxies();

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Title> Titles { get; set; } = null!;
    public DbSet<Certification> Certifications { get; set; } = null!;
    public DbSet<Honor> Honors { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Education> Educations { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Professional> Professionals { get; set; } = null!;
    public DbSet<ProfessionalSkill> ProfessionalSkills { get; set; } = null!;
    public DbSet<ProfessionalServiceCategory> ProfessionalServiceCategories { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<SkillCategory> SkillCategories { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<CourseTranslation> CourseTranslations { get; set; } = null!;
    public DbSet<EducationTranslation> EducationTranslations { get; set; } = null!;
    public DbSet<HonorTranslation> HonorTranslations { get; set; } = null!;
    public DbSet<ExperienceTranslation> PositionTranslations { get; set; } = null!;
    public DbSet<ProfessionalTranslation> ProfessionalTranslations { get; set; } = null!;
    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Effort> Efforts { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ProjectTask> ProjectTasks { get; set; } = null!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;

    public DbSet<ProfessionalSevices> ProfessionalSevices { get; set; } = null!;

    public DbSet<ProfessionalContractType> ProfessionalContractTypes { get; set; } = null!;
    public DbSet<ProfessionalJobType> ProfessionalJobTypes { get; set; } = null!;
    public DbSet<ProfessionalSalaryType> ProfessionalSalaryTypes { get; set; } = null!;

    public DbSet<Job> Jobs { get; set; } = null!;
    public DbSet<JobContractType> JobContractTypes { get; set; } = null!;
    public DbSet<JobsJobType> JobsJobTypes { get; set; } = null!;
    
    public DbSet<JobsSalaryType> jobsSalaryTypes { get; set; } = null!;

    public DbSet<JobResponsabilities> JobResponsabilities { get; set; } = null!;

    public DbSet<JobSkill> JobSkill { get; set; } = null!;

    public DbSet<Portfolio> Portfolio { get; set; } = null!;
    public DbSet<PortfolioArchievements> PortfolioArchievements { get; set; } = null!;

    public DbSet<SkillPortfolio> skillPortfolios { get; set; } = null!;

    public DbSet<Invoice> Invoice { get; set; } = null!;

    public DbSet<Timesheet> Timesheet { get; set; } = null!;
    public DbSet<Contracts> Contracts { get; set; } = null!;

   
    public DbSet<Institution> institutions { get; set; } = null!;

    public DbSet<OrganizationSevices> OrganizationSevices { get; set; } = null!;
    public DbSet<OrganizationServiceCategory> OrganizationServiceCategories { get; set; } = null!;


    public DbSet<ProfessionalLanguage> ProfessionalLanguages { get; set; } = null!;

    public DbSet<Roles> Roles { get; set; } = null!;
    public DbSet<UserRoles> UserRole { get; set; } = null!;

    public DbSet<Cities> Cities { get; set; } = null!;
    public DbSet<JobLanguage> JobLanguages { get; set; } = null!;

    /// <inheritdoc cref="DbContext"/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UsersEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TitleEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CertificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new HonorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EducationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrganizationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PositionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CountryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalSkillEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SkillEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SkillCategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new LanguageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseTranslationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EducationTranslationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new HonorTranslationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ExperienceTranslationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalTranslationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ClientEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EffortEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectTaskEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentMethodEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalServicesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalServiceCategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalJobEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalContractEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalSalaryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobContractEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobJobEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobSalaryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobResponsabilitiesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobSkillEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PortfolioAchievementsEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SkillPortfolioEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ContractsEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TimesSheetEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new InstitutionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrganizationServicesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OrganizationServiceCategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RolesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserRolesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProfessionalLanguegeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CitiesEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new JobLanguageEntityTypeConfiguration());
    }

    public override int SaveChanges()
    {
        SetDates();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetDates();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetDates()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).Modified = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).Created = DateTime.UtcNow;
            }
        }
    }
}