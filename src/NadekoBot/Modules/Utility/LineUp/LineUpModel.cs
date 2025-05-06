using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NadekoBot.Modules.Utility.LineUp;

/// <summary>
/// Represents a user waiting in a lineup for a specific channel.
/// </summary>
public class LineUpUser
{
    /// <summary>
    /// Gets or sets the unique identifier of the Guild where the lineup exists.
    /// </summary>
    public required ulong GuildId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the Channel where the lineup exists.
    /// </summary>
    public required ulong ChannelId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the User in the lineup.
    /// </summary>
    public required ulong UserId { get; set; }

    /// <summary>
    /// Gets or sets the optional text the user provided when joining.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user joined the lineup.
    /// </summary>
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Configures the database mapping for the <see cref="LineUpUser"/> entity.
/// </summary>
public class LineUpUserConfiguration : IEntityTypeConfiguration<LineUpUser>
{
    /// <summary>
    /// Configures the entity of type <see cref="LineUpUser"/>.
    /// </summary>
    /// <param name="builder">The builder to be used to configure the entity type.</param>
    public void Configure(EntityTypeBuilder<LineUpUser> builder)
    {
        // Define composite primary key
        builder.HasKey(lu => new { lu.GuildId, lu.ChannelId, lu.UserId });

        // Configure Reason property
        builder.Property(lu => lu.Reason)
            .HasMaxLength(200) // Optional: Limit the length of the reason
            .IsRequired(false); // Reason is optional

        // Configure DateAdded property
        builder.Property(lu => lu.DateAdded)
            .IsRequired();

        // Add index for efficient querying by guild and channel, ordered by date added
        builder.HasIndex(lu => new { lu.GuildId, lu.ChannelId, lu.DateAdded });
        
        // Add index for efficient querying by user
        builder.HasIndex(lu => new { lu.GuildId, lu.ChannelId, lu.UserId });
    }
}
