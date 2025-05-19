using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

public class WaifuEquippedItemEntityConfiguration : IEntityTypeConfiguration<WaifuEquippedItem>
{
    public void Configure(EntityTypeBuilder<WaifuEquippedItem> builder)
    {
        builder.HasKey(ei => ei.Id);

        builder.HasIndex(ei => new { ei.WaifuInfoId, ei.Slot }).IsUnique();

        builder.HasOne(ei => ei.WaifuInfo)
            .WithMany(wi => wi.EquippedItems)
            .HasForeignKey(ei => ei.WaifuInfoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ei => ei.WaifuItem)
            .WithMany()
            .HasForeignKey(ei => ei.WaifuItemId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.Property(ei => ei.Slot).IsRequired();
    }
}
