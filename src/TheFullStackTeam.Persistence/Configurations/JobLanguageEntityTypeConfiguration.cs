using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class JobLanguageEntityTypeConfiguration : IEntityTypeConfiguration<JobLanguage>
{
    public void Configure(EntityTypeBuilder<JobLanguage> builder)
    {
        builder.ToTable("JobLanguage");
        builder.HasKey(jl => new { jl.JobId, jl.LanguageId });

        builder.HasOne(jl => jl.Job)
         .WithMany(j => j.RequiredLanguages)
         .HasForeignKey(jl => jl.JobId);
    }
}
