using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;

namespace TheFullStackTeam.Persistence.Configurations;

public class EducationEntityTypeConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.Property(p => p.Notes).HasMaxLength(Education.NotesMaxLenght);
        builder.Property(p => p.Program).HasMaxLength(Education.ProgramMaxLenght);
        builder.Property(p => p.DegreeName).HasMaxLength(Education.DegreeNameMaxLenght);
        builder.Property(p => p.FieldsOfStudy).HasMaxLength(Education.FieldsOfStudyMaxLenght);
    }
}
