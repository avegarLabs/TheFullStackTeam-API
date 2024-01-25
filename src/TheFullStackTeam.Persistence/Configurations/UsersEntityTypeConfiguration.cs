using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheFullStackTeam.Domain.Entities;
using TheFullStackTeam.Domain.Entities.Base;

namespace TheFullStackTeam.Persistence.Configurations;

/// <summary>
/// UserProfile entity type configuration
/// </summary>
public class UsersEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(pk => pk.Id);

        builder.HasIndex(ix => ix.Moniker).IsUnique();
        builder.HasIndex(ix => ix.AccountId).IsUnique();

        builder.Property(p => p.Moniker).HasMaxLength(NicknamedEntity.MonikerMaxLenght);
        builder.Property(p => p.Name).HasMaxLength(User.NameMaxLenght);
        builder.Property(p => p.Phone).HasMaxLength(Professional.PhoneMaxLenght);
        builder.Property(p => p.ContactEmail).HasMaxLength(Professional.ContactEmailMaxLenght);
      
        //Value object config
        builder.OwnsOne(a => a.Picture).WithOwner();
        builder.OwnsOne(a => a.Address).WithOwner();

        builder.HasOne(o => o.Country)
          .WithMany(co => co.UserProfiles)
          .HasForeignKey(fk => fk.CountryId)
          .IsRequired(false)
          .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Professional)
          .WithOne(p => p.User)
          .HasForeignKey<Professional>(or => or.UserId);


    }
}