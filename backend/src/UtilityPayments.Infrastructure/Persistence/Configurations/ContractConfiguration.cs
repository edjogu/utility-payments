using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.Contracts;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class ContractConfiguration
    : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("contracts", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ContractNumber)
            .HasColumnName("contract_number")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ContractProductId)
            .HasColumnName("contract_product_id")
            .IsRequired();

        builder.Property(x => x.ContractStatusId)
            .HasColumnName("contract_status_id")
            .IsRequired();

        builder.Property(x => x.ContractDate)
            .HasColumnName("contract_date")
            .IsRequired();

        builder.HasIndex(x => x.ContractNumber)
            .IsUnique()
            .HasDatabaseName("uq_contracts_contract_number");

        builder.HasIndex(x => x.ContractProductId)
            .HasDatabaseName("ix_contracts_contract_product_id");

        builder.HasIndex(x => x.ContractStatusId)
            .HasDatabaseName("ix_contracts_contract_status_id");

        builder.HasOne<ContractProduct>()
            .WithMany()
            .HasForeignKey(x => x.ContractProductId);

        builder.HasOne<ContractStatus>()
            .WithMany()
            .HasForeignKey(x => x.ContractStatusId);
    }
}
