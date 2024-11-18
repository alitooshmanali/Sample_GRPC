using Application.Aggregates.Audits;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Aggregates.Audits.Configurations
{
    public class AuditEntityTypeConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.ToTable(nameof(WriteDbContext.Audits));

            builder.HasKey(i => i.Id);
            builder.Property(i => i.Action).IsRequired();
            builder.Property(i => i.Data).IsRequired();
        }
    }
}
