using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(p => p.CourseNumber).HasMaxLength(Course.CourseNumberMaxLenght);
        builder.Property(p => p.Occupation).HasMaxLength(Course.OccupationMaxLenght);
    }
}
