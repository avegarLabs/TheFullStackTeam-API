using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class ProjectTaskEntityTypeConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.Property(p => p.Description).HasMaxLength(ProjectTask.DescriptionMaxLength);
        builder.Property(p => p.Name).HasMaxLength(ProjectTask.NameMaxLength);
    }
}