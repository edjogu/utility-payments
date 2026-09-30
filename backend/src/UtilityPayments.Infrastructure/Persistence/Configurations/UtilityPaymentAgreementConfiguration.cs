using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.UtilityPaymentAgreements;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public class UtilityPaymentAgreementConfiguration
    : IEntityTypeConfiguration<UtilityPaymentAgreement>
{
    public void Configure(EntityTypeBuilder<UtilityPaymentAgreement> builder)
    {
        builder.ToTable("utility_payment_agreements", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(300);

        builder.Property(x => x.IntegrationKey)
            .HasColumnName("integration_key")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.AllowInformationRequest)
            .HasColumnName("allow_information_request")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(x => x.Code)
            .IsUnique()
            .HasDatabaseName("uq_utility_payment_agreements_code");

        builder.HasIndex(x => x.IntegrationKey)
            .HasDatabaseName("ix_utility_payment_agreements_integration_key");

        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_utility_payment_agreements_is_active");
    }
}
