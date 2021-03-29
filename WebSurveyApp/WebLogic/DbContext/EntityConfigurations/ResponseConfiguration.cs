using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebLogic
{
    public class ResponseConfiguration : IEntityTypeConfiguration<Response>
    {
        public void Configure(EntityTypeBuilder<Response> builder)
        {
            builder.HasOne(r => r.Question)
                .WithMany(qe => qe.Responses)
                .HasForeignKey(r => r.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Option)
                .WithMany(qe => qe.Responses)
                .HasForeignKey(r => r.OptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.HasAlternateKey(r => new { r.QuestionId, r.OptionId, r.ReportId });
        }
    }
}
