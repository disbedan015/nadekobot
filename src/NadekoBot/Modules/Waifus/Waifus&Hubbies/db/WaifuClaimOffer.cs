namespace NadekoBot.Modules.Waifus.WaifusHubbies.Db;

/// <summary>
/// Represents an offer made by a user to claim/buy a Waifu/Hubby.
/// </summary>
public class WaifuClaimOffer
{
    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Discord User ID of the Waifu/Hubby being offered for.
    /// </summary>
    public ulong WaifuUserId { get; set; }

    /// <summary>
    /// Gets or sets the Discord User ID of the user making the offer.
    /// </summary>
    public ulong OffererUserId { get; set; }

    /// <summary>
    /// Gets or sets the amount offered.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the offer was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the date and time when the offer expires.
    /// Null if the offer does not expire.
    /// </summary>
    public DateTime? ExpiresAt { get; set; }
}