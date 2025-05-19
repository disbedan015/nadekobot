using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

public class WaifuItemEntityConfiguration : IEntityTypeConfiguration<WaifuItem>
{
    public void Configure(EntityTypeBuilder<WaifuItem> builder)
    {
        builder.HasKey(item => item.Id);
        builder.HasIndex(item => item.Name).IsUnique();
        builder.Property(item => item.Price).IsRequired();
        builder.Property(item => item.Value).IsRequired();
        builder.Property(item => item.ItemType).IsRequired();
        builder.Property(item => item.SlotType).HasDefaultValue(WaifuItemSlot.NotApplicable);
    }
}