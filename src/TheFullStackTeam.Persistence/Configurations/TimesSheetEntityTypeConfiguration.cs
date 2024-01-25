using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// UserProfile entity type configuration
/// </summary>
public class TimesSheetEntityTypeConfiguration : IEntityTypeConfiguration<Timesheet>
{
    public void Configure(EntityTypeBuilder<Timesheet> builder)
    {
        builder.ToTable("Timesheet");


        builder.Property(p => p.Month);
        builder.Property(p => p.State);
    }
}