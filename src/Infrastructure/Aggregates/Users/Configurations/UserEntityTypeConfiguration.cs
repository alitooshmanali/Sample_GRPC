using Domain.Aggregates.Users;
using Domain.Aggregates.Users.ValueObjects;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Aggregates.Users.Configurations
{
    public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(WriteDbContext.Users));

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .HasConversion(i => i.Value, i => UserId.Create(i));

            builder.Property(i => i.Name)
                .IsRequired()
                .HasConversion(i => i.Value, i => Name.Create(i));

            builder.Property(i => i.Surname)
            .IsRequired()
                .HasConversion(i => i.Value, i => Surname.Create(i));

            builder.Property(i => i.NationalCode)
            .IsRequired()
                .HasConversion(i => i.Value, i => NationalCode.Create(i));

            builder.Property(i => i.BirthDay)
            .IsRequired()
                .HasConversion(i => i.Value, i => BirthDay.Create(i));

            base.Configure(builder);
        }
    }

}
