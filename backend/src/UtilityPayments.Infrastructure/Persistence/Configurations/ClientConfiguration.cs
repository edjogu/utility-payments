using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.Clients;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class ClientConfiguration
    : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ClientTypeId)
            .HasColumnName("client_type_id")
            .IsRequired();

        builder.Property(x => x.ClientCode)
            .HasColumnName("client_code")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.DocumentNumber)
            .HasColumnName("document_number")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.HasIndex(x => x.ClientCode)
            .IsUnique()
            .HasDatabaseName("uq_clients_client_code");

        builder.HasIndex(x => x.DocumentNumber)
            .IsUnique()
            .HasDatabaseName("uq_clients_document_number");

        builder.HasIndex(x => x.ClientTypeId)
            .HasDatabaseName("ix_clients_client_type_id");

        builder.HasOne<ClientType>()
            .WithMany()
            .HasForeignKey(x => x.ClientTypeId);
    }
}
