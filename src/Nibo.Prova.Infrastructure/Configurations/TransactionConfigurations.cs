using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nibo.Prova.Domain.Models.Transactions;

namespace Nibo.Prova.Infrastructure.Configurations
{
    class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(cr => cr.Id)
                .HasName("Id");

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.DatePosted)
                .HasColumnName("DatePosted")
                .IsRequired();

            builder.Property(x => x.TransAmount)
                .HasColumnName("TransAmount")
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(x => x.Memo)
                .HasColumnName("Memo")
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(x => x.TransactionType)
                .HasColumnName("Type")
                .IsRequired();
        }
    }
}
