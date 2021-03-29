using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebLogic
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasMany(ques => ques.Responses)
                .WithOne(r => r.Report)
                .HasForeignKey(r => r.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.Created)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
