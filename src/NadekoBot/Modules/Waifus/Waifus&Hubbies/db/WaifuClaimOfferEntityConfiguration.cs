using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

public class WaifuClaimOfferEntityConfiguration : IEntityTypeConfiguration<WaifuClaimOffer>
{
    public void Configure(EntityTypeBuilder<WaifuClaimOffer> builder)
    {
        builder.HasKey(offer => offer.Id);
        builder.HasIndex(offer => new { offer.WaifuUserId, offer.OffererUserId }).IsUnique();
    }
}