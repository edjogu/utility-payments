using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.Clients;
using UtilityPayments.Domain.Contracts;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class CustomerContractRelationConfiguration
    : IEntityTypeConfiguration<CustomerContractRelation>
{
    public void Configure(
        EntityTypeBuilder<CustomerContractRelation> builder)
    {
        builder.ToTable("customer_contract_relations", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ClientId)
            .HasColumnName("client_id")
            .IsRequired();

        builder.Property(x => x.ContractId)
            .HasColumnName("contract_id")
            .IsRequired();

        builder.Property(x => x.CustomerContractRelationTypeId)
            .HasColumnName("customer_contract_relation_type_id")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.ClientId,
            x.ContractId,
            x.CustomerContractRelationTypeId
        })
            .IsUnique()
            .HasDatabaseName("uq_customer_contract_relations");

        builder.HasIndex(x => x.ClientId)
            .HasDatabaseName(
                "ix_customer_contract_relations_client_id");

        builder.HasIndex(x => x.ContractId)
            .HasDatabaseName(
                "ix_customer_contract_relations_contract_id");

        builder.HasIndex(x => x.CustomerContractRelationTypeId)
            .HasDatabaseName(
                "ix_customer_contract_relations_relation_type_id");

        builder.HasIndex(x => new
        {
            x.ClientId,
            x.IsActive
        })
            .HasDatabaseName(
                "ix_customer_contract_relations_client_id_is_active");

        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId);

        builder.HasOne<Contract>()
            .WithMany()
            .HasForeignKey(x => x.ContractId);

        builder.HasOne<CustomerContractRelationType>()
            .WithMany()
            .HasForeignKey(x => x.CustomerContractRelationTypeId);
    }
}
