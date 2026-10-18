using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.Contracts;
using UtilityPayments.Domain.Currencies;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class ContractProductConfiguration
    : IEntityTypeConfiguration<ContractProduct>
{
    public void Configure(EntityTypeBuilder<ContractProduct> builder)
    {
        builder.ToTable("contract_products", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ContractTypeId)
            .HasColumnName("contract_type_id")
            .IsRequired();

        builder.Property(x => x.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(50)
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
            .HasDatabaseName("uq_contract_products_code");

        builder.HasIndex(x => x.ContractTypeId)
            .HasDatabaseName("ix_contract_products_contract_type_id");

        builder.HasIndex(x => x.CurrencyId)
            .HasDatabaseName("ix_contract_products_currency_id");

        builder.HasOne<ContractType>()
            .WithMany()
            .HasForeignKey(x => x.ContractTypeId);

        builder.HasOne<Currency>()
            .WithMany()
            .HasForeignKey(x => x.CurrencyId);
    }
}
