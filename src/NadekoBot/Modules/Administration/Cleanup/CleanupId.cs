using System.ComponentModel.DataAnnotations;

namespace NadekoBot.Modules.Administration.Cleanup;

public sealed class CleanupId
{
    [Key]
    public ulong GuildId { get; set; }
}