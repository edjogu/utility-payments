using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UtilityPayments.Domain.UtilityPaymentAgreements;
using UtilityPayments.Domain.UtilityPaymentRequiredInfos;

namespace UtilityPayments.Infrastructure.Persistence.Configurations;

public sealed class UtilityPaymentRequiredInfoConfiguration
    : IEntityTypeConfiguration<UtilityPaymentRequiredInfo>
{
    public void Configure(
        EntityTypeBuilder<UtilityPaymentRequiredInfo> builder)
    {
        builder.ToTable("utility_payment_required_info", "public");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.UtilityPaymentAgreementId)
            .HasColumnName("utility_payment_agreement_id")
            .IsRequired();

        builder.Property(x => x.Code)
            .HasColumnName("code")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Label)
            .HasColumnName("label")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.DataType)
            .HasColumnName("data_type")
            .HasMaxLength(20)
            .HasConversion(
                value => value == RequiredInfoDataType.Text
                    ? "text"
                    : value == RequiredInfoDataType.Number
                        ? "number"
                        : "date",
                value => value == "text"
                    ? RequiredInfoDataType.Text
                    : value == "number"
                        ? RequiredInfoDataType.Number
                        : RequiredInfoDataType.Date)
            .IsRequired();

        builder.Property(x => x.IsRequired)
            .HasColumnName("is_required")
            .IsRequired();

        builder.Property(x => x.DisplayOrder)
            .HasColumnName("display_order")
            .IsRequired();

        builder.Property(x => x.MinLength)
            .HasColumnName("min_length");

        builder.Property(x => x.MaxLength)
            .HasColumnName("max_length");

        builder.Property(x => x.ValidationPattern)
            .HasColumnName("validation_pattern")
            .HasMaxLength(300);

        builder.Property(x => x.Placeholder)
            .HasColumnName("placeholder")
            .HasMaxLength(150);

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(x => x.CreatedAtUtc)
            .HasColumnName("created_at_utc")
            .IsRequired();

        builder.Property(x => x.UpdatedAtUtc)
            .HasColumnName("updated_at_utc");

        builder.HasIndex(x => x.UtilityPaymentAgreementId)
            .HasDatabaseName(
                "ix_utility_payment_required_info_agreement_id");

        builder.HasIndex(x => new
        {
            x.UtilityPaymentAgreementId,
            x.Code
        })
            .IsUnique()
            .HasDatabaseName(
                "uq_utility_payment_required_info_agreement_code");

        builder.HasIndex(x => new
        {
            x.UtilityPaymentAgreementId,
            x.DisplayOrder
        })
            .IsUnique()
            .HasDatabaseName(
                "uq_utility_payment_required_info_agreement_order");

        builder.HasOne<UtilityPaymentAgreement>()
            .WithMany()
            .HasForeignKey(x => x.UtilityPaymentAgreementId);
    }
}
