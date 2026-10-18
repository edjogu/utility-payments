using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.Contracts;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class CustomerContractRelationTypeConfiguration
    : IEntityTypeConfiguration<CustomerContractRelationType>
{
    public void Configure(
        EntityTypeBuilder<CustomerContractRelationType> builder)
    {
        builder.ToTable(
            "cls_customer_contract_relation_types",
            "public");

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
            .HasDatabaseName(
                "uq_cls_customer_contract_relation_types_code");
    }
}
