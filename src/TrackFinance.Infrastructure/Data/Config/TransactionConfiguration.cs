using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackFinance.Core.TransactionAgregate;

namespace TrackFinance.Infrastructure.Data.Config;
public class TransactionConfiguration: IEntityTypeConfiguration<Transaction>
{
  public void Configure(EntityTypeBuilder<Transaction> builder)
  {
   builder.Property(d => d.Description).IsRequired();
    builder.Property(a => a.Amount).IsRequired().HasPrecision(15,3);
    builder.Property(e => e.TransactionDescriptionType).IsRequired();
    builder.Property(e => e.ExpenseDate).IsRequired();
    builder.Property(e => e.UserId).IsRequired();
  }
}
