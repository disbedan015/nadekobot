using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

public class WaifuPossessionEntityConfiguration : IEntityTypeConfiguration<WaifuPossession>
{
    public void Configure(EntityTypeBuilder<WaifuPossession> builder)
    {
        builder.HasKey(wp => wp.Id);
        builder.HasIndex(wp => new { wp.WaifuInfoId, wp.WaifuItemId }).IsUnique();

        builder.HasOne(wp => wp.WaifuInfo)
            .WithMany(wi => wi.Possessions)
            .HasForeignKey(wp => wp.WaifuInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(wp => wp.WaifuItem)
            .WithMany()
            .HasForeignKey(wp => wp.WaifuItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(wp => wp.Quantity).IsRequired().HasDefaultValue(1);
    }
}