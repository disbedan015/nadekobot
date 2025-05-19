using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

public class WaifuInfoEntityConfiguration : IEntityTypeConfiguration<WaifuInfo>
{
    public void Configure(EntityTypeBuilder<WaifuInfo> builder)
    {
        builder.HasKey(wi => wi.Id);
        builder.HasIndex(wi => wi.UserId).IsUnique();
        builder.HasIndex(wi => wi.OwnerId);

        builder.Property(wi => wi.Price).HasDefaultValue(1000);
        builder.Property(wi => wi.Ascension).HasDefaultValue(0);
        builder.Property(wi => wi.Mood).HasDefaultValue(100);
        builder.Property(wi => wi.Rest).HasDefaultValue(100);
        builder.Property(wi => wi.Food).HasDefaultValue(100);
        builder.Property(wi => wi.IsForSale).HasDefaultValue(false);
    }
}