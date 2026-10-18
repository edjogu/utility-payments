using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.Contracts;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class ContractStatusConfiguration
    : IEntityTypeConfiguration<ContractStatus>
{
    public void Configure(EntityTypeBuilder<ContractStatus> builder)
    {
        builder.ToTable("cls_contract_statuses", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique()
            .HasDatabaseName("uq_cls_contract_statuses_code");
    }
}
